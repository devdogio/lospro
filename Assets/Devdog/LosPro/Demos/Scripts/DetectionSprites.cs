using UnityEngine;
using System.Collections;

namespace Devdog.LosPro.Demo
{
    public class DetectionSprites : MonoBehaviour, IObserverCallbacks, IListenerCallbacks
    {
        public float unhearTime = 5f;

        public Sprite heardTargetSprite;
        public Sprite detectingTargetSprite;
        public Sprite detectedTargetSprite;


        [SerializeField]
        private SpriteRenderer _sightRenderer;

        [SerializeField]
        private SpriteRenderer _hearingRenderer;


        private WaitForSeconds _wait;
        protected void Awake()
        {
            _wait = new WaitForSeconds(unhearTime);
        }


        public void OnUnDetectedTarget(SightTargetInfo info)
        {
            _sightRenderer.sprite = null;
        }

        public void OnTargetWentOutOfRange(SightTargetInfo info)
        {
            _sightRenderer.sprite = null;
        }

        public void OnTargetCameIntoRange(SightTargetInfo info)
        {

        }

        public void OnTryingToDetectedTarget(SightTargetInfo info)
        {
            
        }

        public void OnDetectingTarget(SightTargetInfo info)
        {
            _sightRenderer.sprite = detectingTargetSprite;
        }

        public void OnTryingToDetectTarget(SightTargetInfo info)
        {
            
        }

        public void OnDetectedTarget(SightTargetInfo info)
        {
            _sightRenderer.sprite = detectedTargetSprite;
        }

        public void OnStopDetectingTarget(SightTargetInfo info)
        {
            _sightRenderer.sprite = null;
        }

        public void OnTargetDestroyed(SightTargetInfo info)
        {
            _sightRenderer.sprite = null;
        }

        public void OnHeardTarget(AudioSourceInfo info)
        {
            StopAllCoroutines();
            StartCoroutine(_OnHeardTarget());
        }

        private IEnumerator _OnHeardTarget()
        {
            _hearingRenderer.sprite = heardTargetSprite;

            yield return _wait;

            _hearingRenderer.sprite = null;
        }
    }
}