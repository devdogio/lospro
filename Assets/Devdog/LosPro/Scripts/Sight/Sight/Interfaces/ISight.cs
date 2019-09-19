using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Devdog.LosPro
{
    public interface ISight
    {
        SightConfiguration config { get; set; }
        IObserver observer { get; }
        
        ISightCache cache { get; set; }
        ISightRaycaster raycaster { get; set; }
        ISightPathExtrapolator extrapolator { get; set; }

        /// <summary>
        /// A list of predicates that can be used to verify if an object is detectable of not. 
        /// <remarks>NOTE: This list is NULL by default to prevent unnecesarry allocations on each ISight</remarks>
        /// </summary>
        List<Predicate<SightTargetInfo>> canDetectPredicates { get; set; }


        #region Unity MonoBehaviour

        string name { get; }
        GameObject gameObject { get; }
        Transform transform { get; }

        #endregion

        bool IsTargetInRange(ISightTarget target);
        bool IsTargetInFOV(ISightTarget target);
        bool IsTargetVisible(ISightTarget target);
        SightTargetInfo GetInRangeTargetInfo(ISightTarget target);

        void ForgetTarget(ISightTarget target);
        void DetectTarget(ISightTarget target);


        void UpdateAll(bool updateInactiveTargets = false);
        void Update(SightTargetInfo sightInfo);

        void OnTriggerEnter(GameObject obj);
        void OnTriggerExit(GameObject obj);
    }
}
