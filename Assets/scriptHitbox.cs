using UnityEngine;

public class PlayerHitbox : MonoBehaviour

{

    public PlayerHealth health;

    private void OnTriggerEnter(Collider other)

    {

        // Versuche, das EnemyAttack-Script vom anderen Objekt zu bekommen

       // EnemyAttack attack = other.GetComponent<EnemyAttack>();

        // Wenn das Objekt ein EnemyAttack-Script hat → Schaden zufügen

       // if (attack != null)

        {

         //   health.TakeDamage(attack.damage);

        }

    }

}

