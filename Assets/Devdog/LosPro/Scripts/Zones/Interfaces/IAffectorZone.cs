using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Devdog.LosPro
{
    public interface IAffectorZone
    {
        GameObject gameObject { get; }
        Transform transform { get; }
        SphereCollider sphereCollider { get; }
    }
}
