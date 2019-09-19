using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Devdog.LosPro
{
    public partial class SightCache2D : SightCache
    {

        public SightCache2D(SightConfiguration config)
            : base(config)
        { }

        public override bool GenerateCache(ISightTarget target)
        {
            return base.GenerateCache(target);
        }
        
    }
}
