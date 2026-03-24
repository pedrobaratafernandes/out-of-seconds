using UnityEngine;

// video referencia https://www.youtube.com/watch?v=RuvfOl8HhhM
public class MovePlatform : MonoBehaviour
{
    private enum StartDirection
    {
        PointA,
        PointB
    }

    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;
    private Rigidbody2D rb;


    [SerializeField] float speed = 2f;
    [SerializeField] StartDirection startDirection;
    [SerializeField] bool _canMove = true;


    public bool CanMove
    {
        get { return _canMove; }
        set { _canMove = value; }
    }
    private Transform currentPoint;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (startDirection == StartDirection.PointA)
        {
            currentPoint = pointB;
        }
        else
        {
            currentPoint = pointA;
        }
    }

    private void Update()
    {
        if (!_canMove)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 direction = (currentPoint.position - transform.position).normalized;
        rb.linearVelocity = direction * speed;

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.1f)
        {
            currentPoint = (currentPoint == pointB) ? pointA : pointB;
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f)
        {


            if (currentPoint == pointB)
                currentPoint = pointA;
            else
                currentPoint = pointB;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }



}
