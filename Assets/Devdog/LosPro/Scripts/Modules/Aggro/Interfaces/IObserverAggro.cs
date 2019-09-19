using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Devdog.LosPro
{
    public interface IObserverAggro
    {
        Vector3 startLocation { get; }
        float currentAggro { get; set; }

        IEnumerable<IObserverAggroModule> aggroModules { get; }
        IObserver observer { get; }

        [Obsolete]
        void AddAggroToTarget(ISightTarget target, float addAmount);

        void ChangeAggroForTarget(ISightTarget target, float changeAmount);
        void SetAggroForTarget(ISightTarget target, float amount);

        void ForgetTarget(ISightTarget target);


        SightTargetAggroInfo GetAggroForTarget(ISightTarget target);
        SightTargetAggroInfo GetTargetWithHighestAggro();
    }
}