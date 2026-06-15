using UnityEngine;

public class AttackScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Animator animator;
    void Start()
    {

    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // 1 = Rechtsklick
        {
            animator.SetTrigger("PlayAttack");
        }
    }

}
