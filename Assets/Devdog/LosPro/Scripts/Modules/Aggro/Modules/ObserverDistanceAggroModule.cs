using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Assertions;

namespace Devdog.LosPro
{
    public class ObserverDistanceAggroModule : MonoBehaviour, IObserverAggroModule
    {
        private IObserverAggro _observerAggro;

        public new string name
        {
            get { return "Target distance"; }
        }
        public float intensity = 1f;
        public float loseAggroWhenOutOfRangePerSecond = 1f;

//        private float _effect = 0f;

        public void Init(IObserverAggro observerAggro)
        {
            _observerAggro = observerAggro;
        }

        public float CalculateAggroForTarget(ISightTarget target)
        {
            var info = target.GetInfoFromObserver(_observerAggro.observer);
            if (info != null)
            {
                return (_observerAggro.observer.config.viewingDistance - info.distance) / _observerAggro.observer.config.viewingDistance * intensity;
            }

            return 0f;
        }

//        private void Update()
//        {
//            float effectPerFrame = intensity * Time.deltaTime;
//            _effect -= effectPerFrame;
//        }
    }
}
