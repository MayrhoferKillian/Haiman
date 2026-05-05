using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform destination; 
    public GameObject interactionText; // Optional: Der gleiche UI-Text wie beim Schalter
    
    private bool playerInRange = false;
    private GameObject playerToTeleport; // Wir merken uns, wer teleportiert werden soll

    void Update()
    {
        // Wenn jemand auf der Plattform steht UND 'E' drückt
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Sicherheitscheck: Gibt es ein Ziel und einen Spieler?
            if (destination != null && playerToTeleport != null)
            {
                // Teleport-Vorgang
                playerToTeleport.SetActive(false);
                playerToTeleport.transform.position = destination.position;
                playerToTeleport.transform.rotation = destination.rotation;
                playerToTeleport.SetActive(true);

                // Nach dem Teleport alles sauber zurücksetzen
                playerInRange = false;
                if (interactionText != null) 
                {
                    interactionText.SetActive(false);
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            playerToTeleport = other.gameObject; // Den Spieler für das Update() merken
            
            // UI Text einschalten ("Drücke E")
            if (interactionText != null) 
            {
                interactionText.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            playerToTeleport = null;
            
            // UI Text wieder ausschalten, wenn man runtergeht
            if (interactionText != null) 
            {
                interactionText.SetActive(false);
            }
        }
    }
}