using UnityEngine;
using UnityEngine.AI;

public class enemyai : MonoBehaviour
{
    private Animator anim;
    public NavMeshAgent agent;
    public Transform player;

    // Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange = 10f;

    // Attacking
    public float timeBetweenAttacks = 1.5f;
    bool alreadyAttacked;

    // States (werden NUR von Triggern gesetzt)
    public bool playerInSightRange;
    public bool playerInAttackRange;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = false;
    }

    private void Update()
    {
        if (!playerInSightRange && !playerInAttackRange) 
            Patroling();

        if (playerInSightRange && !playerInAttackRange) 
            ChasePlayer();

        if (playerInSightRange && playerInAttackRange) 
            AttackPlayer();

        if (agent.isStopped == false)
            anim.SetTrigger("Walk");

        Debug.Log("hasPath: " + agent.hasPath
            + " | pathStatus: " + agent.pathStatus
            + " | isStopped: " + agent.isStopped
            + " | velocity: " + agent.velocity);
    }

    private void Patroling()
    {
        agent.isStopped = false;

        if (!walkPointSet) 
            SearchWalkPoint();

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
        transform.position.y + 2f,
        transform.position.z + randomZ
    );

    if (Physics.Raycast(potentialPoint, Vector3.down, out RaycastHit hit, 10f))
    {
        NavMeshHit navHit;
        if (NavMesh.SamplePosition(hit.point, out navHit, 1f, NavMesh.AllAreas))
        {
            walkPoint = navHit.position;
            walkPointSet = true;
        }
    }
}


    private void ChasePlayer()
    {
        agent.isStopped = false;
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
{
    agent.isStopped = true;
    transform.LookAt(player);

    if (!alreadyAttacked)
    {
            // Animation starten
            anim.SetTrigger("Attack");

            // Schaden aus EnemyAttack.cs holen
            int dmg = GetComponent<EnemyAttack>().damage;

        // Schaden am Player anwenden
        player.GetComponent<PlayerHealth>().TakeDamage(dmg);

        alreadyAttacked = true;
        Invoke(nameof(ResetAttack), 2f); // alle 2 Sekunden
    }
}


    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}

