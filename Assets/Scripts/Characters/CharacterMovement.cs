using UnityEngine;
using UnityEngine.AI;

namespace Subvrsive.Combat.Characters
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private float rotationSpeed = 500f;

        private NavMeshAgent agent;

        public Transform targetPosition;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            ApplyMovementSettings();
        }

        private void ApplyMovementSettings()
        {
            agent.speed = moveSpeed;
            agent.angularSpeed = rotationSpeed;
        }

        private void Update()
        {
            if(targetPosition != null)
            {
                MoveTo(targetPosition.transform.position);
            }

        }

        private void MoveTo(Vector3 destination)
        {
            if (agent.enabled && agent.isOnNavMesh)
            {
                agent.SetDestination(destination);
            }
        }

    }
}


