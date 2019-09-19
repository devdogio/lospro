#if BEHAVIOR_DESIGNER

using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace Devdog.LosPro.Integration.BehaviorDesigner
{
    public abstract class CreateAudioSourceBase : Action
    {
        public SharedGameObject emitter;
        public AudioClip clip;
        public SharedFloat volume = 1f;

        [global::BehaviorDesigner.Runtime.Tasks.Tooltip("The range of the audio source expansion.")]
        public SharedFloat range = 10f;
        public bool playOneShot = true;

        public SharedInt emitterCategoryBitFlag = -1;

        protected virtual IAudioSource CreateAudiosource(Vector3 position)
        {
            var source = AudioSourceManager.CreateAudioSourcePooled(position, emitter.Value ?? gameObject, emitterCategoryBitFlag.Value);
            source.maxGrowthSize = range.Value;

            if (clip != null)
            {
                if (playOneShot)
                {
                    source.audioSource.PlayOneShot(clip, volume.Value);
                }
                else
                {
                    source.audioSource.clip = clip;
                    source.audioSource.Play();
                }
            }

            return source;
        }
    }
}

#endif