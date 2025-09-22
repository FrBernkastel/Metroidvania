using UnityEngine;

public abstract class EntityState
{
    protected Player player;
    protected Rigidbody2D rb;
    protected Animator anim;
    protected PlayerInputSet input;

    protected StateMachine stateMachine;
    protected string animBoolName;

    protected float stateTimer;
    protected bool triggeredCall;

    public EntityState(Player player, StateMachine stateMachine, string animBoolName)
    {
        this.player = player;
        this.rb = player.rb;
        this.anim = player.anim;
        input = player.input;

        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        this.anim.SetBool(animBoolName, true);
        triggeredCall = false;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;

        anim.SetFloat("yVelocity", rb.linearVelocityY);
        if (input.Player.Dash.WasPressedThisFrame() && CanDash())
        {
            stateMachine.ChangeState(player.dashState);
        }
    }

    public virtual void Exit()
    {
        this.anim.SetBool(animBoolName, false);
    }

    public void CallAnimationTrigger()
    {
        triggeredCall = true;
    }

    private bool CanDash()
    {
        if (player.wallDetected)
            return false;
        if (stateMachine.currentState == player.dashState)
            return false;

        return true;
    }
}
