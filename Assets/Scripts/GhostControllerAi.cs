using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GhostControllerAi : MonoBehaviour
{
    // GameObject to follow (e.g., player)
    public Transform target;
    // Waypoints for patrolling
    public Transform[] patrolPoints;
    // Range within which to start chasing the target
    public float chaseRange = 10f;
    // Audio Clip
    public AudioSource audioSource;
    public AudioClip hit;
    public AudioClip ghost;


    // Seconds Ghost is frozen for
    public float seconds = 4;

    //used to allow ghost to move
    public bool isSummoned = false;
    // Used to to play audio clip
    public bool isMusicOn = false;
    //used to freeze the ghost
    public bool isFrozen = false;

    private NavMeshAgent agent;
    private int currentPatrolIndex = 0;

    public Image flashPanel;  // Reference to a UI Image component

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Start patrolling if patrolPoints array has elements
        if (patrolPoints.Length > 0)
        {
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }

        audioSource = GetComponent<AudioSource>();

    }

    private void Update()
    {
        if (isSummoned && !isFrozen)
        {
            // Check if the target is within chase range
            if (target != null)
            {
                //play music
                if (!isMusicOn)
                {
                    isMusicOn = true;
                    audioSource.clip = ghost;
                    audioSource.Play();
                }

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !isFrozen)
        {
            GameVariables.lives -= 1;
            audioSource.PlayOneShot(hit);
            //audioSource.Play();

            // Freeze the ghost for a specified amount of time
            StartCoroutine(FreezeForSeconds(seconds));
        }
    }

    IEnumerator FreezeForSeconds(float seconds)
    {
        isFrozen = true;
        agent.isStopped = true; // Stop the ghost's movement
        yield return new WaitForSeconds(seconds);
        agent.isStopped = false; // Resume the ghost's movement
        isFrozen = false;
    }

}
