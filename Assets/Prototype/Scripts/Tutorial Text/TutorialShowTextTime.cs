using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class TutorialShowTextTime : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tutorialText; // referencia para o texto de tutorial

    void Update()
    {
        // 1. Check if the gamepad exists first
        bool gamepadPressed = Gamepad.current != null && Gamepad.current.buttonNorth.wasPressedThisFrame;

        // 2. Check keyboard keys
        bool keyboardPressed = Keyboard.current.qKey.wasPressedThisFrame || Keyboard.current.altKey.wasPressedThisFrame;

        if (gamepadPressed || keyboardPressed)
        {
            if (tutorialText != null)
            {
                tutorialText.gameObject.SetActive(false);
            }
        }
    }
}
