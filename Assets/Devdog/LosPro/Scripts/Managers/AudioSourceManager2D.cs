using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Devdog.General;

namespace Devdog.LosPro
{
    [AddComponentMenu(LosPro.AddComponentMenuPath + "Audio Source Manager 2D", -9)]
    [HelpURL("http://devdog.io/unity-assets/los-pro/documentation/components/audio-source-manager")]
    public class AudioSourceManager2D : AudioSourceManagerBase<AudioSourceManager2D>
    {
        protected override void CreateAudioSourcePool()
        {
            var audio2D = CreateAudioSourceNotPooled<AudioSourceBehaviour2D>(Vector2.zero);
            audio2D.gameObject.SetActive(false);
            audioSourcePool = new ComponentPool<IAudioSource>(audio2D);
            audioSourcePool.rootObject.name += "2D";
            audioSourcePool.rootObject.SetParent(transform);
        }

        public static IAudioSource CreateAudioSourcePooled(Vector2 pos, GameObject emitter = null)
        {
            var obj = instance.audioSourcePool.Get();
            obj.gameObject.transform.position = pos;
            obj.emitter = emitter;
           
            return obj;
        }
        
        public static IAudioSource CreateAudioSourceNotPooled(Vector2 pos, GameObject emitter = null)
        {
            return CreateAudioSourceNotPooled<AudioSourceBehaviour2D>(pos, emitter);
        }
        
        public static T CreateAudioSourceNotPooled<T>(Vector2 pos, GameObject emitter = null) where T : UnityEngine.Component, IAudioSource
        {
            var obj = new GameObject("AUDIO_SOURCE");
            obj.transform.SetParent(instance.transform);
            obj.gameObject.transform.position = pos;
            obj.gameObject.transform.localScale = Vector3.zero;

            var audioSource = obj.gameObject.AddComponent<T>();
            audioSource.gameObject.layer = LosManager.instance.settings.hearingLayerID;
            audioSource.emitter = emitter;

            var col = audioSource.gameObject.AddComponent<CircleCollider2D>();
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