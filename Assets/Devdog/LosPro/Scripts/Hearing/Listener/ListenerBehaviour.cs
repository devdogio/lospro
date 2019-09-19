using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Devdog.LosPro
{
    [RequireComponent(typeof(Rigidbody))]
    [AddComponentMenu(LosPro.AddComponentMenuPath + "Audio/Listener 3D")]
    public class ListenerBehaviour : MonoBehaviour, IListener
    {
        [SerializeField]
        private ListenerConfiguration _config = new ListenerConfiguration();
        public ListenerConfiguration config
        {
            get { return _config; }
            set { _config = value; }
        }

        private IAudioSourceHearableValidator[] _hearableValidators = new IAudioSourceHearableValidator[0];
        public IAudioSourceHearableValidator[] hearableValidators
        {
            get { return _hearableValidators; }
        }

        public AudioSourceInfo? lastHeardAudioSource { get; protected set; }


        protected virtual void Awake()
        {
            SetAudioSourceHearableValidator();
        }

        protected virtual void SetAudioSourceHearableValidator()
        {
            _hearableValidators = new IAudioSourceHearableValidator[]
            {
                new PathfindingAudioSourceHearableValidator(config), 
                new RaycastAudioSourceHearableValidator(new Raycaster3D(), config)
            };
        }

        public void OnHeardTarget(AudioSourceInfo info)
        {
            lastHeardAudioSource = info;
            if (config.debug)
            {
                Debug.Log("<color=green>Heard target " + info.audioSource.audioSource.clip + " (volume: " + info.volume + ")</color>", info.audioSource.gameObject);
            }
        }

        public virtual void ResetStateForPool()
        {
            // Nothing to reset
        }
    }
}
