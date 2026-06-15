using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public GameObject deathParticles;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        if (deathParticles != null)
            Instantiate(deathParticles, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
