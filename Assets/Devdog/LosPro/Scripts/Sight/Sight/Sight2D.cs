using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Assertions;

namespace Devdog.LosPro
{
    public class Sight2D : Sight
    {

        public Sight2D(IObserver observer, SightConfiguration config)
            : base(observer, config, new SightCache(config), new Raycaster2D(), new SightPathExtrapolator(config))
        { }

        public Sight2D(IObserver observer, SightConfiguration config, ISightCache cache)
            : base(observer, config, cache, new Raycaster3D(), new SightPathExtrapolator(config))
        { }

        public Sight2D(IObserver observer, SightConfiguration config, ISightCache cache, ISightRaycaster raycaster, ISightPathExtrapolator extrapolator)
            : base(observer, config, cache, raycaster, extrapolator)
        { }


        public override bool IsTargetInFOV(ISightTarget target)
        {
            return Vector3.Dot(-transform.right, Vector3.Normalize(target.transform.position - transform.position)) > config.fieldOfViewDotValue;
        }
    }
}