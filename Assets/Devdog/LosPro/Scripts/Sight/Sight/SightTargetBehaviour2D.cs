using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Devdog.LosPro
{
    [RequireComponent(typeof(Rigidbody2D))]
    [AddComponentMenu(LosPro.AddComponentMenuPath + "Sight/Sight Target 2D")]
    [HelpURL("http://devdog.io/unity-assets/los-pro/documentation/components/sight-target")]
    public class SightTargetBehaviour2D : MonoBehaviour, ISightTarget
    {
        [SerializeField]
        private SightTargetConfiguration _config;
        public SightTargetConfiguration config
        {
            get { return _config; }
            set { _config = value; }
        }

        [SerializeField]
        private SightTargetIndexingConfiguration _indexingConfig;
        public SightTargetIndexingConfiguration indexingConfig
        {
            get { return _indexingConfig; }
            set { _indexingConfig = value; }
        }

        protected virtual void Awake()
        {

        }

        public void OnObserverTryingToDetect(SightTargetInfo sightInfo)
        {
            
        }

        public void OnGettingDetected(SightTargetInfo info)
        {
//            Debug.Log("<color=blue>Getting detected by</color> " + bySight.name + "\nVisisble factor: " + (info.visibleForSeconds / info.target.config.detectionTime), gameObject);
        }

        public void OnStopGettingDetected(SightTargetInfo info)
        {
//            Debug.Log("<color=blue>Stopped getting detected by</color> " + bySight.name + "\nVisisble factor: " + (info.visibleForSeconds / info.target.config.detectionTime), gameObject);
        }

        public void OnDetectedByObserver(SightTargetInfo info)
        {

        }

        public void OnUnDetectedByObserver(SightTargetInfo info)
        {

        }

        public void OnCameIntoObserverRange(SightTargetInfo sightInfo)
        {
            
        }

        public void OnWentOutOffObserverRange(SightTargetInfo sightInfo)
        {

        }

        protected void OnDestroy()
        {

        }
    }
}