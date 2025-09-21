using UnityEngine;

public class Player : MonoBehaviour
{

    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }

    private PlayerInputSet input;
    public StateMachine stateMachine { get; private set; }

    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }

    public Vector2 moveInput;

    [Header("Movement details")]
    public float moveSpeed;
    private bool facedRight = true;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        stateMachine = new StateMachine();
        input = new PlayerInputSet(); // parameterless create

        idleState = new Player_IdleState(this, stateMachine);
        moveState = new Player_MoveState(this, stateMachine);
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
        stateMachine.UpdateStateActivity();
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.linearVelocity = new Vector2(xVelocity, yVelocity);
        HandleFlip(xVelocity);
    }

    private void HandleFlip(float xVelocity)
    {
        if(facedRight && xVelocity < 0)
        {
            Flip();
        }

        if(!facedRight && xVelocity > 0)
        {
            Flip();
        }
    }

    private void Flip()
    {
        transform.Rotate(0, 180, 0);
        facedRight = !facedRight;
    }
}
