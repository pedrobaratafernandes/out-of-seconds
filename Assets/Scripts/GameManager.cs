using UnityEngine;

public class GameManager : MonoBehaviour
{
    //documentacao para aprender sobre singleton no unity https://www.game-developers.org/singletons-in-unity-101-a-comprehensive-tutorial
    // acesso ao GameManager.Instance
    public static GameManager Instance { get; private set; }

    private int totalCapsuleItems = 10;
    private int currentCapsulesCollected = 0;
    public float timeRemaining = 60f;           // tempo restante
    public float levelMaxTime = 60f;            // tempo maximo
    public int currentLevel = 1;                 // nivel atual
    public bool gameStarted = false;             // jogo iniciado ?
    public string currentSceneName = "Prototype 1"; // nome do nivel atual
    public Vector3 lastPlayerPosition; // Stores the player's position
    public bool shouldRestorePosition = false; // flag para verificar se o posicao do jogador é guardado
    public bool coffee = false;
    private BuyCoffee coffeeScript;
    private InventoryManager inventoryManagerScript;
    public bool Level1DoorIsOpen = false;
    public bool Level2DoorIsOpen = false;

    void Start()
    {
        // Procura na scene pelos scripts
        coffeeScript = FindFirstObjectByType<BuyCoffee>();
        inventoryManagerScript = FindFirstObjectByType<InventoryManager>();
    }
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

    public int GetCapsuleCount()
    {
        return currentCapsulesCollected;
    }


    // reduz tempo e atualiza HUB de tempo
    public void DeductTime(float amount)
    {
        timeRemaining -= amount;
        if (timeRemaining < 0)
        {
            timeRemaining = 0;
        }

        if (coffeeScript == null)
        {
            coffeeScript = FindFirstObjectByType<BuyCoffee>();
        }

        if (coffeeScript != null)
        {
            coffeeScript.UpdateUI(); // atualiza a HUB de tempo
        }
    }
}