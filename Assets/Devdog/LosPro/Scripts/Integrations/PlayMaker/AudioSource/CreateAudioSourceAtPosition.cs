#if PLAYMAKER

using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;

namespace Devdog.LosPro.Integration.PlayMaker
{
    [HutongGames.PlayMaker.Tooltip("Create an audio source at a given position. The audio clip (actual sound) is not required.")]
    public class CreateAudioSourceAtPosition : CreateAudioSourceBase
    {
        public FsmVector3 createAtPosition;

        public override void OnEnter()
        {
            var source = CreateAudiosource(createAtPosition.Value);
            source.Emit();

            Finish();
        }
    }
}

#endif