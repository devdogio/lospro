using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Devdog.LosPro
{
    public interface IObserverAggroCallbacks
    {
        void OnTargetWithHighestAggroChanged(ISightTarget target, float aggro);
    }
}