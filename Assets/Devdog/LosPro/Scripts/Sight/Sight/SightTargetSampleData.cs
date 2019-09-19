using System;
using System.Collections.Generic;
using UnityEngine;

namespace Devdog.LosPro
{
    /// <summary>
    /// Represents a single sample taken from a sight target.
    /// </summary>
    public struct SightTargetSampleData : IEquatable<SightTargetSampleData>
    {

        public Vector3 position { get; set; }
        public Quaternion rotation { get; set; }

        public float time { get; set; }



        public bool Equals(SightTargetSampleData other)
        {
            return other.position == position &&
                   other.rotation == rotation &&
                   Mathf.Approximately(other.time, time);
        }

        public override string ToString()
        {
            return position + " - " + time;
        }
    }
}
