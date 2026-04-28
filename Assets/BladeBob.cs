using UnityEngine;

public class BladeBob : MonoBehaviour
{
    public float amplitude = 0.02f; // The height of the bob (distance from center)
    public float period = 5f;     // The time it takes to complete one full cycle

    private Vector3 startLocalPos;

    void Start()
    {
        startLocalPos = transform.localPosition;
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        bool isMoving = Mathf.Abs(x) > 0.1f || Mathf.Abs(z) > 0.1f;

        float amplitudeY = isMoving ? 0.05f : 0.02f;
        float amplitudeX = isMoving ? 0.03f : 0.0f;
        float period = isMoving ? 2f : 5f;

        float theta = Time.time * (2f * Mathf.PI / period);

        // Links ↔ Rechts Bewegung
        float bobX = amplitudeX * Mathf.Sin(theta);

        // Immer nach unten (U-Form)
        float bobY = -amplitudeY * Mathf.Abs(Mathf.Sin(theta));

        transform.localPosition = startLocalPos + new Vector3(bobX, bobY, 0f);
    }
}
