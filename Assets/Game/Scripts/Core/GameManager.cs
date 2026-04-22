using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //documentacao para aprender sobre singleton no unity https://www.game-developers.org/singletons-in-unity-101-a-comprehensive-tutorial
    // acesso ao PrototypeGameManager.Instance
    public static GameManager Instance { get; private set; }

    private int totalCapsuleItems = 10;
    public int currentCapsulesCollected = 0;
    public int currentLevel = 1;

    [Header("Level Settings")]
    public float timeRemaining;         // tempo restante
    public float levelMaxTime;            // tempo maximo

    [Header("Global Progression")]
    public float globalTimeRemaining = 300f; // tempo de jogo 5 minutos
    public bool gameStarted = false;           // jogo iniciado ?
    public string currentSceneName; // nome do nivel atual
    public Vector2 lastPlayerPosition; // guarda a posição do jogador
    public bool shouldRestorePosition = false; // flag para verificar se o posicao do jogador é guardado
    public bool coffee = false;
    private BuyCoffee coffeeScript;
    private InventoryManager inventoryManagerScript;
    public bool isContinuing = false;
    public float returnTime;
    void Start()
    {
        // Procura na scene pelos scripts
        coffeeScript = FindFirstObjectByType<BuyCoffee>();
        inventoryManagerScript = FindFirstObjectByType<InventoryManager>();
    }

    private void Update()
    {
        if (gameStarted)
        {
            if (globalTimeRemaining > 0)
            {
                globalTimeRemaining -= Time.deltaTime;
            }
            else
            {
                CheckGameEnd();
            }

        }
    }
    private void Awake()
    {
        // Singleton Pattern: apenas um PrototypeGameManager pode existir no jogo
        if (Instance != null && Instance != this)
        {
            // destruir object se ja existir apenas pode existir um
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // nao permitir que o PrototypeGameManager seja eleminado da scene
        DontDestroyOnLoad(gameObject);
    }
    public void SetupLevel(int level, string sceneName, float secondsForThisLevel)
    {
        currentLevel = level;        // em que nivel esta ?
        currentSceneName = sceneName; // qual e o nome da scene ?
        shouldRestorePosition = false; // reset da posicao do jogador quando o nivel começa

        timeRemaining = secondsForThisLevel;
        gameStarted = true;
    }

    // decidir qual final aparece
    public void CheckGameEnd()
    {
        gameStarted = false;

        // barra ou popup timer chega ao 0 jogador morre
        if (globalTimeRemaining <= 0 || timeRemaining <= 0)
        {
            // jogador morre
            SceneManager.LoadScene("End A");
        }
        else if (globalTimeRemaining >= 30f && currentCapsulesCollected == totalCapsuleItems)
        {
            // salvou a mãe
            SceneManager.LoadScene("End C");

        }
        else
        {
            // mãe morre
            SceneManager.LoadScene("End B");
        }
    }



    // definir nivel e estado de posicao do jogador

    public void AddCapsule()
    {
        if (currentCapsulesCollected < totalCapsuleItems)
        {
            currentCapsulesCollected++;

            // Avisa o inventário para atualizar o visual
            if (inventoryManagerScript == null)
                inventoryManagerScript = FindFirstObjectByType<InventoryManager>();

            if (inventoryManagerScript != null)
                inventoryManagerScript.UpdateCapsuleUI();
        }
    }
    public void RemoveCapsule()
    {
        if (currentCapsulesCollected > 0)
        {
            currentCapsulesCollected--;

            // Update inventory UI
            if (inventoryManagerScript == null)
                inventoryManagerScript = FindFirstObjectByType<InventoryManager>();

            if (inventoryManagerScript != null)
                inventoryManagerScript.UpdateCapsuleUI();
        }
    }
    public int GetCapsuleCount()
    {
        return currentCapsulesCollected;
    }


    // reduz tempo e atualiza HUB de tempo
    public void DeductTime(float amount)
    {
        //Remove do tempo do nível (Barra Verde)
        timeRemaining -= amount;
        if (timeRemaining < 0)
        {
            timeRemaining = 0;
        }

        // Remove do tempo global
        globalTimeRemaining -= amount;
        if (globalTimeRemaining < 0)
        {
            globalTimeRemaining = 0;
        }
        if (coffeeScript == null)
        {
            coffeeScript = FindFirstObjectByType<BuyCoffee>();
        }
        if (coffeeScript != null)

            // atualiza a HUB de tempo
            coffeeScript.UpdateUI();

    }
}