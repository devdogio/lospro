#if PLAYMAKER

using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;

namespace Devdog.LosPro.Integration.PlayMaker
{
    [HutongGames.PlayMaker.Tooltip("Set sight's detection time")]
    public class SetSightTargetDetectionTime : SetSightTargetConfigBase
    {
        public FsmFloat detectionTime;

        public override void OnEnter()
        {
            iSightTarget.config.detectionTime = detectionTime.Value;
            Finish();
        }
    }
}

#endif