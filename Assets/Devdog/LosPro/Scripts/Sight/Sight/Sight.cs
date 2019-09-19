using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Assertions;

namespace Devdog.LosPro
{
    public class Sight : ISight
    {
        public IObserver observer { get; protected set; }

        public string name
        {
            get { return gameObject.name; }
        }
        public GameObject gameObject
        {
            get { return transform.gameObject; }
        }
        public Transform transform
        {
            get { return observer.eyes; }
        }


        public SightConfiguration config { get; set; }
        public ISightCache cache { get; set; }
        public ISightRaycaster raycaster { get; set; }
        public ISightPathExtrapolator extrapolator { get; set; }


        public List<SightTargetInfo> targetsInRange { get; protected set; }
        public List<Predicate<SightTargetInfo>> canDetectPredicates { get; set; }


        private static List<IObserverCallbacks> _observerCallbacksList = new List<IObserverCallbacks>();
        private static List<ISightTargetCallbacks> _sightTargetCallbacksList = new List<ISightTargetCallbacks>();

        public Sight(IObserver observer, SightConfiguration config)
            : this(observer, config, new SightCache(config), new Raycaster3D(), new SightPathExtrapolator(config))
        { }

        public Sight(IObserver observer, SightConfiguration config, ISightCache cache)
            : this(observer, config, cache, new Raycaster3D(), new SightPathExtrapolator(config))
        { }

        public Sight(IObserver observer, SightConfiguration config, ISightCache cache, ISightRaycaster raycaster, ISightPathExtrapolator extrapolator)
        {
            Assert.IsNotNull(observer);
            Assert.IsNotNull(config);
//            Assert.IsNotNull(cache); // Cache can be null IF all targets use manual indexing.
            Assert.IsNotNull(raycaster);
            if (config.extrapolatePath)
            {
                Assert.IsNotNull(extrapolator, "Use extrapolate path is enabled, but no ISightPathExtrapolator implementation given.");
            }

            this.observer = observer;
            this.config = config;
            this.cache = cache;
            this.raycaster = raycaster;
            this.extrapolator = extrapolator;
            
            targetsInRange = new List<SightTargetInfo>(16);
        }

        public virtual bool IsTargetInFOV(ISightTarget target)
        {
            return Vector3.Dot(transform.forward, Vector3.Normalize(target.transform.position - transform.position)) > config.fieldOfViewDotValue;
        }

        public bool IsTargetVisible(ISightTarget target)
        {
            var info = GetInRangeTargetInfo(target);
            return info != null && info.isDetected;
        }

        public bool IsTargetInRange(ISightTarget target)
        {
            return GetInRangeTargetInfo(target) != null;
        }

        public SightTargetInfo GetInRangeTargetInfo(ISightTarget target)
        {
            // NOTE: FirstOrDefault generates GC - So manual loop
            foreach (var t in targetsInRange)
            {
                if (t.target == target)
                    return t;
            }

            return null;
        }

        public void ForgetTarget(ISightTarget target)
        {
            var info = GetInRangeTargetInfo(target);
            if (info != null)
            {
                targetsInRange.Remove(info);
            }
        }

