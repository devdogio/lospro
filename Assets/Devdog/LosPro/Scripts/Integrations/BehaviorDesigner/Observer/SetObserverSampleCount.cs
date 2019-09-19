#if BEHAVIOR_DESIGNER

using UnityEngine;
using System.Collections;
using BehaviorDesigner;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace Devdog.LosPro.Integration.BehaviorDesigner
{
    [TaskCategory("Los Pro/Observer")]
    [TaskIcon("Assets/LosPro/Scripts/Integrations/BehaviorDesigner/EditorStyles/FSMIcon.png")]
    public class SetObserverSampleCount : Conditional
    {
        public SharedGameObject observer;
        public SharedInt sampleCount;

        public override TaskStatus OnUpdate()
        {
            var o = observer.Value.GetComponent<IObserver>();
            o.config.sampleCount = sampleCount.Value;

            return TaskStatus.Success;
        }
    }
}

#endif