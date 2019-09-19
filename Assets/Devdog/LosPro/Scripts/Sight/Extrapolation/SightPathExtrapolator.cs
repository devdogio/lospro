using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Devdog.LosPro
{
    public partial class SightPathExtrapolator : ISightPathExtrapolator
    {
        private readonly Dictionary<ISightTarget, FixedSizeQueue<SightTargetSampleData>> _samplesQueue;
        private readonly Dictionary<ISightTarget, SightTargetSampleData[]> _extrapolatedData; 

        private readonly SightConfiguration _config;

        public SightPathExtrapolator(SightConfiguration config)
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
                _samplesQueue.Add(target, new FixedSizeQueue<SightTargetSampleData>(_config.extrapolateSampleCount));
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
                _extrapolatedData.Add(target, new SightTargetSampleData[samples.maxCount]);
            }

            var extrapolationSamples = _extrapolatedData[target];

            int counter = 0;
            var firstSample = samples.FirstOrDefault();
            foreach (var currentSample in samples)
            {
                extrapolationSamples[counter] = new SightTargetSampleData()
                {
                    position = target.transform.position + (currentSample.position - firstSample.position),
                    rotation = target.transform.rotation * (Quaternion.Inverse(currentSample.rotation) * firstSample.rotation), // * by inverse gives dif.
                    time = LosUtility.time + (currentSample.time - firstSample.time)
                };

                counter++;
            }

            extrapolationData = extrapolationSamples;
        }
    }
}
