using UnityEngine;
using System.Collections;

#if UNITY_5_5_OR_NEWER
using UnityEngine.AI;
#endif

namespace Devdog.LosPro
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class RandomPatrol : MonoBehaviour
    {

        public float patrolRange = 10f;

        private Vector3 _spawnPosition;
        private Vector3 _aimPosition;

        private NavMeshAgent _navMeshAgent;

        protected IEnumerator Start()
        {
            _spawnPosition = transform.position;
            _navMeshAgent = GetComponent<NavMeshAgent>();

            var wait = new WaitForSeconds(5f);
            while (true)
            {
                UpdateAimLocation();
                yield return wait;
            }
        }

        private void UpdateAimLocation()
        {
            var r = Random.insideUnitCircle;
            r *= patrolRange;
            _aimPosition = _spawnPosition + new Vector3(r.x, _spawnPosition.y, r.y);
        }

        protected void Update()
        {
            _navMeshAgent.SetDestination(_aimPosition);
        }
    }
}