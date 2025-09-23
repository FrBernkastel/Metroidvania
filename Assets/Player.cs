using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }

    public PlayerInputSet input { get; private set; }
    public StateMachine stateMachine { get; private set; }

    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }
    public Player_DashState dashState { get; private set; }
    public Player_JumpState jumpState { get; private set; }
    public Player_FallState fallState { get; private set; }
    public Player_JumpAttackState jumpAttackState { get; private set; }
    public Player_WallSlideState wallSlideState { get; private set; }
    public Player_WallJumpState wallJumpState { get; private set; }
    public Player_BasicAttackState basicAttackState { get; private set; }

    // Input Flag
    public Vector2 moveInput;

    [Header("Movement details")]
    public float moveSpeed;
    public float dashSpeed;
    public float dashDuration;
    public float jumpForce = 5f;
    public Vector2 wallJumpForce;
    [Range(0f, 1f)] public float inAirMultiplier = 0.85f;
    [Range(0f, 1f)] public float wallSlideSlowMultiplier = 0.3f;
    private bool facingRight = true;
    public int facingDir => facingRight ? 1 : -1;

    [Header("Attack details")]
    public Vector2[] attackVelocity;
    public Vector2 jumpAttackVelocity;
    public float attackVelocityDuration = .1f;
    public float comboResetTime = 0.5f;
    private Coroutine queueAttackCo;

    [Header("Collision detection")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform primaryWallCheck;
    [SerializeField] private Transform secondaryWallCheck;
    public bool groundDetected { get; private set; }
    public bool wallDetected { get; private set; }

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        stateMachine = new StateMachine();
        input = new PlayerInputSet(); // parameterless create

        // States registry
        // Basic State
        idleState = new Player_IdleState(this, stateMachine, "idle");
        moveState = new Player_MoveState(this, stateMachine, "move");
        dashState = new Player_DashState(this, stateMachine, "dash");
        jumpState = new Player_JumpState(this, stateMachine, "jumpFall");
        fallState = new Player_FallState(this, stateMachine, "jumpFall");
        jumpAttackState = new Player_JumpAttackState(this, stateMachine, "jumpAttack");
        wallSlideState = new Player_WallSlideState(this, stateMachine, "wallSlide");
        wallJumpState = new Player_WallJumpState(this, stateMachine, "jumpFall");

        // Attack State
        basicAttackState = new Player_BasicAttackState(this, stateMachine, "basicAttack");
    }

    private void OnEnable()
    {
        input.Enable();
        //input.Player.Movement.started - just press the button
        //input.Player.Movement.performed - press or hold
        //input.Player.Movement.canceled - input stops, when you release the key

        input.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        input.Player.Movement.canceled += ctx => moveInput = Vector2.zero;
    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void Start()
    {
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        HandleColisionDetection();
        stateMachine.UpdateStateActivity();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance));
        int facingDir = facingRight ? 1 : -1;
        Gizmos.DrawLine(primaryWallCheck.position, primaryWallCheck.position + new Vector3(wallCheckDistance * facingDir, 0));
        Gizmos.DrawLine(secondaryWallCheck.position, secondaryWallCheck.position + new Vector3(wallCheckDistance * facingDir, 0));
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.linearVelocity = new Vector2(xVelocity, yVelocity);
        HandleFlip(xVelocity);
    }

    public void CallAnimationTrigger()
    {
        stateMachine.currentState.CallAnimationTrigger();
    }

    public void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
    }

    private void HandleFlip(float xVelocity)
    {
        if (facingRight && xVelocity < 0)
        {
            Flip();
        }

        if (!facingRight && xVelocity > 0)
        {
            Flip();
        }
    }


    /// <summary>
    /// Attack Queue Delay Enter
    /// </summary>
    public void EnterAttackWithDelay()
    {
        if (queueAttackCo != null)
        {
            StopCoroutine(queueAttackCo);
        }
        queueAttackCo = StartCoroutine(EnterAttackStateWithDelayCo());
    }

    private IEnumerator EnterAttackStateWithDelayCo()
    {
        yield return new WaitForEndOfFrame();
        stateMachine.ChangeState(basicAttackState);
    }

    private void HandleColisionDetection()
    {
        groundDetected = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(primaryWallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround)
                    && Physics2D.Raycast(secondaryWallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);
    }
}
