using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    [Tooltip("Assign the pause menu GameObject (UI Canvas or panel).")]
    public GameObject PauseMenu;

    public PlayerInput playerInput;

    public void OnHello(InputValue value)
    {
        Debug.Log("OnPause called!");
        if (value.isPressed)
        {
            Pause.TogglePause();
            PauseMenu.SetActive(Pause.IsPaused);
        }
    }
}
