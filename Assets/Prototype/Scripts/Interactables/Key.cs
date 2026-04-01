using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        PrototypePlayerController player = collider.GetComponent<PrototypePlayerController>();

        if (player != null)
        {
            InventoryManager inv = FindFirstObjectByType<InventoryManager>();
            if (inv != null)
            {
                inv.ActiveKeyUI();
                Destroy(gameObject);
            }
        }
    }
}