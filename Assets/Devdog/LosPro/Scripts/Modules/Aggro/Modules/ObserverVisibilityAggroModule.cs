using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Assertions;

namespace Devdog.LosPro
{
    public class ObserverVisibilityAggroModule : MonoBehaviour, IObserverAggroModule
    {
        private IObserverAggro _observerAggro;

        public new string name
        {
            get { return "Target visibility factor"; }
        }
        public float intensity = 1f;

        public void Init(IObserverAggro observerAggro)
        {
            _observerAggro = observerAggro;
        }

        public float CalculateAggroForTarget(ISightTarget target)
        {
            var info = target.GetInfoFromObserver(_observerAggro.observer);
            if (info != null)
            {
                return info.visibleFactor * intensity;
            }

            return 0f;
        }
    }
}
