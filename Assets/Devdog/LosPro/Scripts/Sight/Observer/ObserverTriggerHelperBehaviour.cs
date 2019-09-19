using System;
using UnityEngine;

namespace Devdog.LosPro
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody))]
    public sealed class ObserverTriggerHelperBehaviour : MonoBehaviour
    {
        private IObserver _observer;
        public IObserver observer
        {
            get
            {
                if (_observer == null)
                {
                    _observer = GetComponentInParent<IObserver>();
                }

                return _observer;
            }
        }

        private void OnTriggerEnter(Collider col)
        {
            observer.sight.OnTriggerEnter(col.gameObject);
        }

        private void OnTriggerExit(Collider col)
        {
            observer.sight.OnTriggerExit(col.gameObject);
        }
    }
}
