#if PLAYMAKER

using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;


namespace Devdog.LosPro
{
    /// <summary>
    /// Receives messages from Observers, Sight targets, Listeners and Audio sources and sends them to PlayMaker.
    /// </summary>
    public class LosToPlayMakerEventBridge : MonoBehaviour, IObserverCallbacks, ISightTargetCallbacks, IListenerCallbacks, IAudioSourceCallbacks
    {
        private PlayMakerFSM _fsm;

        protected void Awake()
        {
            _fsm = GetComponent<PlayMakerFSM>();
        }

        private void _FireEvent(string name)
        {
            if (_fsm != null)
            {
                _fsm.SendEvent("LosPro/" + name);
            }
        }

        private void FireEvent(string name, GameObject obj = null)
        {
            if (obj != null && _fsm != null)
            {
                var i = _fsm.FsmVariables.FindFsmGameObject("LosPro/Event_GameObject");
                Assert.IsNotNull(i, "No FSM Variable found with name LosPro/Event_GameObject");
                i.Value = obj;
            }

            _FireEvent(name);
        }

        #region IListener

        public void OnHeardTarget(AudioSourceInfo info)
        {
            FireEvent("OnHeardTarget", info.audioSource.emitter);
        }

        #endregion

        #region IObserver

        public void OnTargetDestroyed(SightTargetInfo info)
        {
            FireEvent("OnTargetDestroyed");
        }

        public void OnTryingToDetectTarget(SightTargetInfo info)
        {
            FireEvent("OnTryingToDetectTarget", info.target.gameObject);
        }

        public void OnDetectingTarget(SightTargetInfo info)
        {
            FireEvent("OnDetectingTarget", info.target.gameObject);
        }

        public void OnDetectedTarget(SightTargetInfo info)
        {
            FireEvent("OnDetectedTarget", info.target.gameObject);
        }

        public void OnStopDetectingTarget(SightTargetInfo info)
        {
            FireEvent("OnStopDetectingTarget", info.target.gameObject);
        }

        public void OnUnDetectedTarget(SightTargetInfo info)
        {
            FireEvent("OnUnDetectedTarget", info.target.gameObject);
        }

        public void OnTargetWentOutOfRange(SightTargetInfo info)
        {
            FireEvent("OnTargetWentOutOfRange", info.target.gameObject);
        }

        public void OnTargetCameIntoRange(SightTargetInfo info)
        {
            FireEvent("OnTargetCameIntoRange", info.target.gameObject);
        }

        #endregion

        #region ISightTarget

        public void OnDetectedByObserver(SightTargetInfo info)
        {
            FireEvent("OnDetectedByObserver", info.sight.observer.gameObject);
        }

        public void OnUnDetectedByObserver(SightTargetInfo info)
        {
            FireEvent("OnUnDetectedByObserver", info.sight.observer.gameObject);
        }

        public void OnCameIntoObserverRange(SightTargetInfo info)
        {
            FireEvent("OnCameIntoObserverRange", info.sight.observer.gameObject);
        }

        public void OnWentOutOffObserverRange(SightTargetInfo info)
        {
            FireEvent("OnWentOutOffObserverRange", info.sight.observer.gameObject);
        }

        public void OnObserverTryingToDetect(SightTargetInfo info)
        {
            FireEvent("OnObserverTryingToDetect", info.sight.observer.gameObject);
        }

        public void OnGettingDetected(SightTargetInfo info)
        {
            FireEvent("OnGettingDetected", info.sight.observer.gameObject);
        }

        public void OnStopGettingDetected(SightTargetInfo info)
        {
            FireEvent("OnStopGettingDetected", info.sight.observer.gameObject);
        }

        #endregion

        public void OnHeardBy(AudioSourceInfo info)
        {
            FireEvent("OnHeardBy", info.audioSource.gameObject);
        }
    }
}

#endif