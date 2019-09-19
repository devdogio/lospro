using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Devdog.LosPro
{
    public interface IAudioSourceCallbacks
    {
        void OnHeardBy(AudioSourceInfo info);
    }
}