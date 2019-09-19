using System;
using System.Collections.Generic;
using Devdog.General;
using UnityEngine;

namespace Devdog.LosPro
{
    [AddComponentMenu(LosPro.AddComponentMenuPath + "Sight/Observer 3D")]
    [HelpURL("http://devdog.io/unity-assets/los-pro/documentation/components/observer")]
    public partial class ObserverBehaviour : MonoBehaviour, IObserver
    {
        private ISight _sight;
        public ISight sight
        {
            get
            {
                if (_sight == null)
                {
                    _sight = new Sight(this, config, cache, new Raycaster3D(), new SightPathExtrapolator(config));
                }

                return _sight;
            }
            protected set { _sight = value; }
        }

        [SerializeField]
        private Transform _eyes;
        public Transform eyes
        {
            get { return _eyes; }
            set { _eyes = value; }
        }

        private SphereCollider _sightTrigger;
        public SphereCollider sightTrigger
        {
            get
            {
                if(_sightTrigger == null)
                    InitSightTrigger();

                return _sightTrigger;
            }
        }

        [SerializeField]
        private SightConfiguration _config = new SightConfiguration();
        public SightConfiguration config
        {
            get { return _config; }
            set { _config = value; }
        }

        private static ISightCache _cache;
        public ISightCache cache
        {
            get
            {
                if (_cache == null)
                {
                    _cache = new SightCache(config);
                }

                return _cache;
            }
        }

        private List<SightTargetInfo> _targetsInRange = new List<SightTargetInfo>();
        public List<SightTargetInfo> targetsInRange
        {
            get { return _targetsInRange; }
            protected set { _targetsInRange = value; }
        }


        protected virtual void Awake()
        {
            InitSightTrigger();
            InvokeRepeating("UpdateSight", UnityEngine.Random.value * config.updateInterval, config.updateInterval);
        }

        protected virtual void OnEnable()
        {
            
        }

        public virtual void InitSightTrigger()
        {
            // _sightTrigger = GetComponentInChildren<SphereCollider>();
            // if (_sightTrigger == null)
            // {
                var obj = new GameObject("_Col");
                obj.transform.SetParent(transform, false);
                _sightTrigger = obj.AddComponent<SphereCollider>();
            // }

            _sightTrigger.isTrigger = true;
            _sightTrigger.radius = config.viewingDistance;
            _sightTrigger.GetOrAddComponent<ObserverTriggerHelperBehaviour>();
            var r = _sightTrigger.GetOrAddComponent<Rigidbody>();
            r.useGravity = false;
            r.isKinematic = true;

            if (LosManager.instance.settings != null)
            {
                _sightTrigger.gameObject.layer = LosManager.instance.settings.sightLayerID;
            }
            else
            {
                DevdogLogger.LogWarning("Settings database not set on LosManager");
            }
        }
        
        public void OnTargetCameIntoRange(SightTargetInfo info)
        {
            targetsInRange.Add(info);
//            Debug.Log("Target came into range of " + name, target.gameObject);
        }

        public void OnTargetWentOutOfRange(SightTargetInfo info)
        {
            targetsInRange.Remove(info);
//            Debug.Log("Target went out of range of " + name, target.gameObject);
        }

        public void OnTargetDestroyed(SightTargetInfo info)
        {
            targetsInRange.RemoveAll(o => o == null);
        }

        public void OnTryingToDetectTarget(SightTargetInfo info)
        {
            
        }

        public void OnDetectingTarget(SightTargetInfo info)
        {
            
        }

        public void OnDetectedTarget(SightTargetInfo info)
        {
            //            DevdogLogger.Log("Target " + info.target.name + " detected by " + name + "\n" + 
            //                "Visible for: " + info.visibleForSeconds + "s" + "\n" + 
            //                "Visible factor: " + (info.visibleFactor * 100)  + "%" + "\n" +
            //                "Detected: " + info.isDetected + "\n" +
            //                "Distance: " + info.distance + "m" + "\n");
        }

        public void OnStopDetectingTarget(SightTargetInfo info)
        {

        }

        public void OnUnDetectedTarget(SightTargetInfo info)
        {
            //            DevdogLogger.Log("Target " + info.target.name + " un detected by " + name + "\n" +
            //                "Visible for: " + info.visibleForSeconds + "s" + "\n" +
            //                "Visible factor: " + (info.visibleFactor * 100) + "%" + "\n" +
            //                "Detected: " + info.isDetected + "\n" +
            //                "Distance: " + info.distance + "m" + "\n");
        }

        protected virtual void UpdateSight()
        {
            if (enabled)
            {
                sight.UpdateAll();
            }
        }
    }
}
