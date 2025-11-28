using UnityEngine;

[CreateAssetMenu(fileName = "ObstacleData", menuName = "Game/Obstacle Data")]
public class ObstacleData : ScriptableObject
{
    [Header("Obstacle Prefabs")]
    public GameObject[] obstaclePrefabs;

    [Header("Speed Settings")]
    public float initialSpeed = 5f;
    public float speedIncreaseRate = 0.1f;
    public float maxSpeed = 15f;
}
