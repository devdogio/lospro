using UnityEngine;
using System.Collections;

#if UNITY_EDITOR

namespace Devdog.LosPro
{
    [RequireInterface(typeof(IObserver))]
    [AddComponentMenu(LosPro.AddComponentMenuPath + "Sight/Extrapolation Debugger", 99)]
    public sealed class ExtrapolationDebugger : MonoBehaviour, IObserverCallbacks
    {
        private Vector3? _currentDrawPosition;
        private Vector3? _nextDrawPosition;
        private float _maxTime = 1f;
        private float _currentInterpTime = 0f;


        private IEnumerator _DebugSample(SightTargetSampleData[] samples, SightTargetInfo info)
        {
            _maxTime = info.sight.config.updateInterval;
            var waitTime = new WaitForSeconds(_maxTime);

            for (int i = 0; i < samples.Length; i++)
            {
                _currentInterpTime = 0f;
                _currentDrawPosition = samples[i].position;
                _nextDrawPosition = samples[Mathf.Clamp(i + 1, 0, samples.Length - 1)].position;

                yield return waitTime;
            }

            _currentDrawPosition = null;
        }

        private void OnDrawGizmos()
        {
            if (_currentDrawPosition != null)
            {
                Gizmos.color = Color.yellow;

                float factor = 1.0f/_maxTime;
                _currentInterpTime += Time.deltaTime * factor;

                Gizmos.DrawWireSphere(Vector3.Lerp(_currentDrawPosition.Value, _nextDrawPosition.Value, _currentInterpTime), 1f);

                Gizmos.color = Color.white;
            }
        }

        public void OnStopDetectingTarget(SightTargetInfo info)
        {
        }

        public void OnUnDetectedTarget(SightTargetInfo info)
        {
            StopAllCoroutines(); // Stop previous ones, just to be safe.
            StartCoroutine(_DebugSample(info.extrapolatedSampleData, info));
        }

        public void OnTargetCameIntoRange(SightTargetInfo info)
        {
        }

        public void OnTargetWentOutOfRange(SightTargetInfo info)
        {
        }

        public void OnTargetDestroyed(SightTargetInfo info)
        {
        }

        public void OnTryingToDetectTarget(SightTargetInfo info)
        {
        }

        public void OnDetectingTarget(SightTargetInfo info)
        {
            
        }

        public void OnDetectedTarget(SightTargetInfo info)
        {
        }
    }
}
#else

namespace Devdog.LosPro
{
    public class ExtrapolationDebugger : MonoBehaviour
    {
        
    }
}


#endif