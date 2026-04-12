using UnityEngine;
using UnityEngine.InputSystem;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] private Animator doorAnimator;
    public bool isOpen = false;
    private bool isPlayerNearby = false;
    
    private void Update()
    {
        // player esta perto da porta e aberta E ele carregou W ou UP
        if (isPlayerNearby && !isOpen)
        {
            // Verifica se a tecla configurada no Input System (ou W/Up) foi premida
            if (Keyboard.current.eKey.wasPressedThisFrame || Keyboard.current.ctrlKey.wasPressedThisFrame || Gamepad.current.buttonEast.wasPressedThisFrame || Gamepad.current.buttonWest.wasPressedThisFrame)
            {
                SetDoorOpen();
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collider)
    {
        PrototypePlayerController player = collider.GetComponent<PrototypePlayerController>();

        if (player != null)
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        PrototypePlayerController player = collider.GetComponent<PrototypePlayerController>();

        if (player != null)
        {
            isPlayerNearby = false;
        }
    }

    public void SetDoorOpen()
    {
        isOpen = true;
        if (doorAnimator != null)
        {
            doorAnimator.SetBool("Open", true);
        }
    }
}