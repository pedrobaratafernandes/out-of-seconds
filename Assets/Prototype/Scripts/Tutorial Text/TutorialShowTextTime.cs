using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
public class TutorialShowTextTime : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tutorialText; // referencia para o texto de tutorial

    void Update()
    {
        if (Gamepad.current.buttonNorth.wasPressedThisFrame || Keyboard.current.qKey.wasPressedThisFrame
        || Keyboard.current.altKey.wasPressedThisFrame)
        {
            tutorialText.gameObject.SetActive(false);
        }
    }
}
