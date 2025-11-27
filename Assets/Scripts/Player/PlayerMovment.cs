using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveDistance = 10f;
    public float smoothTime = 0.2f;

    [Header("Boundaries")]
    public float leftBoundary = -10f;
    public float rightBoundary = 10f;

    [Header("Jump Settings")]
    public float jumpForce = 10f;

    private Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero;
    private Rigidbody rb;
    public Animator animator;

    void Start()
    {
        // Initialize target position to current position
        targetPosition = transform.position;

        // Get Rigidbody component
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("No Rigidbody component found on the player! Jumping requires a Rigidbody.");
        }

        // If no Animator is found, log a warning
        if (animator == null)
        {
            Debug.LogWarning("No Animator component found on the player!");
        }
    }

    void Update()
    {
        // Smoothly move towards target position on X axis only
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        // Only apply smoothing to X position, keep Y and Z from physics
        transform.position = new Vector3(smoothedPosition.x, transform.position.y, transform.position.z);


    }



    // Input System methods - these must have exactly this signature
    public void OnRight(InputValue value)
    {
        if (value.isPressed)
        {
            MoveRight();
        }
    }

    public void OnLeft(InputValue value)
    {
        if (value.isPressed)
        {
            MoveLeft();
        }
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && animator != null && animator.GetBool("isGrounded"))
        {
            Jump();
        }
    }

    void MoveRight()
    {
        // Calculate new X position
        float newX = targetPosition.x + moveDistance;

        // Check if within right boundary
        if (newX <= rightBoundary)
        {
            // Trigger animation
            if (animator != null)
            {
                animator.SetTrigger("Right");
            }

            targetPosition.x = newX;
        }
        else
        {
            // Clamp to boundary if exceeded
            targetPosition.x = rightBoundary;
        }
    }

    void MoveLeft()
    {
        // Calculate new X position
        float newX = targetPosition.x - moveDistance;

        // Check if within left boundary
        if (newX >= leftBoundary)
        {
            // Trigger animation
            if (animator != null)
            {
                animator.SetTrigger("Left");
            }

            targetPosition.x = newX;
        }
        else
        {
            // Clamp to boundary if exceeded
            targetPosition.x = leftBoundary;
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