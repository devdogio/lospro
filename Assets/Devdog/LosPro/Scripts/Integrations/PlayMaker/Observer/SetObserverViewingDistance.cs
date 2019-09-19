#if PLAYMAKER

using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;

namespace Devdog.LosPro.Integration.PlayMaker
{
    [HutongGames.PlayMaker.Tooltip("Set an observer's configuration")]
    public class SetObserverViewingDistance : SetObserverConfigBase
    {
        public FsmFloat viewingDistance;

        public override void OnEnter()
        {
            iobserver.config.viewingDistance = viewingDistance.Value;
            Finish();
        }
    }
}

#endif