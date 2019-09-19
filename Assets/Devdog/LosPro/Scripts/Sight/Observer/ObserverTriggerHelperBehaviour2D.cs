using System;
using UnityEngine;

namespace Devdog.LosPro
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class ObserverTriggerHelperBehaviour2D : MonoBehaviour
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

        private void OnTriggerEnter2D(Collider2D col)
        {
            observer.sight.OnTriggerEnter(col.gameObject);
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            observer.sight.OnTriggerExit(col.gameObject);
        }
    }
}
