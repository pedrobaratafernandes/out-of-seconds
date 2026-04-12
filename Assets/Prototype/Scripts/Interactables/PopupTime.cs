using UnityEngine;
using UnityEngine.InputSystem;

public class PopupTime : MonoBehaviour
{
    private Animator timerAnimator;
    private bool isPopupVisible = false;

    private void Start()
    {
        timerAnimator = GetComponent<Animator>();
    }

    public void Update()
    {
        bool gamepadPressed = Gamepad.current != null && Gamepad.current.buttonNorth.wasPressedThisFrame;

        // 2. Check keyboard keys
        bool keyboardPressed = Keyboard.current.qKey.wasPressedThisFrame || Keyboard.current.altKey.wasPressedThisFrame;

        if (gamepadPressed || keyboardPressed)
        {
            if (Keyboard.current.qKey.wasPressedThisFrame || Keyboard.current.altKey.wasPressedThisFrame || Gamepad.current.buttonNorth.wasPressedThisFrame)
            {
                isPopupVisible = !isPopupVisible;

                if (timerAnimator != null)
                {
                    timerAnimator.SetBool("Show", isPopupVisible);
                }
            }
        }
    }
}