using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Devdog.LosPro
{
    [System.Serializable]
    public class AudioEmitterConfig
    {
        [TargetCategory]
        public int targetCategory;

        public float emitRange = 10f;
        public float emitInterval = 1f;
        public bool debug = false;
    }
}
