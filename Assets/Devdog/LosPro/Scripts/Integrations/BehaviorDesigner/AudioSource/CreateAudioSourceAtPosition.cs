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
    public class CreateAudioSourceAtPosition : CreateAudioSourceBase
    {
        public SharedVector3 position;

        public override TaskStatus OnUpdate()
        {
            CreateAudiosource(position.Value);
            return TaskStatus.Success;
        }
    }
}

#endif