using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Devdog.LosPro
{
    public interface ISightTargetCallbacks
    {
        /// <summary>
        /// The observer is trying to detect the target. Minimal visibility factor isn't met yet.
        /// </summary>
        void OnObserverTryingToDetect(SightTargetInfo sightInfo);

        /// <summary>
        /// Getting detected, the target is visible enough according to the configration's min visibility.
        /// </summary>
        void OnGettingDetected(SightTargetInfo sightInfo);

        /// <summary>
        /// Detection has stopped.
        /// </summary>
        void OnStopGettingDetected(SightTargetInfo sightInfo);

        /// <summary>
        /// Called whenever this target becomes detected.
        /// </summary>
        void OnDetectedByObserver(SightTargetInfo sightInfo);

        /// <summary>
        /// Called whenever this target becomes un detected.
        /// </summary>
        void OnUnDetectedByObserver(SightTargetInfo sightInfo);

        /// <summary>
        /// Came into range of an observer (sightInfo.sight.observer).
        /// Note that the observer isn't necessarily looking in our derection or detecting us.
        /// </summary>
        void OnCameIntoObserverRange(SightTargetInfo sightInfo);

        /// <summary>
        /// Went out of observerer's range. (Observer can't possibly detect us now, unless we get back in range).
        /// </summary>
        void OnWentOutOffObserverRange(SightTargetInfo sightInfo);
    }
}
