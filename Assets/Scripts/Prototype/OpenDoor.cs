using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Animator doorAnimator;
    public int levelKey = 1; // nível da porta
    public bool isOpen { get; private set; } = false;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && levelKey == 2)
        {
            SetDoorOpen();
            if (GameManager.Instance != null)
            {
                GameManager.Instance.Level2DoorIsOpen = true;

            }
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player") && GameManager.Instance.coffee && levelKey == 1)
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