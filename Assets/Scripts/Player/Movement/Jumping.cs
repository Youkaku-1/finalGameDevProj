using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    [Header("Jump Settings")]
    public float jumpForce = 10f;

    [Header("References")]
    public Animator animator;

    private Rigidbody rb;

    void Start()
    {
        // Get Rigidbody component
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("No Rigidbody component found on the player! Jumping requires a Rigidbody.");
        }

        // Find animator in children if not assigned
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
            if (animator == null)
            {
                Debug.LogWarning("No Animator component found in children!");
            }
        }
    }

    // Input System method
    public void OnJump(InputValue value)
    {
        if (value.isPressed && animator != null && animator.GetBool("isGrounded"))
        {
            Jump();
        }
    }

    void Jump()
    {
        // Apply jump force
        if (rb != null)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // Trigger jump animation
        if (animator != null)
        {
            animator.SetTrigger("Jump");
        }
    }
}