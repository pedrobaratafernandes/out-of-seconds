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
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            rb.linearVelocity = new Vector2(-speed, 0);
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f)
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
            // virar inimigo para o jogador
            if (collider.transform.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
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

        // inverter direção
        if (currentPoint == pointB)
            currentPoint = pointA;
        else
            currentPoint = pointB;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }
}