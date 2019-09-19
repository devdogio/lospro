using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Devdog.General;

namespace Devdog.LosPro
{
    [AddComponentMenu(LosPro.AddComponentMenuPath + "Audio Source Manager 3D", -10)]
    [HelpURL("http://devdog.io/unity-assets/los-pro/documentation/components/audio-source-manager")]
    public class AudioSourceManager : AudioSourceManagerBase<AudioSourceManager>
    {
        protected override void CreateAudioSourcePool()
        {
            var audio3D = CreateAudioSourceNotPooled<AudioSourceBehaviour>(Vector3.zero); // Use audio source as default pool object.
            audio3D.gameObject.SetActive(false);
            audioSourcePool = new ComponentPool<IAudioSource>(audio3D);
            audioSourcePool.rootObject.name += "3D";
            audioSourcePool.rootObject.SetParent(transform);
        }

        public static IAudioSource CreateAudioSourcePooled(Vector3 pos, GameObject emitter = null, int emitterCategoryBitFlag = 0)
        {
            var obj = instance.audioSourcePool.Get();
            obj.gameObject.transform.position = pos;
            obj.emitter = emitter;
            obj.emitterCategory = emitterCategoryBitFlag;

            return obj;
        }

        public static IAudioSource CreateAudioSourceNotPooled(Vector3 pos, GameObject emitter = null, int emitterCategoryBitFlag = 0)
        {
            return CreateAudioSourceNotPooled<AudioSourceBehaviour>(pos, emitter, emitterCategoryBitFlag);
        }

        public static T CreateAudioSourceNotPooled<T>(Vector3 pos, GameObject emitter = null, int emitterCategoryBitFlag = 0) where T : UnityEngine.Component, IAudioSource
        {
            var obj = new GameObject("AUDIO_SOURCE");
            obj.transform.SetParent(instance.transform);
            obj.gameObject.transform.position = pos;
            obj.gameObject.transform.localScale = Vector3.zero;

            var audioSource = obj.gameObject.AddComponent<T>();
            audioSource.gameObject.layer = LosManager.instance.settings.hearingLayerID;
            audioSource.emitter = emitter;
            audioSource.emitterCategory = emitterCategoryBitFlag;

            var col = audioSource.gameObject.AddComponent<SphereCollider>();
            col.radius = 1f;
            col.isTrigger = true;

            return audioSource;
        }
        
        public static void AddAudioSource(IAudioSource audioSource)
        {
            instance.activeAudioSources.Add(audioSource);
        }

        public static void RemoveAudioSource(IAudioSource audioSource)
        {
            instance.activeAudioSources.Remove(audioSource);
        }
    }
}