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
            if (Keyboard.current.eKey.wasPressedThisFrame || Keyboard.current.ctrlKey.wasPressedThisFrame)
            {
                if (GameManager.Instance.currentLevel == 1)
                {
                    GameManager.Instance.Level1DoorIsOpen = true;
                }
                else if (GameManager.Instance.currentLevel == 2)
                {
                    GameManager.Instance.Level2DoorIsOpen = true;
                }
                SetDoorOpen();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
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