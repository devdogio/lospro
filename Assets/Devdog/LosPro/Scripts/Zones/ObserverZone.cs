using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace Devdog.LosPro
{
    [AddComponentMenu(LosPro.ProductName + "Zones/Observer zone 3D")]
    public class ObserverZone : AffectorZoneBase<IObserver, SightConfiguration>
    {
        public SightConfiguration sightConfig = new SightConfiguration();

        protected override void ObjectEnteredTrigger(IObserver observer)
        {
            overrideValues.Add(new OverrideValue<IObserver, SightConfiguration>()
            {
                obj = observer,
                prevValue = observer.config
            });

            observer.config = sightConfig;
        }

        protected override void ObjectExitedTrigger(IObserver observer)
        {
            for (int i = overrideValues.Count - 1; i >= 0; i--)
            {
                if (overrideValues[i].obj == observer)
                {
                    observer.config = overrideValues[i].prevValue;
                    overrideValues.RemoveAt(i);
                    break;
                }
            }
        }
    }
}