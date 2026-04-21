using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform destination; 
    
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.gameObject.SetActive(false); 
            other.transform.position = destination.position; 
            other.gameObject.SetActive(true); 
        }
    }
}