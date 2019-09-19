using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace Devdog.LosPro
{
    [RequireComponent(typeof(SphereCollider))]
    public abstract class AffectorZoneBase<T1, T2> : MonoBehaviour, IAffectorZone
        where T1 : class
        where T2 : class
    {
        protected struct OverrideValue<T3, T4>
        {
            public T3 obj { get; set; }
            public T4 prevValue { get; set; }
        }

        public SphereCollider sphereCollider { get; protected set; }
        protected List<OverrideValue<T1, T2>> overrideValues = new List<OverrideValue<T1, T2>>();

        protected virtual void Awake()
        {
            sphereCollider = GetComponent<SphereCollider>();
            sphereCollider.isTrigger = true;
        }

        protected void OnEnable()
        {
            
        }

        protected virtual void OnTriggerEnter(Collider col)
        {
            if (isActiveAndEnabled)
            {
                var observer = GetT1(col.gameObject, true);
                if (observer != null)
                {
                    ObjectEnteredTrigger(observer);
                }
            }
        }

        protected virtual void OnTriggerExit(Collider col)
        {
            if (isActiveAndEnabled)
            {
                var observer = GetT1(col.gameObject, false);
                if (observer != null)
                {
                    ObjectExitedTrigger(observer);
                }
            }
        }

        protected abstract void ObjectEnteredTrigger(T1 observer);
        protected abstract void ObjectExitedTrigger(T1 observer);


        protected T1 GetT1(GameObject obj, bool searchIndexed)
        {
            var observer = obj.GetComponentInParent<T1>();
            if (observer != null) //  && LosUtility.IsValidTarget(observer.gameObject, obj)
            {
                if (searchIndexed == false)
                {
                    return observer;
                }

                if (overrideValues.Any(o => o.obj.Equals(observer)) == false)
                {
                    return observer;
                }
            }

            return null;
        }
    }
}