using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Devdog.LosPro
{
    /// <summary>
    /// An optional component to override the aggro indexing information of a target.
    /// </summary>
    [RequireInterface(typeof(ISightTarget))]
    public class SightTargetAggroBehaviour : MonoBehaviour
    {
        
        public SightTargetAggroConfiguration aggroConfig = new SightTargetAggroConfiguration();

    }
}