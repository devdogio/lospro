#if BEHAVIOR_DESIGNER

using UnityEngine;
using System.Collections;
using BehaviorDesigner;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace Devdog.LosPro.Integration.BehaviorDesigner
{
    [TaskCategory("Los Pro/Target")]
    [TaskIcon("Assets/LosPro/Scripts/Integrations/BehaviorDesigner/EditorStyles/FSMIcon.png")]
    public class SetTargetVisibilityMultiplier : Conditional
    {
        public SharedGameObject target;
        public SharedFloat visibilityMultiplier;

        public override TaskStatus OnUpdate()
        {
            var o = target.Value.GetComponent<ISightTarget>();
            o.config.visibilityMultiplier = visibilityMultiplier.Value;

            return TaskStatus.Success;
        }
    }
}

#endif