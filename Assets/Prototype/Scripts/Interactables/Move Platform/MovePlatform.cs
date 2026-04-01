using UnityEngine;

// video referencia https://www.youtube.com/watch?v=oftgVDuxn8k
public class MovePlatform : MonoBehaviour
{
    private enum StartDirection
    {
        PointA,
        PointB
    }

    [Header("Points A and B")]
    [SerializeField] private Transform pointA; // O ponto A no mapa
    [SerializeField] private Transform pointB; // O ponto B no mapa

    [Header("Type of Movement")]
    [SerializeField] float speed = 2; // Velocidade da plataforma
    [SerializeField] StartDirection startDirection; // começa o movimento no ponto A ou ponto B
    [SerializeField] bool _canMove = true; // Se ela pode andar ou não
    private Vector3 nextPosition;

    // Esta propriedade permite que outros scripts pausem ou despausem a plataforma
    public bool CanMove
    {
        get { return _canMove; }
        set { _canMove = value; }
    }
    private Rigidbody2D rb;
    private Transform currentPoint;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        nextPosition = pointB.position;
        // Descobre para onde ir primeiro
        if (startDirection == StartDirection.PointA)
        {
            // Se começa no A ir para o B
            transform.position = pointA.position;
            nextPosition = pointB.position;
        }
        else
        {
            // Se começa no B ir para o A
            transform.position = pointB.position;
            nextPosition = pointA.position;
        }
    }

    private void Update()
    {
        // Se a plataforma NÃO não se move
        if (!_canMove)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);
        if (transform.position == nextPosition)
        {
            nextPosition = (nextPosition == pointA.position) ? pointB.position : pointA.position;
        }

    }

    // Desenha linhas visuais para game designer, caminho do movimento
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            // colocar jogador fixado na plataforma
            collision.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            collision.transform.parent = null;
        }
    }
}
