#if PLAYMAKER

using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;

namespace Devdog.LosPro.Integration.PlayMaker
{
    [ActionCategory("Los Pro")]
    public abstract class CreateAudioSourceBase : FsmStateAction
    {
        public AudioClip clip;
        public FsmFloat volume = 1f;

        [HutongGames.PlayMaker.Tooltip("The range of the audio source. All listeners within this range will be able to hear the audio source.")]
        public FsmFloat range = 10f;
        public bool playOneShot = true;

        public FsmInt emitterCategoryBitFlag = -1;

        public override void Init(FsmState state)
        {
            base.Init(state);
        }

        public override void Reset()
        {

        }

//        public override void OnEnter()
//        {
//
//        }

        protected virtual IAudioSource CreateAudiosource(Vector3 position)
        {
            var source = AudioSourceManager.CreateAudioSourcePooled(position, Owner, emitterCategoryBitFlag.Value);
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