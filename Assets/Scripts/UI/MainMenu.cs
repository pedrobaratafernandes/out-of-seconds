using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System;

public class MainMenu : MonoBehaviour
{

    public TextMeshProUGUI timeDisplay;      // texto de UI do relogio do sistema
    public TextMeshProUGUI continueButton;   // texto de UI para opcao continue
    public TextMeshProUGUI startButton;      // texto de UI para opcao start
    public TextMeshProUGUI quitButton;       // texto de UI text para opcao quit

    public Color unselectedColor = Color.white; // cor para deselecao
    public Color selectedColor = Color.green;   // cor para selecao

    public InputAction navigateAction; // tecla UP e DOWN new input system
    public InputAction submitAction;   // tecla ENTER new input system

    private int selectedIndex = 0; // index para item selecionado de opcoes de menu
    private int maxIndex = 1;      // 1 (Start/Quit)  2 (Continue/Start/Quit)
    private bool hasNavigated = false; // nao permitir scroll nas opcoes

    private string continueOriginalText; // string continue
    private string startOriginalText;    // string start
    private string quitOriginalText;     // string quit

    void OnEnable()
    {
        // ativar input actions
        navigateAction.Enable();
        submitAction.Enable();
    }

    void OnDisable()
    {
        // desativar input actions
        navigateAction.Disable();
        submitAction.Disable();
    }

    void Start()
    {
        // texto de opcoes sem estar sublinhados
        startOriginalText = startButton.text;
        quitOriginalText = quitButton.text;

        // se jogo começou variavel singleton
        if (GameManager.Instance != null && GameManager.Instance.gameStarted)
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

    void Update()
    {
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

    void UpdateSelectionVisuals()
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
    void HighlightTextVisuals(TextMeshProUGUI txt, string original)
    {
        txt.text = $"<u>{original}</u>";
        txt.color = selectedColor;
    }

    // remover formatacao e restaurar cor original nas opcoes do main menu
    void ResetTextVisuals(TextMeshProUGUI txt, string original)
    {   
        txt.text = original;
        txt.color = unselectedColor;
    }

    public void OnStartClicked()
    {
        // variaveis singleton
        GameManager.Instance.gameStarted = true; // jogo iniciou
        GameManager.Instance.shouldRestorePosition = false; // nao guardar posicao do jogador
        GameManager.Instance.lastPlayerPosition = Vector3.zero; // reset a posicao do jogador
        GameManager.Instance.timeRemaining = 60f; // tempo inicial 60 segundos
        SceneManager.LoadScene("Prototype 1"); // carregar nivel 1
    }

    public void OnContinueClicked()
    {
        // carrega nome da scene atual
        SceneManager.LoadScene(GameManager.Instance.currentSceneName);
    }

    public void OnQuitClicked()
    {
        Application.Quit(); // fecha janela do jogo
    }
}