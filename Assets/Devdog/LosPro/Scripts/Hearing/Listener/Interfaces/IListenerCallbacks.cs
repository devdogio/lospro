using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Devdog.LosPro
{
    public interface IListenerCallbacks
    {
        void OnHeardTarget(AudioSourceInfo info);
     
        //void OnHearingTarget(IAudioSource source, HearingInfo info); // IDEA: Needed?
    }
}