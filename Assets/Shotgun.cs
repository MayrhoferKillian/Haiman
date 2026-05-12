using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public ParticleSystem muzzleflash;
    public ShotgunRecoil recoil; // Referenz zum Recoil Script

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            muzzleflash.Play();
            recoil.ApplyRecoil(); // Rückstoß auslösen
        }
    }
}