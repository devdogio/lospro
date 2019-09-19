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
    public class IsTargetInRange : Conditional
    {
//        public SharedGameObject observer;
        public SharedGameObject target;

        private IObserver _observer;
        public override void OnAwake()
        {
            _observer = gameObject.GetComponent<IObserver>();
        }

        public override TaskStatus OnUpdate()
        {
            var t = target.Value.GetComponent<ISightTarget>();
            if (_observer.sight.IsTargetInRange(t))
            {
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
        }
    }
}

#endif