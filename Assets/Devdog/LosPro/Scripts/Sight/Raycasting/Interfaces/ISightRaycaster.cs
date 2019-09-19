using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Devdog.LosPro
{
    public interface ISightRaycaster
    {
        bool Linecast(Vector3 from, Vector3 to, out RaycastHit hitInfo, LayerMask layers);
        bool Raycast(Vector3 from, Vector3 direction, out RaycastHit hitInfo, float distance, LayerMask layers);
    }
}
