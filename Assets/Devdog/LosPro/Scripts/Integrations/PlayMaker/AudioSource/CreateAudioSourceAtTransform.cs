#if PLAYMAKER

using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;

namespace Devdog.LosPro.Integration.PlayMaker
{
    [HutongGames.PlayMaker.Tooltip("Create an audio source at a given transform's position. The audio clip (actual sound) is not required.")]
    public class CreateAudioSourceAtTransform : CreateAudioSourceBase
    {
        public FsmGameObject createAtPosition;

        public override void OnEnter()
        {
            var source = CreateAudiosource(createAtPosition.Value.transform.position);
            source.emitter = createAtPosition.Value;
            source.Emit();

            Finish();
        }
    }
}

#endif