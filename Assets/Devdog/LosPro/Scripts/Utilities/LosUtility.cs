using System;
using UnityEngine;

namespace Devdog.LosPro
{
    public static class LosUtility
    {
        public static float time
        {
            get { return Time.time; }
        }

        public static bool IsValidTarget(GameObject target, GameObject hit)
        {
            return target == hit || hit.transform.IsChildOf(target.transform);
        }
    }
}
