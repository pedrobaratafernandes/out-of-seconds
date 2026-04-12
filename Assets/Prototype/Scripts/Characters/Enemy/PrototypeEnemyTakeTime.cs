using UnityEngine;

public class PrototypeEnemyTakeTime : MonoBehaviour
{
    private Animator animator;
    private PrototypeEnemy enemyScript;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        enemyScript = GetComponentInParent<PrototypeEnemy>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        PrototypePlayerController player = collider.GetComponent<PrototypePlayerController>();

        if (player != null)
        {

            if (enemyScript != null)
            {
                enemyScript.CanMove = false;
            }

            if (animator != null)
            {
                animator.SetBool("TakeTime", true);
            }

            // reduz o tempo do jogador em 5 segundos
            GameManager.Instance.DeductTime(5f);

            Rigidbody2D playerRb = collider.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.linearVelocity = Vector2.zero;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        PrototypePlayerController player = collider.GetComponent<PrototypePlayerController>();

        if (player != null)
        {

            if (enemyScript != null) enemyScript.CanMove = true;

            if (animator != null) animator.SetBool("TakeTime", false);

            Rigidbody2D playerRb = collider.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }
}