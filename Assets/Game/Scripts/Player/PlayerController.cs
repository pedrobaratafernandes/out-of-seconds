using UnityEngine;
using UnityEngine.InputSystem; // new input system
using UnityEngine.SceneManagement; // transicao entre scenes

//  https://www.youtube.com/watch?v=BXmUemP3a6o

public class PlayerController : MonoBehaviour
{

    [Header("Movement Settings")]
    [SerializeField] float speed = 4f;       // velocidade horizontal
    [SerializeField] float jumpForce = 8f;   // força vertical para quando o jogador salta

    private Rigidbody2D rb;        // referencia para componente rigidbody 2d
    private Animator animator;
    private float horizontal;    // guardar vector2 do moveinput

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

    private void OnEnable()
    {
        // mostrar scene main menu com a tecla ESC
        menuAction.Enable();
    }

    private void OnDisable()
    {
        //esconder scene main menu com  a tecla ESC
        menuAction.Disable();
    }

    // componente PlayerInput mover
    public void OnMove(InputAction.CallbackContext ctx)
    {
        // WASD  ou gamepad)
        horizontal = ctx.ReadValue<Vector2>().x;
    }

    // componente PlayerInput saltar
    public void OnJump(InputAction.CallbackContext ctx)
    {
        // verifica se botao foi premido
        if (ctx.performed && IsGrounded())
        {
            // aplicar movimento de salto
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

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
        // flip animação do sprite do jogador
        animator.SetFloat("Speed", Mathf.Abs(horizontal));
        // Se houver movimento, o jogador define a própria rotação
        if (horizontal != 0)
        {
            float angle = (horizontal < 0) ? 180f : 0f;
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
        // verifica se tecla ESC (menuAction) foi premida
        if (menuAction.triggered)
        {
            ReturnToMenu();
        }
    }

    private void FixedUpdate()
    {

        // mover na horizontal
        // FixedUpdate para physics-based movement
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
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