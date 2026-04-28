using UnityEngine;
using UnityEngine.AI;

public class enemyai : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    // Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange = 10f;

    // Attacking
    public float timeBetweenAttacks = 1.5f;
    bool alreadyAttacked;

    // States
    public float sightRange = 12f;
    public float attackRange = 1.5f;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();

        // Wichtig: Agent darf am Anfang laufen
        agent.isStopped = false;
    }

    private void Update()
    {
        // Check ranges
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();

        // Debug
        Debug.Log("hasPath: " + agent.hasPath
            + " | pathStatus: " + agent.pathStatus
            + " | isStopped: " + agent.isStopped
            + " | velocity: " + agent.velocity);
    }

    private void Patroling()
    {
        agent.isStopped = false;

        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        Vector3 potentialPoint = new Vector3(
            transform.position.x + randomX,
            transform.position.y + 5f,
            transform.position.z + randomZ
        );

        // Raycast nach unten, um Boden zu finden
        if (Physics.Raycast(potentialPoint, Vector3.down, out RaycastHit hit, 20f, whatIsGround))
        {
            walkPoint = hit.point;
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        agent.isStopped = false;
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        // NICHT stoppen – sonst bleibt er hängen
        agent.isStopped = false;

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            // Attack code here

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
