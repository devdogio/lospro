using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Devdog.General;

namespace Devdog.LosPro
{
    [RequireComponent(typeof(LosManager))]
    public abstract class AudioSourceManagerBase<T> : MonoBehaviour where T : AudioSourceManagerBase<T>
    {
        private static T _instance;
        public static T instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();

                    if (_instance == null)
                    {
                        Debug.LogError("No instance of " + typeof(T).Name + " in the scene, yet it is requested. (one will be created)");
                        var obj = new GameObject("_Managers");
                        _instance = obj.AddComponent<T>();
                    }
                }

                return _instance;
            }
        }

        public static bool hasInstance
        {
            get { return _instance != null; }
        }

        private readonly List<IAudioSource> _activeAudioSources = new List<IAudioSource>(32);
        public List<IAudioSource> activeAudioSources
        {
            get { return _activeAudioSources; }
        }

        protected ComponentPool<IAudioSource> audioSourcePool;
        
        
        protected virtual void Awake()
        {
            CreateAudioSourcePool();
        }

        protected abstract void CreateAudioSourcePool();

        protected virtual void FixedUpdate()
        {
            UpdateAllAudioSources();
        }

        protected virtual void UpdateAllAudioSources()
        {
            for (int i = activeAudioSources.Count - 1; i >= 0; i--)
            {
                var source = activeAudioSources[i];

                bool delete;
                UpdateAudioSource(source, out delete);

                if (delete)
                {
                    activeAudioSources.RemoveAt(i);
                    audioSourcePool.Destroy(source);
                }
            }
        }

        protected virtual void UpdateAudioSource(IAudioSource audioSource, out bool deleteSource)
        {
            deleteSource = false;
            var t = audioSource.gameObject.transform;
            t.localScale += Vector3.one * (LosManager.instance.settings.speedOfSoundUnitsPerSecond / 60f) * audioSource.growthSizeFactor; // / 60f because it's updated through fixed update (60x a sec)

            if (t.localScale.x > audioSource.maxGrowthSize)
            {
                deleteSource = true;
            }
        }
    }
}