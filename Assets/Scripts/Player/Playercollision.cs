using UnityEngine;
using TMPro;

public class Playercollision : MonoBehaviour
{
    [Header("Coin Data")]
    public CoinData coinData; // Reference to your ScriptableObject

    [Header("Map Loop Reference")]
    public MapLoop mapLoop;

    [Header("Animator Reference")]
    public Animator playerAnimator;

    [Header("Coin UI")]
    public TextMeshProUGUI coinText;

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

        // Update coin UI on start
        UpdateCoinUI();
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
        else if (other.CompareTag("Coin"))
        {
            CollectCoin(other.gameObject, 1);
        }
        else if (other.CompareTag("Coin10"))
        {
            CollectCoin(other.gameObject, 10);
        }
        else if (other.CompareTag("Coin50"))
        {
            CollectCoin(other.gameObject, 50);
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

    void CollectCoin(GameObject coinObject, int value)
    {
        // Increase coin count in ScriptableObject
        if (coinData != null)
        {
            coinData.coinCount += value;
        }
        else
        {
            Debug.LogWarning("CoinData ScriptableObject is not assigned!");
        }

        // Update UI
        UpdateCoinUI();

        // Destroy the coin object
        Destroy(coinObject);
    }

    void UpdateCoinUI()
    {
        if (coinText != null && coinData != null)
        {
            coinText.text = coinData.coinCount.ToString();
        }
    }

    // Public method to get current coin count
    public int GetCoinCount()
    {
        if (coinData != null)
        {
            return coinData.coinCount;
        }
        return 0;
    }

    // Public method to spend coins
    public bool SpendCoins(int amount)
    {
        if (coinData != null && coinData.coinCount >= amount)
        {
            coinData.coinCount -= amount;
            UpdateCoinUI();
            return true;
        }
        return false;
    }

    // Public method to reset coins
    public void ResetCoins()
    {
        if (coinData != null)
        {
            coinData.coinCount = 0;
            UpdateCoinUI();
        }
    }
}