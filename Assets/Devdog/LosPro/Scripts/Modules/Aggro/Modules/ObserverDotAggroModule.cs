using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Assertions;

namespace Devdog.LosPro
{
    public class ObserverDotAggroModule : MonoBehaviour, IObserverAggroModule
    {
        private IObserverAggro _observerAggro;

        public new string name
        {
            get { return "Target dot"; }
        }
        public float intensity = 1f;

        public void Init(IObserverAggro observerAggro)
        {
            _observerAggro = observerAggro;
        }

        public float CalculateAggroForTarget(ISightTarget target)
        {
            var info = target.GetInfoFromObserver(_observerAggro.observer); // Safe to assume info is available here, considering we're getting the update call.
            if (info == null || info.lastSeenAt.HasValue == false)
            {
                return 0f;
            }

            return Vector3.Dot(_observerAggro.observer.transform.forward, Vector3.Normalize(target.transform.position - _observerAggro.observer.transform.position)) * intensity;
        }
    }
}
