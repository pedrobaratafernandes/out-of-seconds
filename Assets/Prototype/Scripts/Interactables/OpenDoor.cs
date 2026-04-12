using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] private Animator doorAnimator;
    public bool isOpen = false;
    private bool isPlayerNearby = false;
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private TextMeshProUGUI infoText;
    private void Update()
    {
        // player esta perto da porta e aberta E ele carregou W ou UP
        if (isPlayerNearby && !isOpen)
        {
            // Verifica se a tecla configurada no Input System (ou W/Up) foi premida
            bool keyboardInput = Keyboard.current != null &&
            (Keyboard.current.eKey.wasPressedThisFrame || Keyboard.current.ctrlKey.wasPressedThisFrame);

            // Verifica se o gamepad existe
            bool gamepadInput = Gamepad.current != null &&
                (Gamepad.current.buttonEast.wasPressedThisFrame || Gamepad.current.buttonWest.wasPressedThisFrame);

            if (keyboardInput || gamepadInput)
            {
                SetDoorOpen();
                //mostrar icon key no HUB
                if (inventoryManager != null)
                {
                    inventoryManager.ActiveKeyUI();
                }
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collider)
    {
        PrototypePlayerController player = collider.GetComponent<PrototypePlayerController>();

        if (player != null)
        {
            if (infoText != null)
            {
                infoText.gameObject.SetActive(true);
            }
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        PrototypePlayerController player = collider.GetComponent<PrototypePlayerController>();

        if (player != null)
        {
            if (infoText != null)
            {
                infoText.gameObject.SetActive(false);
            }
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