using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace Devdog.LosPro
{
    [AddComponentMenu(LosPro.ProductName + "Zones/Listener zone 3D")]
    public class ListenerZone : AffectorZoneBase<IListener, ListenerConfiguration>
    {
        public ListenerConfiguration listenerConfig = new ListenerConfiguration();

        protected override void ObjectEnteredTrigger(IListener listener)
        {
            overrideValues.Add(new OverrideValue<IListener, ListenerConfiguration>()
            {
                obj = listener,
                prevValue = listener.config
            });

            listener.config = listenerConfig;
        }

        protected override void ObjectExitedTrigger(IListener listener)
        {
            for (int i = overrideValues.Count - 1; i >= 0; i--)
            {
                if (overrideValues[i].obj == listener)
                {
                    listener.config = overrideValues[i].prevValue;
                    overrideValues.RemoveAt(i);
                    break;
                }
            }
        }
    }
}