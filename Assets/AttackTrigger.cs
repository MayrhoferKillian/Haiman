using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    public enemyai enemy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            enemy.playerInAttackRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            enemy.playerInAttackRange = false;
    }
}
