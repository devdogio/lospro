#if PLAYMAKER

using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;

namespace Devdog.LosPro.Integration.PlayMaker
{
    [HutongGames.PlayMaker.Tooltip("Set sight's can instantly be detected")]
    public class SetSightCanInstantlyBeDetected : SetSightTargetConfigBase
    {
        public FsmBool canInstantlyBeDetected;

        public override void OnEnter()
        {
            iSightTarget.config.canInstantlyBeDetected = canInstantlyBeDetected.Value;
            Finish();
        }
    }
}

#endif