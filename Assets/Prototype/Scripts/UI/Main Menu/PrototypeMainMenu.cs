using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System;

public class PrototypeMainMenu : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI timeDisplay;      // texto de UI do relogio do sistema
    [SerializeField] private TextMeshProUGUI continueButton;   // texto de UI para opcao continue
    [SerializeField] private TextMeshProUGUI startButton;      // texto de UI para opcao start
    [SerializeField] private TextMeshProUGUI quitButton;       // texto de UI text para opcao quit

    [SerializeField] private Color unselectedColor = Color.white; // cor para deselecao
    [SerializeField] private Color selectedColor = Color.green;   // cor para selecao

    [SerializeField] private InputAction navigateAction; // tecla UP e DOWN new input system
    [SerializeField] private InputAction submitAction;   // tecla ENTER new input system

    private int selectedIndex = 0; // index para item selecionado de opcoes de menu
    private int maxIndex = 1;      // 1 (Start/Quit)  2 (Continue/Start/Quit)
    private bool hasNavigated = false; // nao permitir scroll nas opcoes

    private string continueOriginalText; // string continue
    private string startOriginalText;    // string start
    private string quitOriginalText;     // string quit
    private float menuEnableTime; // tempo de ativacao do menu para controlar input delay
    private float inputDelay = 0.2f; // tempo de espera para evitar input imediato ao abrir o menu
    private void OnEnable()
    {
        // ativar input actions
        navigateAction.Enable();
        submitAction.Enable();
    }

    private void OnDisable()
    {
        // desativar input actions
        navigateAction.Disable();
        submitAction.Disable();
    }

    private void Start()
    {
        menuEnableTime = Time.time;
        // texto de opcoes sem estar sublinhados
        startOriginalText = startButton.text;
        quitOriginalText = quitButton.text;

        // se jogo começou variavel singleton
        if (PrototypeGameManager.Instance != null && PrototypeGameManager.Instance.gameStarted)
        {
            continueOriginalText = continueButton.text; // estado do texto
            continueButton.gameObject.SetActive(true);   // mostrar botao
            maxIndex = 2;       // 0(Continue), 1(Start), 2(Quit)
            selectedIndex = 0;  // continue é o primeiro a ser selecionado
        }
        else
        {
            // se jogo nao esta ativo entao esconder a opcao continue
            if (continueButton != null) continueButton.gameObject.SetActive(false);
            maxIndex = 1;       // 0(Start), 1(Quit)
            selectedIndex = 0;  // start é o primeiro a ser selecionado
        }

        // aplicar cores e formatacao da selecao das opcoes
        UpdateSelectionVisuals();
    }

    private void Update()
    {

        if (Time.time < menuEnableTime + inputDelay) return;
        // tempo real do relogio da maquina horas:minutos:segundos
        if (timeDisplay != null)
            timeDisplay.text = DateTime.Now.ToString("HH:mm:ss");

        // leitura de up(Y = 1) e down(Y = -1)
        Vector2 navInput = navigateAction.ReadValue<Vector2>();

        if (!hasNavigated)
        {
            if (navInput.y > 0.5f) // mover para cima
            {
                selectedIndex = Mathf.Clamp(selectedIndex - 1, 0, maxIndex);
                UpdateSelectionVisuals();
                hasNavigated = true; // bloquear movimento apenas mover se a tecla for solta
            }
            else if (navInput.y < -0.5f) // mover para baixo
            {
                selectedIndex = Mathf.Clamp(selectedIndex + 1, 0, maxIndex);
                UpdateSelectionVisuals();
                hasNavigated = true; // bloquear movimento apenas mover se a tecla for solta
            }
        }

        // permitir nova navegaçao quando a tecla for solta
        if (Mathf.Abs(navInput.y) < 0.1f)
            hasNavigated = false;

        // verifica se opcao é premida
        if (submitAction.triggered)
        {
            if (maxIndex == 2) // continue start e quit
            {
                if (selectedIndex == 0)
                {
                    OnContinueClicked();
                }
                else if (selectedIndex == 1)
                {
                    OnStartClicked();
                }
                else
                {
                    OnQuitClicked();
                }
            }
            else // start quit
            {
                if (selectedIndex == 0)
                {
                    OnStartClicked();
                }
                else
                {
                    OnQuitClicked();
                }
            }
        }
    }

    private void UpdateSelectionVisuals()
    {
        // reset na formatacao dos textos das opcoes do main menu
        ResetTextVisuals(startButton, startOriginalText);
        ResetTextVisuals(quitButton, quitOriginalText);
        ResetTextVisuals(continueButton, continueOriginalText);

        // aplicar formatacao e cor a opcao selecionada
        if (maxIndex == 2) // continue, start, quit
        {
            if (selectedIndex == 0)
            {
                HighlightTextVisuals(continueButton, continueOriginalText);
            }
            else if
                (selectedIndex == 1)
            {
                HighlightTextVisuals(startButton, startOriginalText);
            }
            else if (selectedIndex == 2)
            {
                HighlightTextVisuals(quitButton, quitOriginalText);
            }
        }
        else // start, quit
        {
            if (selectedIndex == 0)
            {
                HighlightTextVisuals(startButton, startOriginalText);
            }
            else if (selectedIndex == 1)
            {
                HighlightTextVisuals(quitButton, quitOriginalText);
            }
        }
    }

    // aplicar cores e formatacao sublinhado nas opcoes do main menu
    private void HighlightTextVisuals(TextMeshProUGUI txt, string original)
    {
        txt.text = $"<u>{original}</u>";
        txt.color = selectedColor;
    }

    // remover formatacao e restaurar cor original nas opcoes do main menu
    private void ResetTextVisuals(TextMeshProUGUI txt, string original)
    {
        txt.text = original;
        txt.color = unselectedColor;
    }

    private void OnStartClicked()
    {
        // variaveis singleton
        PrototypeGameManager.Instance.gameStarted = true; // jogo iniciou
        PrototypeGameManager.Instance.shouldRestorePosition = false; // nao guardar posicao do jogador
        PrototypeGameManager.Instance.lastPlayerPosition = Vector2.zero; // reset a posicao do jogador
        PrototypeGameManager.Instance.timeRemaining = 60f; // tempo inicial 60 segundos
        SceneManager.LoadScene("Prototype 1"); // carregar nivel 1
    }

    private void OnContinueClicked()
    {
        PrototypeGameManager.Instance.returnTime = PrototypeGameManager.Instance.timeRemaining;
        PrototypeGameManager.Instance.isContinuing = true;
        // carrega nome da scene atual
        SceneManager.LoadScene(PrototypeGameManager.Instance.currentSceneName);
    }

    private void OnQuitClicked()
    {
        Application.Quit(); // fecha janela do jogo
    }
}