using UnityEngine;

public class ShotgunRecoil : MonoBehaviour
{
    [Header("Position Recoil")]
    public float recoilBackAmount = 0.05f;   // Rückstoß nach hinten
    public float recoilReturnSpeed = 10f;

    [Header("Rotation Recoil")]
    public float recoilUpAmount = 5f;        // Hochkippen in Grad
    public float rotationReturnSpeed = 10f;

    private Vector3 originalPos;
    private Quaternion originalRot;

    void Start()
    {
        originalPos = transform.localPosition;
        originalRot = transform.localRotation;
    }

    public void ApplyRecoil()
    {
        // Rückstoß nach hinten
        transform.localPosition -= new Vector3(0, 0, recoilBackAmount);

        // Hochkippen (Rotation)
        transform.localRotation *= Quaternion.Euler(-recoilUpAmount, 0, 0);
    }

    void Update()
    {
        // Position zurück
        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            originalPos,
            Time.deltaTime * recoilReturnSpeed
        );

        // Rotation zurück
        transform.localRotation = Quaternion.Lerp(
            transform.localRotation,
            originalRot,
            Time.deltaTime * rotationReturnSpeed
        );
    }
}