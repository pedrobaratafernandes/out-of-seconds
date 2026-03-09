using UnityEngine;

public class Capsule : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddCapsule();
            }

            Destroy(gameObject);
        }
    }
}