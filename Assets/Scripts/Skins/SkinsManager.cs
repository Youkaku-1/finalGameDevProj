using UnityEngine;
using UnityEngine.UI;

public class SkinButtonAssigner : MonoBehaviour
{
    [Header("Skin Storage")]
    public SkinStorage skinStorage;

    [Header("Skin to Assign")]
    public GameObject skinToAssign;

    [Header("UI Button")]
    public Button assignButton;

    void Start()
    {
        // Try to get button from this GameObject if not assigned
        if (assignButton == null)
        {
            assignButton = GetComponent<Button>();
        }

        // If still not found, try to find in children
        if (assignButton == null)
        {
            assignButton = GetComponentInChildren<Button>();
        }

        // Add listener if button exists
        if (assignButton != null)
        {
            assignButton.onClick.AddListener(AssignSkin);
        }
        else
        {
            Debug.LogError("No Button found! Make sure this script is on a Button GameObject.");
        }
    }

    void AssignSkin()
    {
        // Check if ScriptableObject is assigned
        if (skinStorage == null)
        {
            Debug.LogError("SkinStorage is not assigned!");
            return;
        }

        // Check if skin is assigned
        if (skinToAssign == null)
        {
            Debug.LogError("No skin to assign!");
            return;
        }

        // Try to assign the skin
        try
        {
            skinStorage.currentSkin = skinToAssign;
            Debug.Log($"Successfully assigned skin: {skinToAssign.name}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to assign skin: {e.Message}");
            Debug.LogError($"skinStorage type: {skinStorage.GetType()}");
            Debug.LogError($"skinToAssign type: {skinToAssign.GetType()}");
        }
    }

    // Optional: Add this method to see the ScriptableObject in the inspector
    void OnValidate()
    {
        if (skinStorage != null)
        {
            Debug.Log($"SkinStorage type confirmed: {skinStorage.GetType()}");
        }
    }
}