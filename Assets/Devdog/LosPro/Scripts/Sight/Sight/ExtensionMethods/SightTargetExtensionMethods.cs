using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Devdog.LosPro
{
    public static class SightTargetExtensionMethods
    {
        public static bool IsDestroyed(this ISightTarget target)
        {
            return EqualsDestroyed(target, null);
        }

        public static bool EqualsDestroyed(this ISightTarget target, ISightTarget targetB)
        {
            return ReferenceEquals(target, targetB) || target.Equals(targetB);
        }

        /// <summary>
        /// </summary>
        /// <returns>Returns the info the observer has about this target.
        /// Returns null if the observer has no info on this target.</returns>
        public static SightTargetInfo GetInfoFromObserver(this ISightTarget target, IObserver observer)
        {
            foreach (var info in observer.targetsInRange)
            {
                if (info.target == target)
                {
                    return info;
                }
            }

            return null;
        }

        public static bool IsInRangeOfObserver(this ISightTarget target, IObserver observer)
        {
            return GetInfoFromObserver(target, observer) != null;
        }
    }
}
