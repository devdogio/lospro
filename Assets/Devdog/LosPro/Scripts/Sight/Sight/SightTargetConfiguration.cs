using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Devdog.LosPro
{
    [System.Serializable]
    public partial class SightTargetConfiguration
    {
        [TargetCategory]
        public int category = -1;
        
        /// <summary>
        /// How long does it take to detect this target?
        /// </summary>
        public float detectionTime = 0.5f;

        /// <summary>
        /// A multiplier that can be used to make a sighttarget harder to detect.
        /// Example:
        /// IObserver has a minSightFactor of 0.2 (targets have to be at least 20% visible to detect them)
        /// When using a minVisibleMultiplier of 2 this target has to be at least 40% visible.
        /// </summary>
        [Tooltip(@"A multiplier that can be used to make a sighttarget harder to detect.\n
                Example:\n
                IObserver has a minSightFactor of 0.2 (targets have to be at least 20% visible to detect them)\n
                When using a minVisibleMultiplier of 2 this target has to be at least 40% visible.")]
        public float minVisibilityMultiplier = 1f;


        /// <summary>
        /// The multiplier used to define how visible the target is.
        /// </summary>
        public float visibilityMultiplier = 1f;

        /// <summary>
        /// Can this target be detected instantly? If more than 90% of the rays hit the target CAN be detected instantly.
        /// </summary>
        public bool canInstantlyBeDetected = true;

        /// <summary>
        /// Debug mode
        /// </summary>
        public bool debug = false;
    }
}
