using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeBar : MonoBehaviour
{
    public Image fillImage;         // imagem de referencia para colocar barra de tempo verde (sprite square)
    public TextMeshProUGUI timerText; // referencia de texto TMP para tempo

    private float maxTime; // tempo inicial para calcular percentagem

    void Update()
    {
        //se tempo restante for maior que 0 entao       
        if (GameManager.Instance != null && GameManager.Instance.timeRemaining > 0)
        {
            //diminuir tempo
            GameManager.Instance.timeRemaining -= Time.deltaTime;

            // calcula a proporção de tempo restante e gera a barra de 0 a 1
            fillImage.fillAmount = GameManager.Instance.timeRemaining / GameManager.Instance.levelMaxTime;

            //metodo para atualizar tempo
            UpdateTimerText(GameManager.Instance.timeRemaining);
        }
        // se tempo restante chegar ao zero
        else if (GameManager.Instance != null && GameManager.Instance.timeRemaining <= 0)
        {
            // variavel singleton tempo restante para 0
            GameManager.Instance.timeRemaining = 0;
            //barra chega ao 0
            fillImage.fillAmount = 0;
            // colocar o texto do relogio a zeros
            timerText.text = "0000:00:0:00:00:00";
            // variavel singleton reinicia jogo para nivel 1
            GameManager.Instance.gameStarted = false;
        }
    }
    // fazer texto do tempo
    void UpdateTimerText(float timeInSeconds)
    {
        // documentacao para aprender https://gamedevbeginner.com/how-to-make-countdown-timer-in-unity-minutes-seconds/
        // calcular minutos e segundos
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);

        // formatar string do tempo como no filme 0000:00:0:00:minutos:segundos
        // {0:00} para minutos e {1:00} para segundos coloca tempo com 0 antes do valor por exemplo 03 minutos e 05 segundos
        timerText.text = string.Format("0000:00:0:00:{0:00}:{1:00}", minutes, seconds);
    }
}