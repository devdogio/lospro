using System;
using System.Collections.Generic;
using UnityEngine;

namespace Devdog.LosPro
{
    public class SightTargetInfo
    {
        /// <summary>
        /// The target this information belongs to.
        /// </summary>
        public ISightTarget target { get; private set; }

        /// <summary>
        /// The sight object used to detect the target.
        /// Note that the sight belongs to an observer. The observer can be accessed through sight.observer.
        /// </summary>
        public ISight sight { get; private set; }

        /// <summary>
        /// How long has the target been visible? 0 if not visible.
        /// </summary>
        public float visibleForSeconds { get; set; }

        /// <summary>
        /// Is the target detected?
        /// Targets become detected after they've been visible long enough for an observer to detect them.
        /// Detection times can be configured in the configuration classes.
        /// </summary>
        public bool isDetected { get; set; }

        /// <summary>
        /// The place the target was last seen. Null when the target hasn't been seen before.
        /// </summary>
        public SightTargetSampleData? lastSeenAt;

        /// <summary>
        /// The extrapolated (result) samples for this target. Note that the array will be of length 0 if extrapolation is disabled.
        /// </summary>
        public SightTargetSampleData[] extrapolatedSampleData = new SightTargetSampleData[0];

        /// <summary>
        /// How visible is the target. Value ranges from 0 (0%) to 1 (100%) visible.
        /// </summary>
        public float visibleFactor { get; set; }

        /// <summary>
        /// Is the target in the field of view of sight?
        /// </summary>
        public bool isInFOV { get; set; }

        /// <summary>
        /// The distance to the target measured from the observer's location.
        /// </summary>
        public float distance { get; set; }

        public SightTargetInfo(ISightTarget target, ISight sight)
        {
            this.target = target;
            this.sight = sight;
        }

        public void ResetDetection()
        {
            visibleForSeconds = 0f;
            isDetected = false;
        }

    }
}
