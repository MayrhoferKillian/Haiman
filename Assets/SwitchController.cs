using UnityEngine;
using System.Collections;

public class SwitchController : MonoBehaviour
{
    [Header("Kameras")]
    public GameObject playerCamera;   
    public GameObject cutsceneCamera; 

    [Header("UI Elemente")]
    public GameObject interactionText; 
    public GameObject skipText;        

    [Header("Ketten-Animation")]
    public float pullDistance = 0.5f; 
    public float pullSpeed = 2f;

    [Header("Mauer & Erdbeben")]
    public GameObject wallToHide;
    public float wallSinkDistance = 5f;
    public float wallSinkSpeed = 3f;

    private Vector3 originalPosition;
    private bool isCutsceneActive = false;
    private bool canSkip = false;

    void Start()
    {
        originalPosition = transform.position;
        if (skipText != null) skipText.SetActive(false);
        if (cutsceneCamera != null) cutsceneCamera.SetActive(false);
    }

    public void ActivateSwitch()
    {
        if (!isCutsceneActive)
        {
            // FIX 1: Hitbox (Collider) der Kette sofort abschalten!
            // So verschwindet der Raycast-Text sofort und man kann nicht 2x klicken.
            Collider col = GetComponent<Collider>();
            if (col != null) col.enabled = false;

            StartCoroutine(RunCutscene());
        }
    }

    IEnumerator RunCutscene()
    {
        isCutsceneActive = true;

        if (interactionText != null) interactionText.SetActive(false);

        // Kette nach unten ziehen
        Vector3 targetPosition = originalPosition - new Vector3(0, pullDistance, 0);
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, pullSpeed * Time.deltaTime);
            yield return null; 
        }

        // FIX 2: Erst die neue Kamera an, DANN die alte aus (verhindert den Fehler)
        cutsceneCamera.SetActive(true);
        
        // Sicherheitshalber auch die Kamera-Komponente direkt erzwingen
        Camera cutCam = cutsceneCamera.GetComponent<Camera>();
        if (cutCam != null) cutCam.enabled = true;

        playerCamera.SetActive(false);

        CameraShake shake = cutsceneCamera.GetComponent<CameraShake>();
        if (shake != null) StartCoroutine(shake.Shake(4f, 0.2f));

        StartCoroutine(AnimateWallSink());

        yield return new WaitForSeconds(3f);

        canSkip = true;
        if (skipText != null) skipText.SetActive(true);

        float maxDuration = 4f; 
        float timer = 0;
        while (timer < maxDuration)
        {
            if (Input.GetKeyDown(KeyCode.Space) && canSkip) break; 
            timer += Time.deltaTime;
            yield return null;
        }

        EndCutscene();
    }

    IEnumerator AnimateWallSink()
    {
        if (wallToHide == null) yield break;
        Vector3 endPos = wallToHide.transform.position - new Vector3(0, wallSinkDistance, 0);
        
        while (Vector3.Distance(wallToHide.transform.position, endPos) > 0.05f)
        {
            wallToHide.transform.position = Vector3.MoveTowards(wallToHide.transform.position, endPos, wallSinkSpeed * Time.deltaTime);
            yield return null;
        }
        wallToHide.SetActive(false); 
    }

    void EndCutscene()
    {
        if (wallToHide != null) wallToHide.SetActive(false);

        cutsceneCamera.SetActive(false);
        playerCamera.SetActive(true);
        
        if (skipText != null) skipText.SetActive(false);

        StartCoroutine(ResetChain());
    }

    IEnumerator ResetChain()
    {
        while (Vector3.Distance(transform.position, originalPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, (pullSpeed * 1.5f) * Time.deltaTime);
            yield return null;
        }
        
        this.enabled = false; 
    }
}