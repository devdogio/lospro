using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Devdog.LosPro
{
    public class RaycastAudioSourceHearableValidator : IAudioSourceHearableValidator
    {

        private ISightRaycaster _raycaster;
        private ListenerConfiguration _config;

        public RaycastAudioSourceHearableValidator(ISightRaycaster raycaster, ListenerConfiguration config)
        {
            _raycaster = raycaster;
            _config = config;
        }

        public bool IsHearable(IListener listener, IAudioSource source, AudioSourceInfo info)
        {
            if (info.volume >= listener.config.minHearingVolume)
            {
                var raycastPosition = new Vector3[]
                {
                    new Vector3(0f, 0f), // Middle
                    new Vector3(0f, 1f), // Up  
                    new Vector3(0f, -1f), // Down
                    new Vector3(-1f, 0f), // Left side  
                    new Vector3(1f, 0f), // Right side  
                };

                const int levels = 1;
                const float scaling = 1.5f;
                for (int i = 1; i < levels + 1; i++)
                {
                    foreach (var pos in raycastPosition)
                    {
                        var raycastToPosition = listener.transform.position + (pos * i * scaling);

                        RaycastHit hitInfo;
                        bool hit = _raycaster.Linecast(source.transform.position, raycastToPosition, out hitInfo, _config.raycastLayer);
                        // If nothing was hit the object is visible.
                        if (hit == false)
                        {
                            hitInfo.gameObject = listener.gameObject;
                            hitInfo.point = raycastToPosition;
                            hitInfo.normal = Vector3.zero;
                        }

                        // Direct path from audio source to listener.
                        if (_config.debug)
                        {
                            LosDebugUtility.DrawDebugLine(source.gameObject, hitInfo.gameObject, listener.gameObject, source.transform.position, hitInfo.point, raycastToPosition, Color.magenta, Color.black);
                        }

                        // If we did hit something, is it the target?
                        if (LosUtility.IsValidTarget(listener.gameObject, hitInfo.gameObject) || hit == false)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
