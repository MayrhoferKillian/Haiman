using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    // Diese Coroutine lässt die Kamera für eine bestimmte Zeit vibrieren
    public IEnumerator Shake(float duration, float magnitude)
    {
        // Wir merken uns die ursprüngliche Position der Kamera
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            // Zufällige Wackel-Werte berechnen
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            // Kamera leicht verschieben
            transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);
            elapsed += Time.deltaTime;

            yield return null; // Einen Frame warten
        }

        // Am Ende die Kamera exakt wieder mittig ausrichten!
        transform.localPosition = originalPos;
    }
}