using UnityEngine;

public class TakeTime : MonoBehaviour
{
    private Animator animator;
    private Friend friendScript;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        friendScript = GetComponentInParent<Friend>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerController player = collider.GetComponent<PlayerController>();

        if (player != null)
        {

            if (friendScript != null) friendScript.CanMove = false;

            if (animator != null) animator.SetBool("TakeTime", true);

            Rigidbody2D playerRb = collider.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.linearVelocity = Vector2.zero;
                playerRb.bodyType = RigidbodyType2D.Kinematic;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        PlayerController player = collider.GetComponent<PlayerController>();

        if (player != null)
        {

            if (friendScript != null) friendScript.CanMove = true;

            if (animator != null) animator.SetBool("TakeTime", false);

            Rigidbody2D playerRb = collider.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }
}