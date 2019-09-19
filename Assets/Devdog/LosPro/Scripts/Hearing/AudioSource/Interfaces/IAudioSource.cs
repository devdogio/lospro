using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Devdog.General;


namespace Devdog.LosPro
{
    public interface IAudioSource : IPoolable, IAudioSourceCallbacks
    {
        AudioSource audioSource { get; }
        float maxGrowthSize { get; set; }
        float growthSizeFactor { get; set; }
        GameObject emitter { get; set; }
        int emitterCategory { get; set; }

//        float minHearingMultiplier { get; set; } // IDEA: Add?
        
        void Emit();

        bool IsHeardBy(IListener listener);
    }
}