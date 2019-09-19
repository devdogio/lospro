#if PLAYMAKER

using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;

namespace Devdog.LosPro.Integration.PlayMaker
{
    [ActionCategory("Los Pro")]
    public abstract class SetSightTargetConfigBase : FsmStateAction
    {
        protected ISightTarget iSightTarget;

        [RequiredField]
        public FsmGameObject target;

        public override void Init(FsmState state)
        {
            base.Init(state);
            if (target != null && target.Value != null)
            {
                iSightTarget = target.Value.GetComponent<ISightTarget>();
                if (iSightTarget == null)
                {
                    LogError("No sight target found");
                }
            }
        }

        public override void Reset()
        {

        }

        public override void OnEnter()
        {
            
        }
    }
}

#endif