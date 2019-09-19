using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

#if UNITY_5_5_OR_NEWER
using UnityEngine.AI;
#endif

namespace Devdog.LosPro
{
    public class PathfindingAudioSourceHearableValidator : IAudioSourceHearableValidator
    {
        private ListenerConfiguration _config;

        /// <summary>
        /// How much longer is the path allowd to be compared to a direct line?
        /// For example, if the path is allowed to be 1.5x longer and the distance to the sound is 10m the sound can still
        /// be heard if the path is less than 15m long. If the path is longer the sound is considered not heard.
        /// </summary>
        private const float PathMaxLengthMultiplier = 1.5f;

        public PathfindingAudioSourceHearableValidator(ListenerConfiguration config)
        {
            _config = config;
        }

        public bool IsHearable(IListener listener, IAudioSource source, AudioSourceInfo info)
        {
            if (info.volume >= listener.config.minHearingVolume)
            {
                var path = new NavMeshPath();
                bool foundPath = NavMesh.CalculatePath(source.transform.position, listener.transform.position, NavMesh.AllAreas, path);
                if (foundPath && path.status == NavMeshPathStatus.PathComplete)
                {
                    var corners = path.corners;

                    float pathLength = 0f;
                    for (int i = 0; i < corners.Length - 1; i++)
                    {
                        pathLength += Vector3.Distance(corners[i], corners[i + 1]);
                        if (_config.debug)
                        {
                            Debug.DrawLine(corners[i], corners[i + 1], Color.red, 0.2f);
                        }
                    }

                    //Debug.Log("Path length is " + pathLength);
                    return pathLength < Vector3.Distance(source.transform.position, listener.transform.position) * PathMaxLengthMultiplier;
                }
            }

            return false;
        }
    }
}
