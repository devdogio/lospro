using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Devdog.LosPro
{
    using UnityEngine;

    public interface ISightCache
    {
        SightConfiguration config { get; }


        bool Contains(ISightTarget target);
        void GetFromCache(ISightTarget target, out SightCacheLookup cacheLookup);
        bool GenerateCache(ISightTarget target);


        void ClearCache();
        void ClearFromCache(ISightTarget target);
    }
}
