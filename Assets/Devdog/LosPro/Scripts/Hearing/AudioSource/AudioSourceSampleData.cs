using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Devdog.LosPro
{
    public struct AudioSourceSampleData : IEquatable<AudioSourceSampleData>
    {
        public Vector3 position { get; set; }
        public float time { get; set; }

        public bool Equals(AudioSourceSampleData other)
        {
            return other.position == position && Mathf.Approximately(other.time, time);
        }
    }
}
