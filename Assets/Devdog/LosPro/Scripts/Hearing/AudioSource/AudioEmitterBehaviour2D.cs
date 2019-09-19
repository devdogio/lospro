using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Assertions;

namespace Devdog.LosPro
{
    [AddComponentMenu(LosPro.AddComponentMenuPath + "Audio/Audio emitter 2D")]
    [HelpURL("http://devdog.io/unity-assets/los-pro/documentation/components/audio-emitter")]
    public sealed class AudioEmitterBehaviour2D : MonoBehaviour
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
                var audioSphere = AudioSourceManager2D.CreateAudioSourcePooled(transform.position, gameObject);
                audioSphere.maxGrowthSize = config.emitRange;

                audioSphere.Emit();
            }
        }
    }
}
