using UnityEngine;
using System.Collections;

namespace Devdog.LosPro
{
    public static class LosDebugUtility
    {

        public static void DrawDebugLine(Vector3 from, Vector3 to, Vector3 goal, float time = 0.1f)
        {
#if UNITY_EDITOR
            var positiveColor = Color.green;
            var negativeColor = Color.red;

            DrawDebugLine(from, to, goal, positiveColor, negativeColor, time);
#endif
        }

        public static void DrawDebugLine(GameObject from, GameObject hit, GameObject target, Vector3 fromRaycastPosition, Vector3 hitPosition, Vector3 targetRaycastPosition, float time = 0.1f)
        {
#if UNITY_EDITOR
            DrawDebugLine(from, hit, target, fromRaycastPosition, hitPosition, targetRaycastPosition, Color.green, Color.red, time);
#endif
        }

        public static void DrawDebugLine(GameObject from, GameObject hit, GameObject target, Vector3 fromRaycastPosition, Vector3 hitPosition, Vector3 targetRaycastPosition, Color positive, Color negative, float time = 0.1f)
        {
#if UNITY_EDITOR

            if (LosUtility.IsValidTarget(target, hit))
            {
                DrawDebugLine(fromRaycastPosition, targetRaycastPosition, targetRaycastPosition, positive, negative,
                    time);
            }
            else
            {
                DrawDebugLine(fromRaycastPosition, hitPosition, targetRaycastPosition, positive, negative, time);
            }
#endif
        }


        public static void DrawDebugLine(Vector3 from, Vector3 to, Vector3 goal, Color positiveColor, Color negativeColor, float time = 0.1f)
        {
#if UNITY_EDITOR
            Debug.DrawLine(from, to, positiveColor, time, true);

            if (Approx(to, goal) == false)
            {
                Debug.DrawLine(to, goal, negativeColor, time, true);
            }
#endif
        }

        private static bool Approx(Vector3 a, Vector3 b)
        {
            return Mathf.Approximately(a.x, b.x) && Mathf.Approximately(a.y, b.y) && Mathf.Approximately(a.z, b.z);
        }
    }
}