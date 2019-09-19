using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Assertions;

namespace Devdog.LosPro
{
    public class HearingLastHeardVolumeAggroModule : MonoBehaviour, IObserverAggroModule, IListenerCallbacks
    {
//        private IObserverAggro _observerAggro;

        public new string name
        {
            get { return "Last heard volume"; }
        }
        public float intensity = 1f;
        public float forgetSoundAfterSeconds = 5f;


        private List<KeyValuePair<AudioSourceInfo, float>> _hearingHistory = new List<KeyValuePair<AudioSourceInfo, float>>(); 

        public void Init(IObserverAggro observerAggro)
        {
//            _observerAggro = observerAggro;
        }

        public float CalculateAggroForTarget(ISightTarget target)
        {
            // TODO: May create some GC...
            _hearingHistory.RemoveAll(o => o.Value < LosUtility.time);

            float extra = 0f;
            foreach (var keyValuePair in _hearingHistory)
            {
                var source = keyValuePair.Key.audioSource;
                if (source.emitter.transform == target.transform || source.emitter.transform.IsChildOf(target.transform))
                {
                    extra += keyValuePair.Key.volume * intensity;
                }
            }

            return extra;
//            var listener = _observerAggro.observer.gameObject.GetComponent<IListener>();
//            if (listener != null && listener.lastHeardAudioSource != null)
//            {
//                var source = listener.lastHeardAudioSource.Value.audioSource;
//                if (source.emitter == target.gameObject || source.emitter.transform.IsChildOf(listener.transform))
//                {
//                    return listener.lastHeardAudioSource.Value.volume * intensity;
//                }
//            }
//            return 0f;
        }

        public void OnHeardTarget(AudioSourceInfo info)
        {
            _hearingHistory.Add(new KeyValuePair<AudioSourceInfo, float>(info, LosUtility.time + forgetSoundAfterSeconds));
        }
    }
}
