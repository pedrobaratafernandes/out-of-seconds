using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
public class BuyCoffee : MonoBehaviour
{
    [SerializeField] private Slider timeBarSlider;
    [SerializeField] private TextMeshProUGUI timeTextDisplay;
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private float coffeeCost = 5f;
    [SerializeField] private OpenDoor doorToOpen;
    private bool isPlayerNearby = false;

    private void Start()
    {
        infoText.gameObject.SetActive(false);
    }

    // jogador esta na shop e carrega na tecla E ou CTRL
    private void Update()
    {

        if (isPlayerNearby && Keyboard.current.eKey.wasPressedThisFrame || Keyboard.current.ctrlKey.wasPressedThisFrame)
        {
            // Só compra se ainda não tiver café e se houver tempo suficiente
            if (!GameManager.Instance.coffee && GameManager.Instance.timeRemaining >= coffeeCost)
            {
                GameManager.Instance.DeductTime(coffeeCost);
                GameManager.Instance.coffee = true;
               
                doorToOpen.SetDoorOpen();

                UpdateUI();
            }
        }
    }

    // Atualiza HUB do tempo e esconde a mensagem de compra do café
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
        infoText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        PrototypePlayerController player = collider.GetComponent<PrototypePlayerController>();
        infoText.gameObject.SetActive(true);
        if (player != null)
        {
            isPlayerNearby = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collider)
    {
        PrototypePlayerController player = collider.GetComponent<PrototypePlayerController>();
        infoText.gameObject.SetActive(false);
        if (player != null)
        {
            isPlayerNearby = false;
        }
    }
}