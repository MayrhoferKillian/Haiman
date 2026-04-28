using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactDistance = 3f; 
    public GameObject interactionText;   
    private Camera cam;
    
    // NEU: Ein Gedächtnis, um sich zu merken, ob wir gerade schon etwas anvisieren
    private SwitchController currentTarget;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            SwitchController sController = hit.collider.GetComponent<SwitchController>();

            if (sController != null && sController.enabled)
            {
                // Nur wenn wir das Objekt NEU anvisieren, schalten wir den Text 1x ein
                if (currentTarget != sController)
                {
                    currentTarget = sController;
                    interactionText.SetActive(true);
                }

                // Tastendruck abfragen
                if (Input.GetKeyDown(KeyCode.E))
                {
                    sController.ActivateSwitch();
                    ClearTarget(); // Text weg, nachdem gedrückt wurde
                }
            }
            else
            {
                // Wir treffen etwas, das kein Schalter ist -> Text aus
                ClearTarget();
            }
        }
        else
        {
            // Wir treffen gar nichts (gucken in die Luft) -> Text aus
            ClearTarget();
        }
    }

    // Hilfsfunktion, um den Text sauber 1x auszuschalten und das Gedächtnis zu leeren
    void ClearTarget()
    {
        if (currentTarget != null)
        {
            currentTarget = null;
            interactionText.SetActive(false);
        }
    }
}