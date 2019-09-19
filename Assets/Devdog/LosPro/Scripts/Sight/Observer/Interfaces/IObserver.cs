using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Devdog.LosPro
{
    public interface IObserver : IObserverCallbacks
    {
        string name { get; }
        GameObject gameObject { get; }
        Transform transform { get; }

        SightConfiguration config { get; set; }
        Transform eyes { get; set; }
        List<SightTargetInfo> targetsInRange { get; }

        ISight sight { get; }
    }
}
