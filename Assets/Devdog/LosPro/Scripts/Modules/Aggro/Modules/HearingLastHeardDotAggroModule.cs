using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Devdog.General;
using UnityEngine;
using UnityEngine.Assertions;

namespace Devdog.LosPro
{
    public class HearingLastHeardDotAggroModule : MonoBehaviour, IObserverAggroModule
    {
        private IObserverAggro _observerAggro;

        public new string name
        {
            get { return "Last heard dot"; }
        }
        public float intensity = 1f;
        
        public void Init(IObserverAggro observerAggro)
        {
            _observerAggro = observerAggro;
        }

        public float CalculateAggroForTarget(ISightTarget target)
        {
            var listener = _observerAggro.observer.gameObject.GetComponent<IListener>();
            if (listener != null && listener.lastHeardAudioSource != null)
            {
                var source = listener.lastHeardAudioSource.Value.audioSource;
                if (source.emitter == target.gameObject || source.emitter.transform.IsChildOf(listener.transform))
                {
                    return Vector3.Dot(_observerAggro.observer.transform.forward, Vector3.Normalize(listener.lastHeardAudioSource.Value.audioSource.transform.position - _observerAggro.observer.transform.position));
                }
            }

            return 0f;
        }
    }
}
