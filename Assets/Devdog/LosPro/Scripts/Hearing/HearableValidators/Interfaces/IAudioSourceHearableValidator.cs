using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Devdog.LosPro
{
    /// <summary>
    /// Used to validate if an audio source can be heard or not.
    /// </summary>
    public interface IAudioSourceHearableValidator
    {
        bool IsHearable(IListener listener, IAudioSource source, AudioSourceInfo info);
    }
}
