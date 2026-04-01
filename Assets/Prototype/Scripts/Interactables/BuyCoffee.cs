using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
public class BuyCoffee : MonoBehaviour
{
    [SerializeField] private Slider timeBarSlider;
    [SerializeField] private TextMeshProUGUI timeTextDisplay;
    [SerializeField] private float coffeeCost = 5f;

    private bool isPlayerNearby = false;

    // jogador esta na shop e carrega na tecla E
    private void OnInteract(InputAction.CallbackContext context)

    {
        if (isPlayerNearby && !GameManager.Instance.coffee)
        {
            ExecutePurchase();
        }
    }

    private void ExecutePurchase()
    {

        if (GameManager.Instance != null)
        {
            GameManager.Instance.DeductTime(coffeeCost);
            GameManager.Instance.coffee = true;
            GameManager.Instance.Level1DoorIsOpen = true;
        }
    }


    // Atualiza HUB do tempo
    public void UpdateUI()
    {

        if (timeBarSlider != null)
        {
            timeBarSlider.value = GameManager.Instance.timeRemaining;
        }

        if (timeTextDisplay != null)
        {
            int minutes = Mathf.FloorToInt(GameManager.Instance.timeRemaining / 60);
            int seconds = Mathf.FloorToInt(GameManager.Instance.timeRemaining % 60);
            timeTextDisplay.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        PrototypePlayerController player = collider.GetComponent<PrototypePlayerController>();

        if (player != null)
        {
            isPlayerNearby = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collider)
    {
        PrototypePlayerController player = collider.GetComponent<PrototypePlayerController>();

        if (player != null)
        {
            isPlayerNearby = false;
        }
    }
}