using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Devdog.LosPro
{
    [System.Serializable]
    public partial class SightTargetIndexingConfiguration
    {
        public TargetIndexingType indexingType = TargetIndexingType.Automatic;

        [SerializeField]
        private Transform[] _raycastPoints = new Transform[0];
        public Transform[] raycastPoints
        {
            get { return _raycastPoints; }
            set { _raycastPoints = value; }
        }
    }
}