        private void RemoveAllDestroyedTargets()
        {
            // targetsInRange.RemoveAll(o => o.target.isDestroyed);
            for (int i = targetsInRange.Count - 1; i >= 0; i--)
            {
                if (targetsInRange[i].target.IsDestroyed())
                {
                    TargetDestroyed(targetsInRange[i]);
                    targetsInRange.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Update all targets that are in range. 
        /// </summary>
        public void UpdateAll(bool updateInactiveTargets = false)
        {
            RemoveAllDestroyedTargets();

            for (int i = targetsInRange.Count - 1; i >= 0; i--)
            {
                var targetInfo = targetsInRange[i];
                if (string.IsNullOrEmpty(config.onlyWithTag) == false)
                {
                    if (targetInfo.target.gameObject.CompareTag(config.onlyWithTag) == false)
                    {
                        continue;
                    }
                }

                if (updateInactiveTargets == false && targetInfo.target.gameObject.activeInHierarchy == false)
                {
                    continue;
                }

                Update(targetInfo);
            }
        }

        public void Update(SightTargetInfo sightInfo)
        {
            Assert.IsTrue(sightInfo.target.IsDestroyed() == false, "Null object as target given, this is not allowed!"); // Note: This doesn't work because of the interface implementation, managed heap object remains, unmanaged code is destroyed.
            Assert.IsTrue(sightInfo.sight == this, "SightTargetInfo given that doesn't belong to this sight.");
            if (sightInfo.target.enabled == false)
            {
                return;
            }

            sightInfo.isInFOV = IsTargetInFOV(sightInfo.target);

            float hitFactor = 0f;
            if (sightInfo.isInFOV)
            {
                switch (sightInfo.target.indexingConfig.indexingType)
                {
                    case TargetIndexingType.Manual:
                        hitFactor = DoManualRaycasting(sightInfo);

                        break;
                    case TargetIndexingType.Automatic:
                        hitFactor = DoAutomaticRaycasting(sightInfo);

                        break;
                    default:
                        throw new ArgumentOutOfRangeException(typeof(TargetIndexingType).Name + " type not found (index: " + sightInfo.target.indexingConfig.indexingType + ")");
                }
            }


            sightInfo.distance = Vector3.Distance(transform.position, sightInfo.target.transform.position);
            sightInfo.visibleFactor = Mathf.Clamp01(hitFactor * sightInfo.target.config.visibilityMultiplier);
            
            if (sightInfo.isDetected == false)
            {
                if (hitFactor > 0f)
                {
                    TryToDetectTarget(sightInfo);
                }
            }

            // Enough hits to detect / see target
            if (hitFactor >= GetMinVisibilityFactor(sightInfo.target))
            {
                sightInfo.visibleForSeconds += config.updateInterval;
                GetCurrentPositionSample(sightInfo, out sightInfo.lastSeenAt);

                if (config.extrapolatePath)
                {
                    extrapolator.TakeSample(sightInfo.target);
                }

                if (sightInfo.isDetected == false)
                {
                    DetectingTarget(sightInfo);
                    if (sightInfo.visibleForSeconds >= sightInfo.target.config.detectionTime)
                    {
                        TargetDetected(sightInfo);
                    }
                }
            }
            else
            {
                // Not visible from current samples taken. Still marked visible
                if (sightInfo.isDetected)
                {
                    TargetUnDetected(sightInfo);
                }

                if (sightInfo.visibleForSeconds > 0f)
                {
                    StopDetectingTarget(sightInfo);
                }

                sightInfo.ResetDetection();
            }
        }

        private float DoAutomaticRaycasting(SightTargetInfo sightInfo)
        {
            float hits = 0f;
            float hitFactor = 0f;

            if (cache.Contains(sightInfo.target) == false)
            {
                cache.GenerateCache(sightInfo.target);
            }

            // The object to cast the line to.
            SightCacheLookup cacheLookup;
            cache.GetFromCache(sightInfo.target, out cacheLookup);

            var sampleCount = Mathf.Min(config.sampleCount, cacheLookup.vertexCount);
            for (int i = 0; i < sampleCount; i++)
            {
                var raycastToPosition = cacheLookup.GetVertexPosition(i);
                bool hasEnoughSamples = DoRaycast(raycastToPosition, sightInfo, ref hits, ref hitFactor, sampleCount);
                if (hasEnoughSamples)
                {
                    break;
                }

                //if (config.updateOneAtATime)
                //{
                //    _lastIndexUpdateOneAtATime++;
                //    _lastIndexUpdateOneAtATime = _lastIndexUpdateOneAtATime % cacheLookup.vertexCount;
                //    break;
                //}
            }

            return hitFactor;
        }

        private float DoManualRaycasting(SightTargetInfo sightInfo)
        {
            float hits = 0;
            float hitFactor = 0f;

            foreach (Transform t in sightInfo.target.indexingConfig.raycastPoints)
            {
                var raycastToPosition = t.position;
                bool hasEnoughSamples = DoRaycast(raycastToPosition, sightInfo, ref hits, ref hitFactor, sightInfo.target.indexingConfig.raycastPoints.Length);
                if (hasEnoughSamples)
                {
                    break;
                }
            }

            return hitFactor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Returns True when the raycasting is all done. False when more raycasts are required.</returns>
        private bool DoRaycast(Vector3 raycastToPosition, SightTargetInfo sightInfo, ref float hits, ref float hitFactor, int sampleCount)
        {
            RaycastHit hitInfo;
            bool hit = raycaster.Linecast(gameObject.transform.position, raycastToPosition, out hitInfo, config.raycastLayer);
            // If nothing was hit the object is visible.
            if (hit == false)
            {
                hitInfo.gameObject = sightInfo.target.gameObject;
                hitInfo.point = raycastToPosition;
                hitInfo.normal = Vector3.zero;
            }

#if UNITY_EDITOR
            if (config.debug)
            {
                LosDebugUtility.DrawDebugLine(gameObject, hitInfo.gameObject, sightInfo.target.gameObject, gameObject.transform.position, hitInfo.point, raycastToPosition, config.updateInterval);
            }
#endif

            // If we did hit something, is it the target?
            if (LosUtility.IsValidTarget(sightInfo.target.gameObject, hitInfo.gameObject) || hit == false)
            {
                hits++;
                hitFactor = hits/sampleCount;

                // If the sight is already detected we can bail early
                if (hitFactor >= GetMinVisibilityFactor(sightInfo.target) && sightInfo.isDetected)
                {
                    return true;
//                    break; // Object is already visible no need to keep casting
                }
            }

            return false;
        }


        public float GetMinVisibilityFactor(ISightTarget target)
        {
            return Mathf.Min(config.minVisibleFactor * target.config.minVisibilityMultiplier, 0.85f);
        }

        /// <summary>
        /// Can the target be detected with the current sight info?
        /// </summary>
        /// <param name="sightInfo"></param>
        /// <returns>Return true if the target can be detected, return false if the target can not be detected.</returns>
        protected virtual bool CanDetectTarget(SightTargetInfo sightInfo)
        {
            if (canDetectPredicates != null)
            {
                foreach (var predicate in canDetectPredicates)
                {
                    if (predicate(sightInfo) == false)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public virtual void TryToDetectTarget(SightTargetInfo sightInfo)
        {
            if (CanDetectTarget(sightInfo) == false)
            {
                return;
            }

            observer.gameObject.GetComponents(_observerCallbacksList);
            foreach (var callback in _observerCallbacksList)
            {
                callback.OnTryingToDetectTarget(sightInfo);
            }

            sightInfo.target.gameObject.GetComponents(_sightTargetCallbacksList);
            foreach (var callback in _sightTargetCallbacksList)
            {
                callback.OnObserverTryingToDetect(sightInfo);
            }

            if (sightInfo.visibleFactor >= 0.9f && config.instantlyDetectWhenTargetIsFullyVisible && sightInfo.target.config.canInstantlyBeDetected)
            {
                TargetDetected(sightInfo);
            }
        }

        /// <summary>
        /// Detect the given target instantly. The object will have to be in range, otherwise the call is ignored.
        /// </summary>
        public void DetectTarget(ISightTarget target)
        {
            var info = GetInRangeTargetInfo(target);
            if(info != null)
            {
                TargetDetected(info);
            }
        }

        private void TargetDetected(SightTargetInfo sightInfo)
        {
            if (CanDetectTarget(sightInfo) == false)
            {
                return;
            }

            sightInfo.isDetected = true; // Incase not updating anymore, set manually
            sightInfo.extrapolatedSampleData = null; // Clear data here...

            observer.gameObject.GetComponents(_observerCallbacksList);
            foreach (var callback in _observerCallbacksList)
            {
                callback.OnDetectedTarget(sightInfo);
            }

            sightInfo.target.gameObject.GetComponents(_sightTargetCallbacksList);
            foreach (var callback in _sightTargetCallbacksList)
            {
                callback.OnDetectedByObserver(sightInfo);
            }
        }

        private void DetectingTarget(SightTargetInfo sightInfo)
        {
            if (CanDetectTarget(sightInfo) == false)
            {
                return;
            }

            observer.gameObject.GetComponents(_observerCallbacksList);
            foreach (var callback in _observerCallbacksList)
            {
                callback.OnDetectingTarget(sightInfo);
            }

            sightInfo.target.gameObject.GetComponents(_sightTargetCallbacksList);
            foreach (var callback in _sightTargetCallbacksList)
            {
                callback.OnGettingDetected(sightInfo);
            }
        }

        private void StopDetectingTarget(SightTargetInfo sightInfo)
        {
            observer.gameObject.GetComponents(_observerCallbacksList);
            foreach (var callback in _observerCallbacksList)
            {
                callback.OnStopDetectingTarget(sightInfo);
            }

            sightInfo.target.gameObject.GetComponents(_sightTargetCallbacksList);
            foreach (var callback in _sightTargetCallbacksList)
            {
                callback.OnStopGettingDetected(sightInfo);
            }
        }

        private void TargetUnDetected(SightTargetInfo sightInfo)
        {
            sightInfo.isInFOV = false; // Incase not updating anymore, set manually
//            GetCurrentPositionSample(sightInfo, out sightInfo.lastSeenAt);

            if (config.extrapolatePath)
            {
                extrapolator.ExtrapolatePath(sightInfo.target, 1f, out sightInfo.extrapolatedSampleData);
            }

            observer.gameObject.GetComponents(_observerCallbacksList);
            foreach (var callback in _observerCallbacksList)
            {
                callback.OnUnDetectedTarget(sightInfo);
            }

            sightInfo.target.gameObject.GetComponents(_sightTargetCallbacksList);
            foreach (var callback in _sightTargetCallbacksList)
            {
                callback.OnUnDetectedByObserver(sightInfo);
            }

            extrapolator.ClearSamplesForTarget(sightInfo.target);
            sightInfo.ResetDetection();
        }

        private void GetCurrentPositionSample(SightTargetInfo sightInfo, out SightTargetSampleData? sample)
        {
            sample = new SightTargetSampleData()
            {
                position = sightInfo.target.transform.position,
                rotation = sightInfo.target.transform.rotation,
                time = LosUtility.time
            };
        }

        public void OnTriggerEnter(GameObject obj)
        {
            var target = obj.GetComponentInParent<ISightTarget>();
            if (target != null && GetInRangeTargetInfo(target) == null)
            {
                if (((target.config.category & config.sightTargetCategoriesMask) != 0) || target.config.category == 0)
                {
                    TargetCameIntoRange(target);
                }
            }
        }

        private void TargetCameIntoRange(ISightTarget target)
        {
            var sightInfo = new SightTargetInfo(target, this);
            targetsInRange.Add(sightInfo);


            observer.gameObject.GetComponents(_observerCallbacksList);
            foreach (var callback in _observerCallbacksList)
            {
                callback.OnTargetCameIntoRange(sightInfo);
            }

            sightInfo.target.gameObject.GetComponents(_sightTargetCallbacksList);
            foreach (var callback in _sightTargetCallbacksList)
            {
                callback.OnCameIntoObserverRange(sightInfo);
            }
        }

        public void OnTriggerExit(GameObject obj)
        {
            var target = obj.GetComponentInParent<ISightTarget>();
            if (target != null)
            {
                var targetInfo = GetInRangeTargetInfo(target);
                if (targetInfo == null)
                {
                    return;
                }

                TargetWentOutOfRange(targetInfo);
            }
        }

        private void TargetWentOutOfRange(SightTargetInfo sightInfo)
        {
            targetsInRange.Remove(sightInfo);
            
            if (sightInfo.isDetected)
            {
                TargetUnDetected(sightInfo);
            }
            else if (sightInfo.visibleForSeconds > 0f)
            {
                StopDetectingTarget(sightInfo);
            }

            sightInfo.visibleFactor = 0f;
            observer.gameObject.GetComponents(_observerCallbacksList);
            foreach (var callback in _observerCallbacksList)
            {
                callback.OnTargetWentOutOfRange(sightInfo);
            }

            sightInfo.target.gameObject.GetComponents(_sightTargetCallbacksList);
            foreach (var callback in _sightTargetCallbacksList)
            {
                callback.OnWentOutOffObserverRange(sightInfo);
            }

        }

        private void TargetDestroyed(SightTargetInfo sightInfo)
        {
            observer.gameObject.GetComponents(_observerCallbacksList);
            foreach (var callback in _observerCallbacksList)
            {
                callback.OnTargetDestroyed(sightInfo);
            }
        }
    }
}