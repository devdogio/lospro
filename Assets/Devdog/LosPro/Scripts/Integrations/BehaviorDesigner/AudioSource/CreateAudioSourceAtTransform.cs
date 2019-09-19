#if BEHAVIOR_DESIGNER

using UnityEngine;
using System.Collections;
using BehaviorDesigner;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace Devdog.LosPro.Integration.BehaviorDesigner
{
    [TaskCategory("Los Pro")]
    [TaskIcon("Assets/LosPro/Scripts/Integrations/BehaviorDesigner/EditorStyles/FSMIcon.png")]
    public class CreateAudioSourceAtTransform : CreateAudioSourceBase
    {
        public SharedTransform transform;

        public override TaskStatus OnUpdate()
        {
            CreateAudiosource((transform.Value != null) ? transform.Value.position : gameObject.transform.position);
            return TaskStatus.Success;
        }
    }
}

#endif