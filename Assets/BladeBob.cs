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
        float theta = Time.time * (2f * Mathf.PI / period);
        float distance = amplitude * Mathf.Sin(theta);

        transform.localPosition = startLocalPos + Vector3.up * distance;
    }
}
