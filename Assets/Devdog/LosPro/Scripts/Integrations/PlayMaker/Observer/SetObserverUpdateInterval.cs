#if PLAYMAKER

using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;

namespace Devdog.LosPro.Integration.PlayMaker
{
    [HutongGames.PlayMaker.Tooltip("Set an observer's update interval")]
    public class SetObserverUpdateInterval : SetObserverConfigBase
    {
        public FsmFloat updateInterval = 0.5f;

        public override void OnEnter()
        {
            iobserver.config.updateInterval = updateInterval.Value;
            Finish();
        }
    }
}

#endif