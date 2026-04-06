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