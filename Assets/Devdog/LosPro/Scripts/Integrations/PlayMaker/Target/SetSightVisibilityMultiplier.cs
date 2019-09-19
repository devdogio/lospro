#if PLAYMAKER

using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;

namespace Devdog.LosPro.Integration.PlayMaker
{
    [HutongGames.PlayMaker.Tooltip("Set sight's visibility multiplier")]
    public class SetSightVisibilityMultiplier : SetSightTargetConfigBase
    {
        public FsmFloat visibilityMultiplier;

        public override void OnEnter()
        {
            iSightTarget.config.visibilityMultiplier = visibilityMultiplier.Value;
            Finish();
        }
    }
}

#endif