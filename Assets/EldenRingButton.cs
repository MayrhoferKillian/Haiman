using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; // <-- NEU: Wichtig für 'Image'
using TMPro; // Wichtig für TextMeshPro

public class EldenRingButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("UI Referenzen")]
    public TextMeshProUGUI buttonText;
    public Image glowFlareImage; // <-- NEU: Hier kommt das Glow-Bild rein

    [Header("Einstellungen")]
    public Color normalColor = new Color(0.6f, 0.6f, 0.6f, 1f); // Grau
    public Color hoverColor = new Color(1f, 0.8f, 0.2f, 1f); // Gold

    void Start()
    {
        // Setzt den Text am Anfang auf die normale Farbe
        if (buttonText != null)
        {
            buttonText.color = normalColor;
        }

        // NEU: Deaktiviert den Glow-Nebel am Anfang, damit er unsichtbar ist
        if (glowFlareImage != null)
        {
            glowFlareImage.gameObject.SetActive(false);
        }
    }

    // Wird automatisch gefeuert, wenn die Maus den Button berührt
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Text umfärben
        if (buttonText != null)
        {
            buttonText.color = hoverColor;
        }

        // NEU: Glow-Nebel einschalten
        if (glowFlareImage != null)
        {
            glowFlareImage.gameObject.SetActive(true);
        }
    }

    // Wird gefeuert, wenn die Maus den Button verlässt
    public void OnPointerExit(PointerEventData eventData)
    {
        // Text zurücksetzen
        if (buttonText != null)
        {
            buttonText.color = normalColor;
        }

        // NEU: Glow-Nebel wieder ausschalten
        if (glowFlareImage != null)
        {
            glowFlareImage.gameObject.SetActive(false);
        }
    }
}