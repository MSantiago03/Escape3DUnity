using UnityEngine;
using UnityEngine.AI;

public class GhostControllerAi : MonoBehaviour
{
    // GameObject to follow (e.g., player)
    public Transform target;
    // Waypoints for patrolling
    public Transform[] patrolPoints;
    // Range within which to start chasing the target
    public float chaseRange = 10f; 

    //used to allow ghost to move
    public bool isSummoned = false;

    private NavMeshAgent agent;
    private int currentPatrolIndex = 0;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Start patrolling if patrolPoints array has elements
        if (patrolPoints.Length > 0)
        {
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
        
    }

    private void Update()
    {
        if (isSummoned)
        {
            Debug.Log(isSummoned);
            Debug.Log("The bool worked");
            // Check if the target is within chase range
            if (target != null)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                // if target is within distance - chase
                if (distanceToTarget <= chaseRange)
                {
                    ChaseTarget();
                }
                // else chase between provided points
                else
                {
                    Patrol();
                }
            }
        }
        else {
            
        }

    }

    private void ChaseTarget()
    {
        if (target != null)
        {
            // Set the destination to the target's position for chasing
            agent.SetDestination(target.position);
        }
    }

    private void Patrol()
    {
        // Check if the agent is not currently following a path and has reached the current patrol point
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            if (patrolPoints.Length == 0)
                return;

            // Set the agent to go to the currently selected destination.
            agent.destination = patrolPoints[currentPatrolIndex].position;

            // Choose the next point in the array as the destination,
            // cycling to the start if necessary.
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;

        }
    }

}
