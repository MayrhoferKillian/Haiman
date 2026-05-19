using UnityEngine;
using UnityEngine.EventSystems;
using TMPro; // Wichtig für TextMeshPro

public class EldenRingButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Settings")]
    public TextMeshProUGUI buttonText;

    // Du kannst die Farben später bequem im Inspector ändern
    public Color normalColor = new Color(0.6f, 0.6f, 0.6f, 1f); // Grau
    public Color hoverColor = new Color(1f, 0.8f, 0.2f, 1f); // Gold

    void Start()
    {
        // Setzt den Text am Anfang auf die normale Farbe
        if (buttonText != null)
        {
            buttonText.color = normalColor;
        }
    }

    // Wird automatisch gefeuert, wenn die Maus den Button berührt
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonText != null)
        {
            buttonText.color = hoverColor;
        }
    }

    // Wird gefeuert, wenn die Maus den Button verlässt
    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonText != null)
        {
            buttonText.color = normalColor;
        }
    }
}