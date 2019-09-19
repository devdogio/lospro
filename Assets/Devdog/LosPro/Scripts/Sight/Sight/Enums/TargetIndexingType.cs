using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Devdog.LosPro
{
    public enum TargetIndexingType
    {
        /// <summary>
        /// Automatic indexing; Let LosPro automatically generate a cache for the target.
        /// </summary>
        Automatic,

        /// <summary>
        /// Manually define raycasts position.
        /// The fastest and most accurate option.
        /// </summary>
        Manual
    }
}
