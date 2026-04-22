using UnityEngine;

public class Enemy : MonoBehaviour
{
    private enum StartDirection
    {
        Left,
        Right
    }

    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;
    [SerializeField] float speed = 2f;
    [SerializeField] StartDirection startDirection;

    private Rigidbody2D rb;
    private Animator anim;

    private Transform currentPoint;
    private bool isPaused = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (startDirection == StartDirection.Right)
            currentPoint = pointB;
        else
            currentPoint = pointA;
    }

    private void Update()
    {
        if (isPaused)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        if (currentPoint == pointB)
        {
            rb.linearVelocity = new Vector2(speed, 0);
            transform.rotation = Quaternion.identity;
        }
        else
        {
            rb.linearVelocity = new Vector2(-speed, 0);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 2.0f)
        {
            if (currentPoint == pointB)
                currentPoint = pointA;
            else
                currentPoint = pointB;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerLoseTime playerLoseTime = collider.GetComponent<PlayerLoseTime>();

        if (playerLoseTime != null)
        {
            isPaused = true;
            // Se o jogador está à DIREITA do inimigo
            if (collider.transform.position.x < transform.position.x)
            {
                // Inimigo olha para a DIREITA
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                // Inimigo olha para a ESQUERDA
                transform.rotation = Quaternion.identity;
            }
            anim.SetTrigger("DrawTime");
            // animação do jogador e passa a posição do inimigo para o jogador virar para ele
            playerLoseTime.PlayDrawTime(transform);

            Invoke(nameof(ResumeMovement), 1f);
        }
    }
    private void ResumeMovement()
    {
        isPaused = false;

       
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }
}