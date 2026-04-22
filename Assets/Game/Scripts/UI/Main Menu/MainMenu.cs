using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System; // para DateTime.Now
public class MainMenu : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI timeDisplay;      // texto de UI do relogio do sistema
    [SerializeField] private TextMeshProUGUI continueButton;   // texto de UI para opcao continue
    [SerializeField] private TextMeshProUGUI startButton;      // texto de UI para opcao start
    [SerializeField] private TextMeshProUGUI quitButton;       // texto de UI text para opcao quit

    [SerializeField] private Color selectedColor = new Color(0.28f, 0.89f, 0.53f);   // cor para selecao

    [SerializeField] private InputAction navigateAction; // tecla UP e DOWN new input system
    [SerializeField] private InputAction submitAction;   // tecla ENTER new input system

    private int selectedIndex = 0; // index para item selecionado de opcoes de menu
    private int maxIndex = 1;      // 1 (Start/Quit)  2 (Continue/Start/Quit)
                                   // string quit
    private float menuEnableTime; // tempo de ativacao do menu para controlar input delay
    private float inputDelay = 0.2f; // tempo de espera para evitar input imediato ao abrir o menu

    // main menu para ler o input do teclado usando o new input system
    // https://docs.unity3d.com/6000.3/Documentation/ScriptReference/MonoBehaviour.OnEnable.html
    private void OnEnable()
    {
        // ativar input actions
        navigateAction.Enable();
        submitAction.Enable();
    }
    // desativar input actions quando o menu for desativado para evitar leitura de input indesejado
    // https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnDisable.html
    private void OnDisable()
    {
        // desativar input actions
        navigateAction.Disable();
        submitAction.Disable();
    }

    private void Start()
    {
        menuEnableTime = Time.time;

        // se jogo começou mostrar botao continue
        if (GameManager.Instance != null && GameManager.Instance.gameStarted)
        {
            continueButton.gameObject.SetActive(true);   // mostrar botao
            maxIndex = 2;       // 0(Continue), 1(Start), 2(Quit)
            selectedIndex = 0;  // continue é o primeiro a ser selecionado
        }
        else
        {
            // se jogo nao esta ativo entao esconder a opcao continue
            if (continueButton != null)
                continueButton.gameObject.SetActive(false);
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

        // https://docs.unity3d.com/Packages/com.unity.inputsystem@1.19/api/UnityEngine.InputSystem.InputBindingComposite-1.html#methods
        // leitura de up(Y = 1) e down(Y = -1)
        Vector2 navInput = navigateAction.ReadValue<Vector2>();

        // Input Debouncing (ignorar entradas rápidas e repetitivas) referencias:
        // https://docs.unity3d.com/Packages/com.unity.inputsystem@1.8/manual/Workflow-Direct.html
        // https://docs.unity3d.com/6000.3/Documentation/ScriptReference/Mathf.Clamp.html

        //  https://docs.unity3d.com/6000.3/Documentation/ScriptReference/Mathf.Clamp.html
        if (navigateAction.WasPressedThisFrame())
        {
            if (navInput.y > 0.5f) // mover para cima
            {
                // limita um valor entre um minimo e um maximo
                selectedIndex = Mathf.Clamp(selectedIndex - 1, 0, maxIndex);
            }
            else if (navInput.y < -0.5f) // mover para baixo
            {
                // limita um valor entre um minimo e um maximo
                selectedIndex = Mathf.Clamp(selectedIndex + 1, 0, maxIndex);
            }
            // atualizar a formatacao do texto
            UpdateSelectionVisuals();
        }


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
        ResetTextVisuals(startButton);
        ResetTextVisuals(quitButton);
        ResetTextVisuals(continueButton);

        // aplicar formatacao e cor a opcao selecionada
        if (maxIndex == 2) // continue, start, quit
        {
            if (selectedIndex == 0)
            {
                HighlightTextVisuals(continueButton, "Continue");
            }
            else if
                (selectedIndex == 1)
            {
                HighlightTextVisuals(startButton, "Start");
            }
            else if (selectedIndex == 2)
            {
                HighlightTextVisuals(quitButton, "Quit");
            }
        }
        else // start, quit
        {
            if (selectedIndex == 0)
            {
                HighlightTextVisuals(startButton, "Start");
            }
            else if (selectedIndex == 1)
            {
                HighlightTextVisuals(quitButton, "Quit");
            }
        }
    }

    // aplicar cores e formatacao sublinhado nas opcoes do main menu
    private void HighlightTextVisuals(TextMeshProUGUI txt, string original)
    {
        // Aplicar sublinhado e cor verde diretamente no texto
        // https://docs.unity3d.com/Packages/com.unity.textmeshpro@4.0/manual/RichTextStrikethroughUnderline.html
        // https://docs.unity3d.com/Packages/com.unity.textmeshpro@4.0/manual/RichTextColor.html
        // https://docs.unity3d.com/6000.3/Documentation/ScriptReference/ColorUtility.ToHtmlStringRGBA.html
        string hexColor = ColorUtility.ToHtmlStringRGBA(selectedColor);
        txt.text = $"<u><color=#{hexColor}>{original}</color></u>";
    }

    // remover formatacao e restaurar cor original nas opcoes do main menu
    private void ResetTextVisuals(TextMeshProUGUI txt)
    {
        txt.color = Color.white;
        // https://learn.microsoft.com/en-us/dotnet/api/system.string.replace?view=net-10.0
        txt.text = txt.text.Replace("<u>", "").Replace("</u>", "").Replace($"<color=#{selectedColor}>", "").Replace("</color>", "");
    }

    private void OnStartClicked()
    {
        // variaveis singleton
        GameManager.Instance.gameStarted = true; // jogo iniciou
        GameManager.Instance.shouldRestorePosition = false; // nao guardar posicao do jogador
        GameManager.Instance.lastPlayerPosition = Vector2.zero; // reset a posicao do jogador
        SceneManager.LoadScene("Test Player"); // carregar nivel 1
    }

    private void OnContinueClicked()
    {
        // guardar tempo restante do nivel para continuar depois
        GameManager.Instance.returnTime = GameManager.Instance.timeRemaining;
        GameManager.Instance.isContinuing = true;
        // carrega nome da scene atual
        SceneManager.LoadScene(GameManager.Instance.currentSceneName);
    }

    private void OnQuitClicked()
    {
        Application.Quit(); // fecha janela do jogo
    }
}