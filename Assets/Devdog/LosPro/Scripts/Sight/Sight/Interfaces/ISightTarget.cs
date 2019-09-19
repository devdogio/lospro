using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Devdog.LosPro
{
    /// <summary>
    /// Indicates a target that can be detected by a sight.
    /// </summary>
    public interface ISightTarget : ISightTargetCallbacks
    {
        #region Unity MonoBehaviour

        string name { get; }
        GameObject gameObject { get; }
        Transform transform { get; }
        bool enabled { get; set; }

        #endregion

        SightTargetConfiguration config { get; set; }
        SightTargetIndexingConfiguration indexingConfig { get; set; }
    }
}
