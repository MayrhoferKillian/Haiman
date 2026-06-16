using UnityEngine;

public class ShotgunFake : MonoBehaviour
{
    public int pelletCount = 8;
    public float spread = 0.1f;
    public float range = 30f;
    public int damagePerPellet = 10;
    public Camera cam;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Shoot();
    }

    void Shoot()
    {
        for (int i = 0; i < pelletCount; i++)
        {
            Vector3 direction = cam.transform.forward;
            direction.x += Random.Range(-spread, spread);
            direction.y += Random.Range(-spread, spread);

            if (Physics.Raycast(cam.transform.position, direction, out RaycastHit hit, range))
            {
                EnemyHealth enemy = hit.collider.GetComponent<EnemyHealth>();
                if (enemy != null)
                    enemy.TakeDamage(damagePerPellet);
            }
        }
    }
}
