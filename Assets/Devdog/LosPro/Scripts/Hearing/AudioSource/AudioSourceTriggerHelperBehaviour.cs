using System;
using System.Collections.Generic;
using Devdog.General;
using UnityEngine;

namespace Devdog.LosPro
{
    public class AudioSourceTriggerHelperBehaviour : MonoBehaviour
    {
        private IAudioSource _audioSource;
        public IAudioSource audioSource
        {
            get
            {
                if (_audioSource == null)
                {
                    _audioSource = GetComponent<IAudioSource>();
                }

                return _audioSource;
            }
        }

        private static List<IAudioSourceCallbacks> _callbacks = new List<IAudioSourceCallbacks>(); 
        private static List<IListenerCallbacks> _listenerCallbacks = new List<IListenerCallbacks>(); 

        protected virtual void OnTriggerEnter(Collider col)
        {
            OnTargetEnter(col.gameObject);
        }

        protected virtual void OnTargetEnter(GameObject target)
        {
            var listener = target.GetComponentInParent<IListener>();
            if (listener != null && LosUtility.IsValidTarget(listener.gameObject, target))
            {
                if (audioSource.IsHeardBy(listener))
                {
                    // Prevent objects with multiple colliders to invoke events more than once.
                    return;
                }

                if (((listener.config.targetCategoryMask & audioSource.emitterCategory) == 0) || audioSource.emitterCategory == 0)
                {
                    return;
                }

                var dist = Vector3.Distance(transform.position, listener.gameObject.transform.position);
                var info = new AudioSourceInfo()
                {
                    volume = Mathf.Clamp01((audioSource.maxGrowthSize - dist) / audioSource.maxGrowthSize),
                    lastHeardAtPosition = new AudioSourceSampleData()
                    {
                        position = transform.position,
                        time = LosUtility.time
                    },
                    audioSource = audioSource,
                    listener = listener
                };

                if (listener.hearableValidators.Length == 0)
                {
                    HeardBy(ref info);
                }
                else
                {
                    foreach (var validator in listener.hearableValidators)
                    {
                        if (validator.IsHearable(listener, audioSource, info))
                        {
                            HeardBy(ref info);
                            break;
                        }
                    }
                }
            }
        }

        private void HeardBy(ref AudioSourceInfo info)
        {
            info.listener.gameObject.GetComponents(_listenerCallbacks);
            foreach (var callback in _listenerCallbacks)
            {
                callback.OnHeardTarget(info);
            }

            gameObject.GetComponents(_callbacks);
            foreach (var callback in _callbacks)
            {
                callback.OnHeardBy(info);
            }

            if (audioSource.emitter != null && gameObject != audioSource.emitter)
            {
                audioSource.emitter.GetComponents(_callbacks);
                foreach (var callback in _callbacks)
                {
                    callback.OnHeardBy(info);
                }
            }
        }
    }
}
