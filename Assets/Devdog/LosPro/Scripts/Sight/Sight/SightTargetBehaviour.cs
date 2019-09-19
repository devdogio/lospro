using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Devdog.LosPro
{
    [RequireComponent(typeof(Rigidbody))]
    [AddComponentMenu(LosPro.AddComponentMenuPath + "Sight/Sight Target 3D")]
    [HelpURL("http://devdog.io/unity-assets/los-pro/documentation/components/sight-target")]
    public class SightTargetBehaviour : MonoBehaviour, ISightTarget
    {
        [SerializeField]
        private SightTargetConfiguration _config = new SightTargetConfiguration();
        public SightTargetConfiguration config
        {
            get { return _config; }
            set { _config = value; }
        }

        [SerializeField]
        private SightTargetIndexingConfiguration _indexingConfig = new SightTargetIndexingConfiguration();
        public SightTargetIndexingConfiguration indexingConfig
        {
            get { return _indexingConfig; }
            set { _indexingConfig = value; }
        }


        protected virtual void Awake()
        {
           
        }

        protected void OnEnable()
        {
            
        }

        public void OnObserverTryingToDetect(SightTargetInfo sightInfo)
        {
            
        }

        public void OnGettingDetected(SightTargetInfo info)
        {
            //            DevdogLogger.Log("<color=blue>Getting detected by</color> " + bySight.name + "\nVisisble factor: " + (info.visibleForSeconds / info.target.config.detectionTime), gameObject);
        }

        public void OnStopGettingDetected(SightTargetInfo info)
        {
            //            DevdogLogger.Log("<color=blue>Stopped getting detected by</color> " + bySight.name + "\nVisisble factor: " + (info.visibleForSeconds / info.target.config.detectionTime), gameObject);
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