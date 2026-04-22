using UnityEngine;
using UnityEngine.InputSystem; // new input system
using UnityEngine.SceneManagement; // transicao entre scenes

// video referencia https://www.youtube.com/watch?v=BXmUemP3a6o
// codigo referencia https://gist.github.com/GivaldoF/3cde9c920a9a9c837734ec21a2b2eb31
public class PlayerController : MonoBehaviour
{

    [Header("Movement Settings")]
    [SerializeField] float speed = 160f;       // velocidade horizontal
    [SerializeField] float jumpForce = 180f;   // força vertical para quando o jogador salta
    [SerializeField] private float runSpeed = 2f; // velocidade ao correr

    private Rigidbody2D rb;
    private Animator animator;
    private float horizontal;    // guardar vector2 do move input
    private bool isRunning;

    [Header("Grouding")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheck;

    [Header("Input Actions")]
    [SerializeField] InputAction menuAction; // tecla ESC

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    // ler o input do teclado usando o new input system
    // https://docs.unity3d.com/6000.3/Documentation/ScriptReference/MonoBehaviour.OnEnable.html
    private void OnEnable()
    {
        // mostrar scene main menu com a tecla ESC
        menuAction.Enable();
    }
    // desativar input actions para evitar leitura de input indesejado
    // https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnDisable.html
    private void OnDisable()
    {
        //esconder scene main menu com  a tecla ESC
        menuAction.Disable();
    }

    // new input Move
    public void OnMove(InputAction.CallbackContext ctx)
    {
        // ARROW keys ou gamepad
        horizontal = ctx.ReadValue<Vector2>().x;
    }

    // new input Jump
    public void OnJump(InputAction.CallbackContext ctx)
    {
        // verifica se botao foi premido
        if (ctx.performed && IsGrounded())
        {
            // aplicar movimento de salto
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }
    // new input system Run
    public void OnRun(InputAction.CallbackContext ctx)
    {
        isRunning = ctx.performed; // Verifica se o botão de correr (Shift) está pressionado
    }
    // verifica se o jogador esta no chao
    private bool IsGrounded()
    {
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(20f, 1f), CapsuleDirection2D.Horizontal, 0, groundLayer);
    }
    private void Start()
    {
        // coloca a o jogador na posicao correcta se o jogador carregou no main menu no continue
        if (PrototypeGameManager.Instance != null && PrototypeGameManager.Instance.shouldRestorePosition)
        {
            transform.position = PrototypeGameManager.Instance.lastPlayerPosition;
            // Reinicia a flag para que não continue a teletransportar jogador cada vez que o script é executado.
            PrototypeGameManager.Instance.shouldRestorePosition = false;
        }
    }
    private void Update()
    {
        // Atualiza os parâmetros de animação
        // https://docs.unity3d.com/6000.3/Documentation/ScriptReference/Mathf.Abs.html
        animator.SetBool("isWalking", Mathf.Abs(horizontal) > 0);
        animator.SetBool("isRunning", isRunning);

        // Flip do jogador como aprendi nas aulas
        if (horizontal < 0)
        {
            // virar jogador para esquerda
            transform.rotation = Quaternion.Euler(0, 180f, 0);
        }
        else
        {
            // virar jogador para direita
            transform.rotation = Quaternion.Euler(0, 0f, 0);
        }

        // Verifica se tecla ESC (menuAction) foi premida
        if (menuAction.triggered)
        {
            ReturnToMenu();
        }
    }

    private void FixedUpdate()
    {
        // Mover na horizontal com velocidade ajustada para corrida
        // https://gist.github.com/GivaldoF/3cde9c920a9a9c837734ec21a2b2eb31
        float targetSpeed;
        if (isRunning)
        {
            targetSpeed = speed * runSpeed;
        }
        else
        {
            targetSpeed = speed;
        }

        rb.linearVelocity = new Vector2(horizontal * targetSpeed, rb.linearVelocity.y);
    }

    // quando o jogador retorna a scene main menu
    private void ReturnToMenu()
    {

        if (PrototypeGameManager.Instance != null)
        {
            PrototypeGameManager.Instance.gameStarted = true;
            // atualiza o nome da scene para que o jogador retorne ao nivel indicado
            PrototypeGameManager.Instance.currentSceneName = SceneManager.GetActiveScene().name;

            // atualiza a posicao do jogador
            PrototypeGameManager.Instance.lastPlayerPosition = transform.position;
            PrototypeGameManager.Instance.shouldRestorePosition = true;
        }

        // carregar scena main menu
        SceneManager.LoadScene("Main Menu");
    }



}