using UnityEngine;

public class GroundedCheck : MonoBehaviour
{

    public Animator animator;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }


    public void SetGroundedTrue()
    {
        if (animator != null)
        {
            animator.SetBool("isGrounded", true);
        }
    }

    public void SetGroundedFalse()
    {
        if (animator != null)
        {
            animator.SetBool("isGrounded", false);
        }
    }
}
