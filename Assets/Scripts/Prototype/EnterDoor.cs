using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EnterDoor : MonoBehaviour
{
    public string sceneName; // qual e nome da scene que o jogador segue depois de tocar na porta aberta ?

    public OpenDoor door; // referencia para chave

    private bool playerNearDoor = false; // jogador esta perto da porta ?

    // new input system tecla E
    public void OnInteract(InputAction.CallbackContext context)
    {
        //se jogador esta em cima da porta e porta aberta seguir para a scene com nome no inspector
        if (playerNearDoor && door.isOpen)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
    // se jogador esta em cima da porta
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerNearDoor = true;
        }
    }

    //se jogador nao esta em cima da porta
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerNearDoor = false;
        }
    }

}