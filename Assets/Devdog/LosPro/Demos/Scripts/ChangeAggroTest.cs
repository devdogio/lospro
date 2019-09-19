using UnityEngine;
using System.Collections;

namespace Devdog.LosPro.Demo
{
    public class ChangeAggroTest : MonoBehaviour
    {
        public ObserverAggroBehaviour aggroBehaviour;
        public SightTargetBehaviour target;


        protected void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                aggroBehaviour.ChangeAggroForTarget(target, 5f);
            }
        }
    }
}