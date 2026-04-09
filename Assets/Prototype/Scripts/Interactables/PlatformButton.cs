using UnityEngine;
using UnityEngine.InputSystem;

public class PlatformButton : MonoBehaviour
{
    [SerializeField] private MovePlatform platformToActivate;
    private bool isPlayerNearSwitch = false;

    private void Update()
    {
        if (isPlayerNearSwitch)
        {
            if (Keyboard.current.eKey.wasPressedThisFrame ||
                (Gamepad.current != null && (Gamepad.current.buttonWest.wasPressedThisFrame || Gamepad.current.buttonEast.wasPressedThisFrame)))
            {
                platformToActivate.CanMove = !platformToActivate.CanMove;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PrototypePlayerController player = collision.GetComponent<PrototypePlayerController>();
        if (player != null)
        {
            isPlayerNearSwitch = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PrototypePlayerController player = collision.GetComponent<PrototypePlayerController>();
        if (player != null)
        {
            isPlayerNearSwitch = false;
        }
    }
}