using UnityEngine;
public class KnockBack : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMove player = collision.GetComponent<PlayerMove>();

            if (player != null)
            {
                player.Knockback(transform.position);
            }

            GameManager.Instance.RemoveCapsule();
        }
    }
}