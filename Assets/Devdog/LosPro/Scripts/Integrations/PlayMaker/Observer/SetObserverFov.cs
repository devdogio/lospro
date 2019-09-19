#if PLAYMAKER

using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;

namespace Devdog.LosPro.Integration.PlayMaker
{
    [HutongGames.PlayMaker.Tooltip("Set an observer's FOV")]
    public class SetObserverFov : SetObserverConfigBase
    {
        [HasFloatSlider(-1f, 1f)]
        public FsmFloat dotValue;

        public override void OnEnter()
        {
            iobserver.config.fieldOfViewDotValue = dotValue.Value;
            Finish();
        }
    }
}

#endif