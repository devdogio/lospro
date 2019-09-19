using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Devdog.LosPro
{
    public interface IObserverAggroModule
    {
        string name { get; }
        bool enabled { get; set; }

        void Init(IObserverAggro observerAggro);


        float CalculateAggroForTarget(ISightTarget target);

    }
}
