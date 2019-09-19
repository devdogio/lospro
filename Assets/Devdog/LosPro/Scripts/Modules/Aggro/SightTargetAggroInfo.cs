using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Devdog.LosPro
{
    public class SightTargetAggroInfo : IEquatable<SightTargetAggroInfo>
    {
        public ISightTarget target { get; protected set; }

        /// <summary>
        /// The base (start) value.
        /// </summary>
        public float baseValue { get; set; }

        /// <summary>
        /// The sum value of all modules.
        /// </summary>
        public float modulesValue { get; set; }

        /// <summary>
        /// An additional value that can be added on. Useful for run-time info.
        /// </summary>
        public float addValue { get; set; }

        /// <summary>
        /// The final value is multiplied by the multiplier to allow for % based changes.
        /// </summary>
        public float multiplier { get; set; }

        public float finalValue
        {
            get { return (baseValue + modulesValue + addValue) * multiplier; }
        }

        public SightTargetAggroInfo(ISightTarget target, float baseValue)
        {
            this.target = target;
            this.baseValue = baseValue;
            this.multiplier = 1f;
        }

        public bool Equals(SightTargetAggroInfo other)
        {
            return Mathf.Approximately(other.finalValue, finalValue);
        }

        public override string ToString()
        {
            return Math.Round(finalValue, 2) + "(" + Math.Round(baseValue, 2) + " + " + Math.Round(modulesValue, 2) + " + " + Math.Round(addValue, 2) + ") * " + multiplier;
        }
    }
}
