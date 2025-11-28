using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    [Header("Roll Manager Reference")]
    public RollManager rollManager;


    public Animator animator;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }


    public void SetGroundedFalse()
    {
        if (animator != null)
        {
            animator.SetBool("isGrounded", false);
        }
    }

    public void SetGroundeTrue()
    {
        if (animator != null)
        {
            animator.SetBool("isGrounded", true);
        }
    }


    public void EndRoll()
    {
        if (rollManager != null)
        {
            rollManager.EndRoll();
        }
        else
        {
            Debug.LogWarning("RollManager reference is missing!");
        }
    }

}
