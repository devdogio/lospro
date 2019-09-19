using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Assertions;

namespace Devdog.LosPro
{
    public class ObserverTimeAggroModule : MonoBehaviour, IObserverAggroModule
    {
        private IObserverAggro _observerAggro;

        public new string name
        {
            get { return "Target visible time"; }
        }
        public float intensity = 1f;
//        public float forgetAfterTime = 3f;
        public float maxTime = 10f;

        public void Init(IObserverAggro observerAggro)
        {
            _observerAggro = observerAggro;
        }

        public float CalculateAggroForTarget(ISightTarget target)
        {
            var info = target.GetInfoFromObserver(_observerAggro.observer);
            if (info != null)
            {
                return Mathf.Clamp(info.visibleForSeconds, 0f, maxTime) * intensity;
            }

            return 0f;
        }
    }
}
