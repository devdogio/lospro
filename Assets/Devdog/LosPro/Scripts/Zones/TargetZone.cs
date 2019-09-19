using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace Devdog.LosPro
{
    [AddComponentMenu(LosPro.ProductName + "Zones/Target zone 3D")]
    public class TargetZone : AffectorZoneBase<ISightTarget, SightTargetConfiguration>
    {
        public SightTargetConfiguration targetConfig = new SightTargetConfiguration();

        protected override void ObjectEnteredTrigger(ISightTarget observer)
        {
            overrideValues.Add(new OverrideValue<ISightTarget, SightTargetConfiguration>()
            {
                obj = observer,
                prevValue = observer.config
            });

            observer.config = targetConfig;
        }

        protected override void ObjectExitedTrigger(ISightTarget observer)
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