using UnityEngine;
using System.Collections;

namespace Devdog.LosPro.Demo
{
    public class ObserverAggroCallbacksTest : MonoBehaviour, IObserverAggroCallbacks
    {
        public void OnTargetWithHighestAggroChanged(ISightTarget target, float aggro)
        {
            
            Debug.Log("Observer aggro target changed");
        }
    }
}