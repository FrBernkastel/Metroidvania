using UnityEngine;

public class Player_MoveState : EntityState
{
    public Player_MoveState(Player player, StateMachine stateMachine)
        : base(player, stateMachine: stateMachine, stateName: "Move")
    {

    }

    //public override void Enter()
    //{

    //}

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            stateMachine.ChangeState(this.player.idleState);
        }
    }

    //public override void Exit()
    //{

    //}
}
