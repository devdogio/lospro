#if PLAYMAKER

using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;

namespace Devdog.LosPro.Integration.PlayMaker
{
    [ActionCategory("Los Pro")]
    [HutongGames.PlayMaker.Tooltip("Is the given target visible from the observer?")]
    public class IsTargetVisibleFromObserver : FsmStateAction
    {
        [RequiredField]
        public FsmGameObject target;

        [RequiredField]
        public FsmGameObject observer;

        public FsmBool result = new FsmBool();

        private ISightTarget _target;
        private IObserver _observer;

        public override void Init(FsmState state)
        {
            base.Init(state);
            if (target != null && target.Value != null)
            {
                _target = target.Value.GetComponent<ISightTarget>();
                if (_target == null)
                {
                    LogError("No sight target component found on gameObject");
                }
            }

            if (observer != null && observer.Value != null)
            {
                _observer = observer.Value.GetComponent<IObserver>();
                if (_observer == null)
                {
                    LogError("No observer component found on gameObject.");
                }
            }
        }



        public override void OnEnter()
        {
            result.Value = _observer.sight.IsTargetVisible(_target);
            Finish();
        }
    }
}

#endif