#if PLAYMAKER

using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;

namespace Devdog.LosPro.Integration.PlayMaker
{
    [ActionCategory("Los Pro")]
    public abstract class SetObserverConfigBase : FsmStateAction
    {
        protected IObserver iobserver;

        [RequiredField]
        public FsmGameObject observer;

        public override void Init(FsmState state)
        {
            base.Init(state);
            if (observer != null && observer.Value != null)
            {
                iobserver = observer.Value.GetComponent<IObserver>();
                if (iobserver == null)
                {
                    LogError("No observer found, can't detect targets!");
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