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
    public class GetTargetVisibleForSeconds : Conditional
    {
        public SharedGameObject target;

        [RequiredField]
        public SharedFloat result;

        private IObserver _observer;
        public override void OnAwake()
        {
            _observer = gameObject.GetComponent<IObserver>();
        }

        public override TaskStatus OnUpdate()
        {
            var t = target.Value.GetComponent<ISightTarget>();
            var info = _observer.sight.GetInRangeTargetInfo(t);
            result.Value = info.visibleForSeconds;

            return TaskStatus.Success;
        }
    }
}

#endif