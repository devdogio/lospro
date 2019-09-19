#if PLAYMAKER

using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;

namespace Devdog.LosPro.Integration.PlayMaker
{
    [HutongGames.PlayMaker.Tooltip("Set observer's min visibility")]
    public class SetObserverMinVisibility : SetObserverConfigBase
    {
        [HasFloatSlider(0, 1f)]
        public FsmFloat minVisibility;

        public override void OnEnter()
        {
            iobserver.config.minVisibleFactor = minVisibility.Value;
            Finish();
        }
    }
}

#endif