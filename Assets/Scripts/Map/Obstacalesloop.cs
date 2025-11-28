using UnityEngine;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Obstacle Data")]
    public ObstacleData obstacleData;

    [Header("Spawn Settings")]
    public Vector3 spawnPosition = new Vector3(0f, 0f, 50f);
    public float endPositionZ = -20f;
    public float minSpawnInterval = 1f;
    public float maxSpawnInterval = 3f;

    [Header("Player Reference")]
    public Animator playerAnimator; // Reference to player's animator

    [Header("Debug")]
    public float currentSpeed;
    public int activeObstaclesCount;
    public bool isMovementStopped = false;

    private List<GameObject> activeObstacles = new List<GameObject>();
    private float spawnTimer;
    private float nextSpawnTime;
    private bool isSpawningActive = true;
    private float previousSpeed; // Store speed before stopping

    void Start()
    {
        if (obstacleData == null)
        {
            Debug.LogError("ObstacleData is not assigned!");
            return;
        }

        // Try to find player animator if not assigned
        if (playerAnimator == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerAnimator = player.GetComponent<Animator>();
            }
        }

        currentSpeed = obstacleData.initialSpeed;
        CalculateNextSpawnTime();
    }

    void Update()
    {
        if (!isSpawningActive || obstacleData == null)
            return;

        CheckForHitTrigger();
        HandleSpawning();
        HandleObstacleMovement();

        // Only increase speed if movement is not stopped
        if (!isMovementStopped)
        {
            IncreaseSpeedOverTime();
        }

        UpdateDebugInfo();
    }

    void CheckForHitTrigger()
    {
        if (playerAnimator != null)
        {
            // Check if "isHit" trigger is active
            if (playerAnimator.GetBool("isHit") || IsTriggerActive("isHit"))
            {
                if (!isMovementStopped)
                {
                    StopMovement();
                }
            }
            else
            {
                if (isMovementStopped)
                {
                    ResumeMovement();
                }
            }
        }
    }

    // Helper method to check if a trigger is active
    bool IsTriggerActive(string triggerName)
    {
        AnimatorControllerParameter[] parameters = playerAnimator.parameters;
        foreach (AnimatorControllerParameter param in parameters)
        {
            if (param.name == triggerName && param.type == AnimatorControllerParameterType.Trigger)
            {
                return playerAnimator.GetCurrentAnimatorStateInfo(0).IsName(triggerName) ||
                       playerAnimator.GetBool(triggerName);
            }
        }
        return false;
    }

    void HandleSpawning()
    {
        // Don't spawn new obstacles if movement is stopped
        if (isMovementStopped)
            return;

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= nextSpawnTime)
        {
            SpawnRandomObstacle();
            spawnTimer = 0f;
            CalculateNextSpawnTime();
        }
    }

    void HandleObstacleMovement()
    {
        // Move all active obstacles and check for deletion
        for (int i = activeObstacles.Count - 1; i >= 0; i--)
        {
            GameObject obstacle = activeObstacles[i];

            if (obstacle == null)
            {
                activeObstacles.RemoveAt(i);
                continue;
            }

            // Only move obstacle if movement is not stopped
            if (!isMovementStopped)
            {
                // Move obstacle toward -Z axis
                obstacle.transform.Translate(0f, 0f, -currentSpeed * Time.deltaTime, Space.World);
            }

            // Check if obstacle should be deleted (still check even when movement is stopped)
            if (obstacle.transform.position.z <= endPositionZ)
            {
                Destroy(obstacle);
                activeObstacles.RemoveAt(i);
            }
        }
    }

    void SpawnRandomObstacle()
    {
        if (obstacleData.obstaclePrefabs == null || obstacleData.obstaclePrefabs.Length == 0)
        {
            Debug.LogWarning("No obstacle prefabs assigned in ObstacleData!");
            return;
        }

        // Select random obstacle prefab
        int randomIndex = Random.Range(0, obstacleData.obstaclePrefabs.Length);
        GameObject selectedPrefab = obstacleData.obstaclePrefabs[randomIndex];

        if (selectedPrefab != null)
        {
            GameObject newObstacle = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);
            activeObstacles.Add(newObstacle);
        }
    }

    void CalculateNextSpawnTime()
    {
        nextSpawnTime = Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    void IncreaseSpeedOverTime()
    {
        if (currentSpeed < obstacleData.maxSpeed)
        {
            currentSpeed += obstacleData.speedIncreaseRate * Time.deltaTime;
            currentSpeed = Mathf.Min(currentSpeed, obstacleData.maxSpeed);
        }
    }

    void UpdateDebugInfo()
    {
        activeObstaclesCount = activeObstacles.Count;
    }

    // Method to stop all movement
    public void StopMovement()
    {
        isMovementStopped = true;
        previousSpeed = currentSpeed; // Store current speed
        currentSpeed = 0f; // Set speed to zero
    }

    // Method to resume movement
    public void ResumeMovement()
    {
        isMovementStopped = false;
        currentSpeed = previousSpeed; // Restore previous speed
    }

    // Public methods to control spawning
    public void StopSpawning()
    {
        isSpawningActive = false;
    }

    public void StartSpawning()
    {
        isSpawningActive = true;
    }

    public void ResetSpeed()
    {
        currentSpeed = obstacleData.initialSpeed;
        isMovementStopped = false;
    }

    public void ClearAllObstacles()
    {
        foreach (GameObject obstacle in activeObstacles)
        {
            if (obstacle != null)
            {
                Destroy(obstacle);
            }
        }
        activeObstacles.Clear();
    }

    // Clean up when destroyed
    void OnDestroy()
    {
        ClearAllObstacles();
    }
}