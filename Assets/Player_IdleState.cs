using UnityEngine;

public class Player_IdleState : Player_GroundedState
{
    public Player_IdleState(Player player, StateMachine stateMachine, string animBoolName)
        : base(player, stateMachine: stateMachine, animBoolName: animBoolName)
    {

    }

    public override void Enter()
    {
        player.SetVelocity(0, rb.linearVelocityY);    
    }

    public override void Update()
    {
        base.Update();

        if (player.moveInput.x != 0)
        {
            stateMachine.ChangeState(this.player.moveState);
        }
    }

}
