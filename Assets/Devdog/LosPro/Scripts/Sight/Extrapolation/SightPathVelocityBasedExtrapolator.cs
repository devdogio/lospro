using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Devdog.LosPro
{
    public partial class SightPathVelocityBasedExtrapolator : ISightPathExtrapolator
    {
        private readonly Dictionary<ISightTarget, FixedSizeQueue<SightTargetSampleData>> _samplesQueue;
        private readonly Dictionary<ISightTarget, SightTargetSampleData[]> _extrapolatedData; 

        private readonly SightConfiguration _config;

        public SightPathVelocityBasedExtrapolator(SightConfiguration config)
        {
            _config = config;
            _samplesQueue = new Dictionary<ISightTarget, FixedSizeQueue<SightTargetSampleData>>();
            _extrapolatedData = new Dictionary<ISightTarget, SightTargetSampleData[]>();
        }

        public void GetAllSamples(out IEnumerable<KeyValuePair<ISightTarget, FixedSizeQueue<SightTargetSampleData>>> samples)
        {
            samples = _samplesQueue;
        }

        public void ClearAllSamples()
        {
            _samplesQueue.Clear();
        }

        public void ClearSamplesForTarget(ISightTarget target)
        {
            if (_samplesQueue.ContainsKey(target))
            {
                _samplesQueue[target].Clear();
            }
        }

        public void TakeSample(ISightTarget target)
        {
            if (_samplesQueue.ContainsKey(target) == false)
            {
                _samplesQueue.Add(target, new FixedSizeQueue<SightTargetSampleData>(2));
            }

            _samplesQueue[target].Enqueue(new SightTargetSampleData()
            {
                time = LosUtility.time,
                position = target.transform.position,
                rotation = target.transform.rotation
            });

            //            _samplesQueue[target].Enqueue(new SightTargetSampleData()
            //            {
            //                time = LosUtility.time,
            //                position = target.transform.position + new Vector3(UnityEngine.Random.value * _config.extrapolateRandomNoiseFactor, 0f, UnityEngine.Random.value * _config.extrapolateRandomNoiseFactor),
            //                rotation = target.transform.rotation * Quaternion.Euler(0f, UnityEngine.Random.value * 10 * _config.extrapolateRandomNoiseFactor, 0f)
            //            });
        }

        public void ExtrapolatePath(ISightTarget target, float secondsForward, out SightTargetSampleData[] extrapolationData)
        {
            var samples = _samplesQueue[target];
            if (_extrapolatedData.ContainsKey(target) == false)
            {
                _extrapolatedData.Add(target, new SightTargetSampleData[_config.extrapolateSampleCount]);
            }

            var extrapolationSamples = _extrapolatedData[target];

            var firstSample = samples.First();
            var secondSample = samples.ElementAt(1);
            var dir = secondSample.position - firstSample.position;
            for (int i = 0; i < samples.count; i++)
            {
                extrapolationSamples[i] = new SightTargetSampleData()
                {
                    position = target.transform.position + dir * i,
                    time = LosUtility.time + (i * _config.updateInterval)
                };
            }

            extrapolationData = extrapolationSamples;
//            Debug.Log("Extrapolated path", target.gameObject);
        }
    }
}
