#if PLAYMAKER

using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;

namespace Devdog.LosPro.Integration.PlayMaker
{
    [HutongGames.PlayMaker.Tooltip("Set observer's sample count")]
    public class SetObserverSampleCount : SetObserverConfigBase
    {
        public FsmInt sampleCount;

        public override void OnEnter()
        {
            iobserver.config.sampleCount = sampleCount.Value;
            Finish();
        }
    }
}

#endif