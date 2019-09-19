using UnityEngine;
using System.Collections;

namespace Devdog.LosPro.Demo
{
    public class DetectedSprites : MonoBehaviour
    {
        public Sprite beenHeardSprite;
        public Sprite beenSeen;

        [SerializeField]
        private SpriteRenderer _sightRenderer;

        [SerializeField]
        private SpriteRenderer _hearingRenderer;

        protected void OnHeard(AudioSourceInfo info)
        {
            _sightRenderer.sprite = null;
        }
    }
}