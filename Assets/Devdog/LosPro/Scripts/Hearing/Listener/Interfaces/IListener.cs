using UnityEngine;
using System.Collections;
using Devdog.General;

namespace Devdog.LosPro
{
    public interface IListener : IPoolable, IListenerCallbacks
    {
        IAudioSourceHearableValidator[] hearableValidators { get; }
        ListenerConfiguration config { get; set; }

        AudioSourceInfo? lastHeardAudioSource { get; }
    }
}