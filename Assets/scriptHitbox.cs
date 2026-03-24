using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    public PlayerHealth health;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyAttack"))
        {
            health.TakeDamage(10);
        }
    }
}
