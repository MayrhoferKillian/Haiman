using UnityEngine;

public class EnemySight : MonoBehaviour
{
    public enemyai enemy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            enemy.playerInSightRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            enemy.playerInSightRange = false;
    }
}
