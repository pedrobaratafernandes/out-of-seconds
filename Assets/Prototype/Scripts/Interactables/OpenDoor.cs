using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] private Animator doorAnimator;
    private int levelKey = 1; // nível da porta
    public bool isOpen { get; private set; } = false;


    private void OnTriggerEnter2D(Collider2D collider)
    {
        PrototypePlayerController player = collider.GetComponent<PrototypePlayerController>();

        if (player != null && levelKey == 2)
        {
            SetDoorOpen();
            if (GameManager.Instance != null)
            {
                GameManager.Instance.Level2DoorIsOpen = true;

            }
        }
    }
    private void OnTriggerStay2D(Collider2D collider)
    {
        PrototypePlayerController player = collider.GetComponent<PrototypePlayerController>();

        if (player != null && GameManager.Instance.coffee && levelKey == 1)
        {
            SetDoorOpen();
            if (GameManager.Instance != null)
            {
                GameManager.Instance.Level1DoorIsOpen = true;

            }
        }
    }
    private void SetDoorOpen()
    {
        isOpen = true;
        if (doorAnimator != null)
            doorAnimator.SetBool("Open", true);
    }
}