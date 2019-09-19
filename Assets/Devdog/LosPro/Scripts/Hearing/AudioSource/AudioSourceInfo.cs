using UnityEngine;
using System.Collections;


namespace Devdog.LosPro
{
    public struct AudioSourceInfo
    {
        public AudioSourceSampleData? lastHeardAtPosition { get; set; }
        public IAudioSource audioSource { get; set; }
        public IListener listener { get; set; }

//        public IListener listener { get; set; }

        public float volume { get; set; }
    }
}