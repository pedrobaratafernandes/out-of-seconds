using UnityEngine;

public class Key : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
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