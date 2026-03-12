using UnityEngine;

public class TakeTime : MonoBehaviour
{
    private Animator animator;
    private Friend friendScript;

    void Start()
    {
        animator = GetComponentInParent<Animator>();
        friendScript = GetComponentInParent<Friend>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            if (friendScript != null) friendScript.CanMove = false;

            if (animator != null) animator.SetBool("TakeTime", true);

            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.linearVelocity = Vector2.zero;
                playerRb.bodyType = RigidbodyType2D.Kinematic;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            if (friendScript != null) friendScript.CanMove = true;

            if (animator != null) animator.SetBool("TakeTime", false);

            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }
}