using UnityEngine;
using UnityEngine.InputSystem;

public class LevelTransition : MonoBehaviour
{
    public Animator timerAnimator;

    private bool isPopupVisible = false; // estado visivel da UI do tempo

    // Player Input tecla Q
    void OnViewUiTimer(InputValue value)
    {
        // verifica se a tecla foi premida
        if (value.isPressed)
        {
            ToggleTransition();
        }
    }

    // mostrar estado visivel ou esconder a UI do tempo
    void ToggleTransition()
    {
        // trocar o estado de verdadeiro para falso e falso para verdadeiro
        isPopupVisible = !isPopupVisible;

        // animacao slide up e slide down do tempo
        if (timerAnimator != null)
        {
            if (isPopupVisible)
                timerAnimator.SetBool("Show", true);
            else
                timerAnimator.SetBool("Show", false);
        }
    }
}