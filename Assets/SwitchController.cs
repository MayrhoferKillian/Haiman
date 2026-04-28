using UnityEngine;

public class SwitchController : MonoBehaviour
{
    public GameObject wallToHide; 

    // Diese Funktion wird jetzt vom Kamera-Skript aufgerufen
    public void ActivateSwitch()
    {
        if (wallToHide != null)
        {
            wallToHide.SetActive(false);
            Debug.Log("Kette gezogen!");
            
            // Skript deaktivieren, damit man es nicht mehrfach nutzt
            this.enabled = false; 
        }
    }
}