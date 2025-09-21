using UnityEngine;

public class Player_MoveState : EntityState
{
    public Player_MoveState(Player player, StateMachine stateMachine)
        : base(player, stateMachine: stateMachine, stateName: "move")
    {

    }

    //public override void Enter()
    //{

    //}

    public override void Update()
    {
        if (player.moveInput.x == 0)
        {
            stateMachine.ChangeState(this.player.idleState);
        }

        player.SetVelocity(player.moveSpeed * player.moveInput.x, rb.linearVelocityY);
    }

    //public override void Exit()
    //{

    //}
}
