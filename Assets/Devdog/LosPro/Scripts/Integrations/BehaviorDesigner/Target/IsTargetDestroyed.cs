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
    public class IsTargetDestroyed : Conditional
    {
        public SharedGameObject target;
        
        public override TaskStatus OnUpdate()
        {
            var t = target.Value.GetComponent<ISightTarget>();
            if (t.IsDestroyed())
            {
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
        }
    }
}

#endif