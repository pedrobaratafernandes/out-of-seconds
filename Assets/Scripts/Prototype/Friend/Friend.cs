using UnityEngine;

// video referencia https://www.youtube.com/watch?v=RuvfOl8HhhM
public class Friend : MonoBehaviour
{
    public enum StartDirection
    {
        Left,
        Right
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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (startDirection == StartDirection.Right)
        {
            currentPoint = pointB;
        }
        else
        {
            currentPoint = pointA;
        }
    }

    void Update()
    {
        if (!_canMove)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        if (currentPoint == pointB)
        {
            rb.linearVelocity = new Vector2(speed, 0);
        }
        else
        {
            rb.linearVelocity = new Vector2(-speed, 0);
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f)
        {
            Flip();

            if (currentPoint == pointB)
                currentPoint = pointA;
            else
                currentPoint = pointB;
        }
    }
    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }



}
