using UnityEngine;
using System.Collections;


namespace Devdog.LosPro
{
    [System.Serializable]
    public partial class ListenerConfiguration
    {
        [TargetCategoriesMask]
        public int targetCategoryMask;

        public LayerMask raycastLayer = -1;

        //[Range(0.1f, 5f)]
        //public float updateInterval = 0.5f;

//        public float emitRange = 10f;


        /// <summary>
        /// How much of the sound should this listener at least hear before considering it 'heard'?
        /// </summary>
        [Range(0f, 1f)]
        public float minHearingVolume = 0.1f;

        public bool debug = false;
    }
}