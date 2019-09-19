using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Devdog.LosPro
{
    public sealed class Raycaster3D : ISightRaycaster
    {
        public bool Linecast(Vector3 from, Vector3 to, out RaycastHit hitInfo, LayerMask layers)
        {
            UnityEngine.RaycastHit hit;
            bool cast = Physics.Linecast(from, to, out hit, layers);
            hitInfo = new RaycastHit();
            if (cast)
            {
                hitInfo.gameObject = hit.transform.gameObject;
                hitInfo.distance = hit.distance;
                hitInfo.normal = hit.normal;
                hitInfo.point = hit.point;
            }

            return cast;
        }

        public bool Raycast(Vector3 from, Vector3 direction, out RaycastHit hitInfo, float distance, LayerMask layers)
        {
            UnityEngine.RaycastHit hit;
            bool cast = Physics.Raycast(from, direction, out hit, distance, layers);
            hitInfo = new RaycastHit();
            if (cast)
            {
                hitInfo.gameObject = hit.transform.gameObject;
                hitInfo.distance = hit.distance;
                hitInfo.normal = hit.normal;
                hitInfo.point = hit.point;
            }

            return cast;
        }
    }
}
