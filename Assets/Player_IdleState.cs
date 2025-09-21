using UnityEngine;

public class Player_IdleState : EntityState
{
    public Player_IdleState(Player player, StateMachine stateMachine)
        : base(player, stateMachine: stateMachine, stateName: "idle")
    {

    }

    //public override void Enter()
    //{

    //}

    public override void Update()
    {
        if (player.moveInput.x != 0)
        {
            stateMachine.ChangeState(this.player.moveState);
        }
    }

    //public override void Exit()
    //{

    //}
}
