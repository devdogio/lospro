#if PLAYMAKER

using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;

namespace Devdog.LosPro.Integration.PlayMaker
{

    [ActionCategory("Los Pro")]
    [HutongGames.PlayMaker.Tooltip("Log a target to the console.")]
    public class ConsoleLogTarget : FsmStateAction
    {
        public FsmString msg;
        public FsmGameObject sightTarget;
        private ISightTarget _sightTarget;

        public override void Init(FsmState state)
        {
            base.Init(state);

            if (sightTarget != null && sightTarget.Value != null)
            {
                _sightTarget = sightTarget.Value.GetComponent<ISightTarget>();
                if (_sightTarget == null)
                {
                    LogError("No target found, can't detect targets!");
                }
            }
        }

        public override void Reset()
        {

        }

        public override void OnEnter()
        {
            if (_sightTarget != null)
            {
                UnityEngine.Debug.Log(msg.Value + " - " + _sightTarget.name, _sightTarget.gameObject);
            }

            Finish();
        }
    }
}

#endif