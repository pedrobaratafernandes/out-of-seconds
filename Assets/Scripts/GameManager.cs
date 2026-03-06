using UnityEngine;

public class GameManager : MonoBehaviour
{
    //documentacao para aprender sobre singleton no unity https://www.game-developers.org/singletons-in-unity-101-a-comprehensive-tutorial
    // acesso ao GameManager.Instance
    public static GameManager Instance { get; private set; }

    public float timeRemaining = 60f;           // tempo restante
    public float levelMaxTime = 60f;            // tempo maximo
    public int currentLevel = 1;                 // nivel atual
    public bool gameStarted = false;             // jogo iniciado ?
    public string currentSceneName = "Prototype 1"; // nome do nivel atual
    public Vector3 lastPlayerPosition; // Stores the player's position
    public bool shouldRestorePosition = false; // flag para verificar se o posicao do jogador é guardado
    public bool Level1DoorIsOpen = false;

    private void Awake()
    {
        // Singleton Pattern: apenas um GameManager pode existir no jogo
        if (Instance != null && Instance != this)
        {
            // destruir object se ja existir apenas pode existir um
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // nao permitir que o GameManager seja eleminado da scene
        DontDestroyOnLoad(gameObject);
    }

    // progresso do jogador e estado de posicao
    public void SetLevel(int level, string sceneName)
    {
        currentLevel = level;        // em que nivel esta ?
        currentSceneName = sceneName; // qual e o nome da scene ?
        shouldRestorePosition = false; // reset da posicao do jogador quando o nivel começa
    }
}