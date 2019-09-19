using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Devdog.LosPro.Demo
{
    [RequireInterface(typeof(ISightTarget))]
    public class SightDetectedBar : MonoBehaviour, ISightTargetCallbacks
    {
        [SerializeField]
        private Text _percentageVisible;

        [SerializeField]
        private UnityEngine.UI.Graphic _detecting;

        [SerializeField]
        private Image _detectionImageFill;

        private float _startValue = 0f;
        private float _aimValue = 0f;
        private float _timer = 0f;
        private float _detectionTimer = 0f;
        private ISightTarget _sightTarget;

        protected void Awake()
        {
            _sightTarget = GetComponent<ISightTarget>();
            _detecting.gameObject.SetActive(false);
        }

        protected void Update()
        {
            _timer += Time.deltaTime;
            _detectionImageFill.fillAmount = Mathf.Lerp(_startValue, _aimValue, _timer);
        }

        public void OnObserverTryingToDetect(SightTargetInfo sightInfo)
        {
            RepaintVisibility(sightInfo);
        }

        public void OnGettingDetected(SightTargetInfo info)
        {
            _detecting.gameObject.SetActive(true);

            _detectionTimer += info.sight.config.updateInterval;
            _aimValue = _detectionTimer / _sightTarget.config.detectionTime;

            _startValue = _detectionImageFill.fillAmount;
            _timer = 0f;
        }

        public void OnStopGettingDetected(SightTargetInfo info)
        {
            _detecting.gameObject.SetActive(false);

            _startValue = _detectionImageFill.fillAmount;
            _aimValue = 0f;
            _timer = 0f;
            _detectionTimer = 0f;
        }

        public void OnDetectedByObserver(SightTargetInfo sightInfo)
        {
            _startValue = _detectionImageFill.fillAmount;
            _aimValue = 1f;
            _timer = 0f;
        }

        public void OnUnDetectedByObserver(SightTargetInfo sightInfo)
        {
            _startValue = _detectionImageFill.fillAmount;
            _aimValue = 0f;
            _timer = 0f;
        }

        public void OnCameIntoObserverRange(SightTargetInfo sightInfo)
        {
        }

        public void OnWentOutOffObserverRange(SightTargetInfo sightInfo)
        {
            RepaintVisibility(sightInfo);
        }



        private void RepaintVisibility(SightTargetInfo sightInfo)
        {
            _percentageVisible.text = Mathf.Round(sightInfo.visibleFactor * 100f) + "%";
        }
    }
}