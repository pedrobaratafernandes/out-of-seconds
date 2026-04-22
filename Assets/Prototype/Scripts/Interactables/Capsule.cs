using UnityEngine;

public class Capsule : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        PrototypePlayerController player = collider.GetComponent<PrototypePlayerController>();

        if (player != null)
        {
            if (PrototypeGameManager.Instance != null)
            {
                PrototypeGameManager.Instance.AddCapsule();
            }

            Destroy(gameObject);
        }
    }
}