using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Devdog.LosPro
{
    public interface ISightPathExtrapolator
    {
        void GetAllSamples(out IEnumerable<KeyValuePair<ISightTarget, FixedSizeQueue<SightTargetSampleData>>> samples);

        void ClearAllSamples();
        void ClearSamplesForTarget(ISightTarget target);

        void TakeSample(ISightTarget target);
        void ExtrapolatePath(ISightTarget target, float secondsForward, out SightTargetSampleData[] extrapolationData);

    }
}
