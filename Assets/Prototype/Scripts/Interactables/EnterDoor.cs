using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EnterDoor : MonoBehaviour
{
    [SerializeField] private string sceneName; // qual e nome da scene que o jogador segue depois de tocar na porta aberta ?

    [SerializeField] private OpenDoor door; // referencia para chave

    private bool playerNearDoor = false; // jogador esta na porta ?

    [SerializeField] private bool isFinalDoor = false;
    private void Update()
    {
        // player esta perto da porta e aberta E ele carregou W ou UP
        if (playerNearDoor && door != null && door.isOpen)
        {
            
            // Verifica se a tecla configurada no Input System W/UP foi premida
            if (Keyboard.current.wKey.wasPressedThisFrame || Keyboard.current.upArrowKey.wasPressedThisFrame ||
            Gamepad.current.dpad.up.wasPressedThisFrame || Gamepad.current.leftStick.up.wasPressedThisFrame)
                if (isFinalDoor)
                {
                    // Se for a última porta, o GameManager decide o final
                    GameManager.Instance.CheckGameEnd();
                }
                else
                {
                    // Se for uma porta comum entre níveis
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