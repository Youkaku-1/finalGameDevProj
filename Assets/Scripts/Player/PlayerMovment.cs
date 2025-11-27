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

    private Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero;
    public Animator animator;

    void Start()
    {
        // Initialize target position to current position
        targetPosition = transform.position;


        // If no Animator is found, log a warning
        if (animator == null)
        {
            Debug.LogWarning("No Animator component found on the player!");
        }
    }

    void Update()
    {
        // Smoothly move towards target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
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


}