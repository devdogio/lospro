using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Devdog.LosPro
{
    public struct RaycastHit
    {
        public GameObject gameObject { get; set; }
        public Vector3 point { get; set; }
        public Vector3 normal { get; set; }
        public float distance { get; set; }
    }
}
