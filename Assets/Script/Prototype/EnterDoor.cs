using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EnterDoor : MonoBehaviour
{
    public string sceneName;

    public OpenDoor door;

    private bool playerNearDoor = false;

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (playerNearDoor && door.isOpen)
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerNearDoor = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerNearDoor = false;
        }
    }

}