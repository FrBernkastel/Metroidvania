using UnityEngine;

public class Player : MonoBehaviour
{
    
    private PlayerInputSet input;
    public StateMachine stateMachine { get; private set; }

    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }

    public Vector2 moveInput;

    private void Awake()
    {
        stateMachine = new StateMachine();
        input = new PlayerInputSet(); // parameterless create.
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
        stateMachine.currentState.Update();
    }
}
