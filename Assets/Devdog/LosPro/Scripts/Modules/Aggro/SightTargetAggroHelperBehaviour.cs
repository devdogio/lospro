using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Devdog.LosPro
{
    /// <summary>
    /// This component keeps track of which observers are in range and allows for easy aggro control, such as RaiseAggro.
    /// This component is optional!
    /// </summary>
    [RequireInterface(typeof(ISightTarget))]
    public class SightTargetAggroHelperBehaviour : MonoBehaviour, ISightTargetCallbacks
    {
        private List<IObserverAggro> _inRange = new List<IObserverAggro>();

        private ISightTarget _target;
        public ISightTarget target
        {
            get
            {
                if (_target == null)
                {
                    _target = GetComponent<ISightTarget>();
                }

                return _target;
            }
        }

        public void ChangeAggro(float amount)
        {
            foreach (var observer in _inRange)
            {
                observer.ChangeAggroForTarget(target, amount);
            }
        }

        public void OnCameIntoObserverRange(SightTargetInfo sightInfo)
        {
            _inRange.Add(sightInfo.sight.observer.gameObject.GetComponent<IObserverAggro>());
        }

        public void OnWentOutOffObserverRange(SightTargetInfo sightInfo)
        {
            _inRange.Remove(sightInfo.sight.observer.gameObject.GetComponent<IObserverAggro>());
        }

        public void OnObserverTryingToDetect(SightTargetInfo sightInfo) { }
        public void OnGettingDetected(SightTargetInfo sightInfo) { }
        public void OnStopGettingDetected(SightTargetInfo sightInfo) { }
        public void OnDetectedByObserver(SightTargetInfo sightInfo) { }
        public void OnUnDetectedByObserver(SightTargetInfo sightInfo) { }
    }
}