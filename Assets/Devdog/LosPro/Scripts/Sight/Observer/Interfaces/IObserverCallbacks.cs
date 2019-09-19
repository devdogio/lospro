using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Devdog.LosPro
{
    public interface IObserverCallbacks
    {
        void OnTargetCameIntoRange(SightTargetInfo info);
        void OnTargetWentOutOfRange(SightTargetInfo info);

        void OnTargetDestroyed(SightTargetInfo info);

        void OnTryingToDetectTarget(SightTargetInfo info);
        void OnDetectingTarget(SightTargetInfo info);
        void OnDetectedTarget(SightTargetInfo info);
        void OnStopDetectingTarget(SightTargetInfo info);
        void OnUnDetectedTarget(SightTargetInfo info);
    }
}
