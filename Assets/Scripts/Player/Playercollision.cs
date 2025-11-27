using UnityEngine;

public class Playercollision : MonoBehaviour
{
    [Header("Map Loop Reference")]
    public MapLoop mapLoop;

    [Header("Animator Reference")]
    public Animator playerAnimator;

    void Start()
    {
        // If mapLoop reference is not set, try to find it automatically
        if (mapLoop == null)
        {
            mapLoop = FindObjectOfType<MapLoop>();
        }

        // If animator reference is not set, try to get it from this GameObject
        if (playerAnimator == null)
        {
            playerAnimator = GetComponent<Animator>();
        }

        // If still not found, try to find it in children
        if (playerAnimator == null)
        {
            playerAnimator = GetComponentInChildren<Animator>();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            HandleObstacleCollision();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            HandleObstacleCollision();
        }
    }

    void HandleObstacleCollision()
    {
        // Stop the map loop when player collides with obstacle
        if (mapLoop != null)
        {
            mapLoop.StopLoop();
        }

        // Trigger the hit animation
        if (playerAnimator != null)
        {
            playerAnimator.SetTrigger("isHit");
        }
        else
        {
            Debug.LogWarning("Player Animator not found! Make sure to assign the Animator component.");
        }
    }
}