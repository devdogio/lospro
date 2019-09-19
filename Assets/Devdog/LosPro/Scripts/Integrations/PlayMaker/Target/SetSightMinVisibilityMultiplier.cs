#if PLAYMAKER

using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;

namespace Devdog.LosPro.Integration.PlayMaker
{
    [HutongGames.PlayMaker.Tooltip("Set sight's min visibility multiplier")]
    public class SetSightMinVisibilityMultiplier : SetSightTargetConfigBase
    {
        public FsmFloat minVisibilityMultiplier;

        public override void OnEnter()
        {
            iSightTarget.config.minVisibilityMultiplier = minVisibilityMultiplier.Value;
            Finish();
        }
    }
}

#endif