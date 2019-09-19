using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Devdog.LosPro
{
    public sealed class Raycaster2D : ISightRaycaster
    {
        public bool Linecast(Vector3 from, Vector3 to, out RaycastHit hitInfo, LayerMask layers)
        {
            RaycastHit2D hit = Physics2D.Linecast(from, to, layers);
            hitInfo = new RaycastHit();
            if (hit.collider != null)
            {
                hitInfo.gameObject = hit.collider.gameObject;
                hitInfo.distance = hit.distance;
                hitInfo.normal = hit.normal;
                hitInfo.point = hit.point;

                return true;
            }

            return false;
        }

        public bool Raycast(Vector3 from, Vector3 direction, out RaycastHit hitInfo, float distance, LayerMask layers)
        {
            RaycastHit2D hit = Physics2D.Raycast(from, direction, distance, layers);
            hitInfo = new RaycastHit();
            if (hit.collider != null)
            {
                hitInfo.gameObject = hit.collider.gameObject;
                hitInfo.distance = hit.distance;
                hitInfo.normal = hit.normal;
                hitInfo.point = hit.point;

                return true;
            }

            return false;
        }
    }
}
