using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EnterDoor : MonoBehaviour
{
    [SerializeField] private string sceneName; // qual e nome da scene que o jogador segue depois de tocar na porta aberta ?

    [SerializeField] private OpenDoor door; // referencia para chave

    private bool playerNearDoor = false; // jogador esta na porta ?


    private void Update()
    {
        // player esta perto da porta e aberta E ele carregou W ou UP
        if (playerNearDoor && door != null && door.isOpen)
        {
            // Verifica se a tecla configurada no Input System (ou W/Up) foi premida
            if (Keyboard.current.wKey.wasPressedThisFrame || Keyboard.current.upArrowKey.wasPressedThisFrame)
            {
                SceneManager.LoadScene(sceneName);
            }
        }
    }

    // se jogador esta em cima da porta
    private void OnTriggerEnter2D(Collider2D collider)
    {
        PrototypePlayerController player = collider.GetComponent<PrototypePlayerController>();

        if (player != null)
        {
            playerNearDoor = true;
        }
    }

    //se jogador nao esta em cima da porta
    private void OnTriggerExit2D(Collider2D collider)
    {
        PrototypePlayerController player = collider.GetComponent<PrototypePlayerController>();

        if (player != null)
        {
            playerNearDoor = false;
        }
    }

}