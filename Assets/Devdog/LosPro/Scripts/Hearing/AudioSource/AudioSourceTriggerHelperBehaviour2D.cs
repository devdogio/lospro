using System;
using System.Collections.Generic;
using UnityEngine;

namespace Devdog.LosPro
{
    public class AudioSourceTriggerHelperBehaviour2D : AudioSourceTriggerHelperBehaviour
    {
        protected override void OnTriggerEnter(Collider col)
        {
            // Ignore...
        }

        protected virtual void OnTriggerEnter2D(Collider2D col)
        {
            OnTargetEnter(col.gameObject);
        }
    }
}
