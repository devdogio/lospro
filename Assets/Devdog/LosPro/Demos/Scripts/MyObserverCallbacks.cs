using UnityEngine;
using System.Collections;

namespace Devdog.LosPro.Demo
{
    public class MyObserverCallbacks : MonoBehaviour, IObserverCallbacks
    {
        public void OnTargetCameIntoRange(SightTargetInfo info)
        {
        }

        public void OnTargetWentOutOfRange(SightTargetInfo info)
        {
        }

        public void OnTargetDestroyed(SightTargetInfo info)
        {
        }

        public void OnTryingToDetectTarget(SightTargetInfo info)
        {
        }

        public void OnDetectingTarget(SightTargetInfo info)
        {
        }

        public void OnDetectedTarget(SightTargetInfo info)
        {
        }

        public void OnStopDetectingTarget(SightTargetInfo info)
        {
        }

        public void OnUnDetectedTarget(SightTargetInfo info)
        {
        }
    }
}