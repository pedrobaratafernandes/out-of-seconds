using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Animator doorAnimator;
    public bool isOpen { get; private set; } = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            doorAnimator.SetBool("Open", true);
            isOpen = true;
        }
    }
}