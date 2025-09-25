using UnityEngine;

public abstract class PlayerState : EntityState
{
    protected Player player;

    protected PlayerInputSet input;

    public PlayerState(Player player, StateMachine stateMachine, string animBoolName)
        : base(stateMachine, animBoolName)
    {
        this.player = player;
        this.rb = player.rb;  // here is what I feel uncomfortable. Why the rb and anim which is from base class, is init-ed by child class?
        this.anim = player.anim;
        input = player.input;
    }

    public override void Update()
    {
        base.Update();

        anim.SetFloat("yVelocity", rb.linearVelocityY);
        if (input.Player.Dash.WasPressedThisFrame() && CanDash())
        {
            stateMachine.ChangeState(player.dashState);
        }
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
