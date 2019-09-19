using System;
using System.Collections.Generic;
using UnityEngine;

namespace Devdog.LosPro
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(AudioSourceTriggerHelperBehaviour2D))]
    [HelpURL("http://devdog.io/unity-assets/los-pro/documentation/components/audio-source")]
    public class AudioSourceBehaviour2D : AudioSourceBehaviour
    {
        public override void Emit()
        {
            if (AudioSourceManager2D.instance.activeAudioSources.Contains(this))
            {
                Debug.LogWarning("Audio source has already been emitted! You can create a new audio source by using the AudioSourceManager.Get / Create methods", gameObject);
                return;
            }

            AudioSourceManager2D.AddAudioSource(this);
        }
    }
}
