using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Devdog.LosPro
{
    [System.Serializable]
    public partial class SightConfiguration
    {
        [TargetCategoriesMask]
        public int sightTargetCategoriesMask = -1;

        public LayerMask raycastLayer = -1;

        [Range(0.1f, 5f)]
        public float updateInterval = 0.2f;

        /// <summary>
        /// The minimal dot value required to pass the field of view test.
        /// </summary>
        [Range(-1f, 1f)]
        public float fieldOfViewDotValue = 0.2f;
        public float fieldOfViewDegrees
        {
            get
            {
                return Mathf.Acos(fieldOfViewDotValue) * Mathf.Rad2Deg;
            }
        }

        public float viewingDistance = 40f;
        public int sampleCount = 10;

        [Range(0f, 1f)]
        public float minVisibleFactor = 0.2f;
        public bool instantlyDetectWhenTargetIsFullyVisible = true;


        public bool extrapolatePath = true;
        public int extrapolateSampleCount = 10;
//        [Range(0f, 10f)]
//        public float extrapolateRandomNoiseFactor = 1f;

        public string onlyWithTag = ""; // SightTarget
        //public bool updateOneAtATime = false;
        public bool debug = false;


        public SightConfiguration()
        {
            
        }
    }
}
