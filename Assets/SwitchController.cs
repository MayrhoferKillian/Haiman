using UnityEngine;
using System.Collections;

public class SwitchController : MonoBehaviour
{
    [Header("Verknüpfungen")]
    public GameObject wallToHide; 
    public CameraShake cameraShake; // NEU: Hier kommt die Kamera rein!

    [Header("Ketten-Animation")]
    public float pullDistance = 0.5f; 
    public float pullSpeed = 2f;      

    [Header("Mauer-Erdbeben")]
    public float wallSinkDistance = 5f; // Wie tief sinkt die Mauer in den Boden?
    public float wallSinkSpeed = 1.5f;  // Wie schnell sinkt sie?
    public float shakeDuration = 2f;    // Wie lange wackelt das Bild?
    public float shakeMagnitude = 0.1f; // Wie heftig wackelt es?

    private Vector3 originalPosition;
    private bool isPulling = false;   

    void Start()
    {
        originalPosition = transform.position;
    }

    public void ActivateSwitch()
    {
        if (!isPulling)
        {
            StartCoroutine(PullAndCrumbleAnimation());
        }
    }

    IEnumerator PullAndCrumbleAnimation()
    {
        isPulling = true;
        this.enabled = false; // Direkt deaktivieren, damit man nicht spammen kann

        // 1. Kette nach unten ziehen
        Vector3 targetPosition = originalPosition - new Vector3(0, pullDistance, 0);
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, pullSpeed * Time.deltaTime);
            yield return null; 
        }

        // 2. DAS ERDBEBEN STARTET
        if (wallToHide != null)
        {
            // Starte das Kamera-Wackeln (wenn eine Kamera verlinkt ist)
            if (cameraShake != null)
            {
                StartCoroutine(cameraShake.Shake(shakeDuration, shakeMagnitude));
            }

            // Zielpunkt für die Mauer berechnen (tief im Boden)
            Vector3 wallStartPos = wallToHide.transform.position;
            Vector3 wallEndPos = wallStartPos - new Vector3(0, wallSinkDistance, 0);

            // Mauer langsam in den Boden sinken lassen
            while (Vector3.Distance(wallToHide.transform.position, wallEndPos) > 0.01f)
            {
                wallToHide.transform.position = Vector3.MoveTowards(wallToHide.transform.position, wallEndPos, wallSinkSpeed * Time.deltaTime);
                yield return null;
            }
            
            // Wenn sie komplett im Boden versunken ist, deaktivieren wir sie endgültig
            wallToHide.SetActive(false);
        }

        // 3. Kette schnalzt wieder nach oben
        while (Vector3.Distance(transform.position, originalPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, (pullSpeed * 1.5f) * Time.deltaTime);
            yield return null;
        }
    }
}