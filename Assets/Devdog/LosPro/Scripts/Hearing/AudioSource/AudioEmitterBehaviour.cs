using System;
using System.Collections.Generic;
using UnityEngine;

namespace Devdog.LosPro
{
    [AddComponentMenu(LosPro.AddComponentMenuPath + "Audio/Audio emitter 3D")]
    [HelpURL("http://devdog.io/unity-assets/los-pro/documentation/components/audio-emitter")]
    public sealed class AudioEmitterBehaviour : MonoBehaviour
    {
        public AudioEmitterConfig config = new AudioEmitterConfig();
        
        private void Awake()
        {
            InvokeRepeating("EmitAudioSource", UnityEngine.Random.value, config.emitInterval);
        }

        private void OnEnable()
        {
            
        }

        public void EmitAudioSource()
        {
            if (isActiveAndEnabled)
            {
                var audioSphere = AudioSourceManager.CreateAudioSourcePooled(transform.position, gameObject, config.targetCategory);
                audioSphere.maxGrowthSize = config.emitRange;

                audioSphere.Emit();
            }
        }
    }
}
