using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Devdog.LosPro
{
#if UNITY_EDITOR

    [RequireInterface(typeof(ISightTarget))]
    [AddComponentMenu(LosPro.AddComponentMenuPath + "Sight/Sight Target Debugger", 99)]
    public class SightTargetDebugger : MonoBehaviour, ISightTargetCallbacks
    {

        public bool isVisible
        {
            get { return visibleBy.Count > 0; }
        }

        public readonly List<ISight> visibleBy = new List<ISight>();

        private Renderer _renderer;
        private Color _color = _red;
        private static Color _red
        {
            get { return new Color(1, 0, 0, 0.3f); }
        }

        private static Color _green
        {
            get { return new Color(0, 1, 0, 0.3f); }
        }

        protected virtual void Awake()
        {
            var target = GetComponent<ISightTarget>();
            Assert.IsNotNull(target, "Debuggers can only be used in combination with the actual implementation");
        }

        protected void OnEnable()
        {

        }

        private void OnDrawGizmos()
        {
            if (isActiveAndEnabled)
            {
                Gizmos.color = _color;
                Gizmos.DrawSphere(transform.position, 1);
                Gizmos.color = Color.white;
            }
        }

        public void OnObserverTryingToDetect(SightTargetInfo sightInfo)
        {
        }

        public void OnGettingDetected(SightTargetInfo sightInfo)
        {
        }

        public void OnStopGettingDetected(SightTargetInfo sightInfo)
        {
        }

        public void OnDetectedByObserver(SightTargetInfo sightInfo)
        {
            visibleBy.Add(sightInfo.sight);
            _color = _green;

            Debug.Log("<color=green>Got detected by</color> " + sightInfo.sight.name, gameObject);
        }

        public void OnUnDetectedByObserver(SightTargetInfo sightInfo)
        {
            visibleBy.Remove(sightInfo.sight);

            if (isVisible == false)
            {
                _color = _red;
                Debug.Log("<color=red>Became undetected by all</color>", gameObject);
                return;
            }

            Debug.Log("<color=red>Became un detected by</color> " + sightInfo.sight.name, gameObject);
        }

        public void OnCameIntoObserverRange(SightTargetInfo sightInfo)
        {
        }

        public void OnWentOutOffObserverRange(SightTargetInfo sightInfo)
        {
        }

    }

#endif
}
