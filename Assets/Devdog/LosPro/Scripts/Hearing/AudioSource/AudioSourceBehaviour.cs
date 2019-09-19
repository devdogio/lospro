using System;
using System.Collections.Generic;
using UnityEngine;

namespace Devdog.LosPro
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(AudioSourceTriggerHelperBehaviour))]
    [HelpURL("http://devdog.io/unity-assets/los-pro/documentation/components/audio-source")]
    public class AudioSourceBehaviour : MonoBehaviour, IAudioSource
    {
        private AudioSource _audioSource;
        public AudioSource audioSource
        {
            get
            {
                if (_audioSource == null)
                {
                    _audioSource = GetComponent<AudioSource>();
                }

                return _audioSource;
            }
        }

        public GameObject emitter { get; set; }
        public int emitterCategory { get; set; }

        public float maxGrowthSize
        {
            get { return audioSource.maxDistance; }
            set { audioSource.maxDistance = value; }
        }

        [SerializeField]
        private float _growthSizeFactor = 1f;
        public float growthSizeFactor
        {
            get { return _growthSizeFactor; }
            set { _growthSizeFactor = value; }
        }


        private readonly List<IListener> _heardBy = new List<IListener>(2);


        protected void Awake()
        {

        }

        public virtual void Emit()
        {
            if (AudioSourceManager.instance.activeAudioSources.Contains(this))
            {
                Debug.LogWarning("Audio source has already been emitted! You can create a new audio source by using the AudioSourceManager.Get / Create methods", gameObject);
                return;
            }

            AudioSourceManager.AddAudioSource(this);
        }

        public bool IsHeardBy(IListener listener)
        {
            return _heardBy.Contains(listener);
        }

        public void OnHeardBy(AudioSourceInfo info)
        {
            _heardBy.Add(info.listener);

            // Debug.Log("Audio source " + gameObject.name + " is heard by " + listener.gameObject.name, gameObject);
        }

        public void ResetStateForPool()
        {
            _heardBy.Clear();
        }
    }
}
