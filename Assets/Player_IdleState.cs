using UnityEngine;

public class Player_IdleState : EntityState
{
    public Player_IdleState(Player player, StateMachine stateMachine) 
        : base(player, stateMachine: stateMachine, stateName: "Idle")
    {

    }

    //public override void Enter()
    //{

    //}

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            stateMachine.ChangeState(this.player.moveState);
        }
    }

    //public override void Exit()
    //{

    //}
}
