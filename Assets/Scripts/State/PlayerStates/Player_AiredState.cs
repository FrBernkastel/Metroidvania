using UnityEngine;

public class Player_AiredState : PlayerState
{
    public Player_AiredState(Player player, StateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (player.moveInput.x != 0)
            player.SetVelocity(player.moveInput.x * player.moveSpeed * player.inAirMultiplier, rb.linearVelocityY);

        if (input.Player.Attack.WasPressedThisFrame())  // NOTE: this is called first. so it might be overrided by later transit.
        {
            stateMachine.ChangeState(player.jumpAttackState);
        }
    }
}
