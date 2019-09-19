using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Assertions;

namespace Devdog.LosPro
{
    public class ObserverTargetInvisibleAggroModule : MonoBehaviour, IObserverAggroModule
    {
        private IObserverAggro _observerAggro;

        public new string name
        {
            get { return "Target lose aggro over time"; }
        }

        public float intensity = 1f;
        public float startAfterInvisibleTime = 0.5f;
        public float forgetTargetAfterSecondsInvisible = 5f;

        public void Init(IObserverAggro observerAggro)
        {
            _observerAggro = observerAggro;
        }

        public float CalculateAggroForTarget(ISightTarget target)
        {
            var info = target.GetInfoFromObserver(_observerAggro.observer);
            if (info != null && info.lastSeenAt.HasValue)
            {
                var diff = (info.lastSeenAt.Value.time - LosUtility.time);
                var diffAbs = Mathf.Abs(diff);
                if (diffAbs > startAfterInvisibleTime)
                {
                    if (diffAbs > forgetTargetAfterSecondsInvisible)
                    {
                        _observerAggro.ForgetTarget(target);
                    }

                    return diff * intensity;
                }
            }

            return 0f;
        }
    }
}
