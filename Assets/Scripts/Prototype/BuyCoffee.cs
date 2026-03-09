using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
public class BuyCoffee : MonoBehaviour
{
    public Slider timeBarSlider;
    public TextMeshProUGUI timeTextDisplay;
    [SerializeField] private float coffeeCost = 5f;

    private bool isPlayerNearby = false;
    
    // jogador esta na shop e carrega na tecla E
    public void OnInteract(InputAction.CallbackContext context)

    {
        if (isPlayerNearby && !GameManager.Instance.coffee)
        {
            ExecutePurchase();
        }
    }

    void ExecutePurchase()
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }


    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }
}