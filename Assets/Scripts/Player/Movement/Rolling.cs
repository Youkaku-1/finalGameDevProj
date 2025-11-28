using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRoll : MonoBehaviour
{
    [Header("References")]
    public Animator animator;
    public RollManager rollManager;

    void Start()
    {
        // Find animator in children if not assigned
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
            if (animator == null)
            {
                Debug.LogWarning("No Animator component found in children!");
            }
        }

        // Find RollManager if not assigned
        if (rollManager == null)
        {
            rollManager = FindObjectOfType<RollManager>();
            if (rollManager == null)
            {
                Debug.LogError("No RollManager found in scene!");
            }
        }
    }

    // Input System method
    public void OnRoll(InputValue value)
    {
        if (value.isPressed && rollManager != null && !rollManager.IsRolling())
        {
            Roll();
        }
    }

    void Roll()
    {
        // Start roll through RollManager
        if (rollManager != null)
        {
            rollManager.StartRoll();
        }

        // Trigger roll animation
        if (animator != null)
        {
            animator.SetTrigger("isRolling");
        }
    }


}