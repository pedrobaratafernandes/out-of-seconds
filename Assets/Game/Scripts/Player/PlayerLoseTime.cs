using UnityEngine;

public class PlayerLoseTime : MonoBehaviour
{
    private PlayerController playerController;
    private Animator animator;
    private Rigidbody2D rb;

    private bool isPaused = false;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }

    public void PlayDrawTime(Transform enemy)
    {
        if (isPaused) return;

        isPaused = true;

        // vira a sprite para olhar para o inimigo
        if (enemy.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // bloqueia movimento
        rb.linearVelocity = Vector2.zero;
        playerController.enabled = false;
        animator.SetTrigger("LoseTime");
        // duração da animação
        Invoke(nameof(EndDrawTime), 1f);
    }

    private void EndDrawTime()
    {
        // desbloqueia movimento
        playerController.enabled = true;
        isPaused = false;
    }
}