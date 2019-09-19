using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Devdog.LosPro
{
    public static class ObserverExtensionMethods
    {
        public static bool IsTargetInRange(this IObserver observer, ISightTarget target)
        {
            return target.IsInRangeOfObserver(observer);
        }
    }
}
