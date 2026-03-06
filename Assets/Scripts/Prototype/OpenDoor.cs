using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Animator doorAnimator;
    public bool isOpen { get; set; } = false;
    void Start()
    {
        // verifica se a porta do nivel 1 foi aberta pelo singleton
        if (GameManager.Instance != null && GameManager.Instance.Level1DoorIsOpen)
        {
            isOpen = true;
            doorAnimator.SetBool("Open", true);
            
        }
    }
    // quando o jogador toca na porta do nivel 1. colocar o estado como aberta guardar no singleton
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            doorAnimator.SetBool("Open", true);
            isOpen = true;
            GameManager.Instance.Level1DoorIsOpen = true;
        }
    }
}