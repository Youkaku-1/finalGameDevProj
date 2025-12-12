using UnityEngine;
using TMPro;

public class Playercollision : MonoBehaviour
{
    [Header("Coin Data")]
    public CoinData coinData; // Reference to your ScriptableObject

    [Header("Map Loop Reference")]
    public MapLoop mapLoop;

    [Header("Animator Reference")]
    public Animator animator;

    [Header("Coin UI")]
    public TextMeshProUGUI coinText;

    // Local coin variable
    private int coin = 0;

    void Start()
    {
        // If mapLoop reference is not set, try to find it automatically
        if (mapLoop == null)
        {
            mapLoop = FindObjectOfType<MapLoop>();
        }

        // If animator reference is not set, try to get it from this GameObject
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        // If still not found, try to find it in children
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
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
        if (animator != null)
        {
            animator.SetTrigger("isHit");
        }
        else
        {
            Debug.LogWarning("Player Animator not found! Make sure to assign the Animator component.");
        }
    }

    void CollectCoin(GameObject coinObject, int value)
    {
        // Update local coin variable
        coin += value;

        // Update ScriptableObject to keep it in sync
        if (coinData != null)
        {
            coinData.coinCount += value;
        }
        else
        {
            Debug.LogWarning("CoinData ScriptableObject is not assigned!");
        }

        // Update UI using local variable
        UpdateCoinUI();

        // Destroy the coin object
        Destroy(coinObject);
    }

    void UpdateCoinUI()
    {
        if (coinText != null)
        {
            // Use the local coin variable
            coinText.text = coin.ToString();
        }
    }

    // Public method to get current coin count
    public int GetCoinCount()
    {
        return coin;
    }

    // Public method to spend coins
    public bool SpendCoins(int amount)
    {
        if (coin >= amount)
        {
            coin -= amount;

            // Update ScriptableObject
            if (coinData != null)
            {
                coinData.coinCount = coin;
            }

            UpdateCoinUI();
            return true;
        }
        return false;
    }

    // Public method to reset coins
    public void ResetCoins()
    {
        coin = 0;

        // Update ScriptableObject
        if (coinData != null)
        {
            coinData.coinCount = coin;
        }

        UpdateCoinUI();
    }
}