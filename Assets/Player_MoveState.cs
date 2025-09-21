using UnityEngine;

public class Player_MoveState : Player_GroundedState
{
    public Player_MoveState(Player player, StateMachine stateMachine, string animBoolName)
        : base(player, stateMachine: stateMachine, animBoolName: animBoolName)
    {

    }

    public override void Update()
    {
        base.Update();

        if (player.moveInput.x == 0)
        {
            stateMachine.ChangeState(this.player.idleState);
        }

        player.SetVelocity(player.moveSpeed * player.moveInput.x, rb.linearVelocityY);
    }
}
