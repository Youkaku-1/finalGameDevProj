using UnityEditorInternal;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    [Header("Roll Manager Reference")]
    public RollManager rollManager;
    public ObstacleManager obstacleManager;
    public BgMovemntManager bgMovemntManager;


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
    public void StopObstacleLoopAndMovement()
    {
        if (obstacleManager != null)
        {
            obstacleManager.StopObstacleLoopAndMovement();
            bgMovemntManager.StopMovement();
            Debug.Log("Obstacle loop and movement stopped via animation event");
        }
        else
        {
            Debug.LogWarning("ObstacleSpawner reference is null!");
        }
    }
}
