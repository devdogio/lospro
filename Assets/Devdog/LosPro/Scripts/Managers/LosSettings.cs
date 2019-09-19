using UnityEngine;
using System.Collections;

namespace Devdog.LosPro
{
    [CreateAssetMenu(menuName = LosPro.CreateAssetMenuPath + "Los Settings")]
    public class LosSettings : ScriptableObject
    {

        public TargetCategory[] targetCategories = new TargetCategory[]
        {
            new TargetCategory()
            {
                bitFlagID = 1,
                name = "Default"
            }, 
        };

        [Header("Settings")]
        [Category("Audio")]
        public float speedOfSoundUnitsPerSecond = 343f;

        [Header("Layers")]
        [Category("Layers")]
        public int sightLayerID = 10;
        public int hearingLayerID = 11;
    }
} 