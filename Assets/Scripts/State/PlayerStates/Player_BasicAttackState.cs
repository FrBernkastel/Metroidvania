using UnityEngine;

public class Player_BasicAttackState : PlayerState
{
    private float attackVelocityTimer;
    private const int FirstComboIndex = 1;
    private int comboIndex = 1;
    private int comboIndexLimit = 3;
    private bool comboAttackQueued;

    private int attackDir;

    private float lastTimeAttacked;

    public Player_BasicAttackState(Player player, StateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName)
    {
        if (comboIndexLimit != player.attackVelocity.Length)
        {
            Debug.LogWarning("I've adjusted combo limit, according to attack velocity array!");
            comboIndexLimit = player.attackVelocity.Length;
        }
    }

    public override void Enter()
    {
        base.Enter();
        comboAttackQueued = false;
        ResetComboIndexIfNeeded();
        anim.SetInteger("basicAttackIndex", comboIndex);

        // Determine the attack dir by checking if press the movement buttons.
        attackDir = player.moveInput.x != 0 ? (int)player.moveInput.x : player.facingDir;

        ApplyAttackVelocity();
    }

    public override void Update()
    {
        base.Update();
        HandleAttackVelocity();

        // Detect and Damage

        if (input.Player.Attack.WasPressedThisFrame())
            QueueNextAttack();

        if (triggeredCall)
            HandleAnimExit();
    }

    public override void Exit()
    {
        base.Exit();

        comboIndex += 1;
        lastTimeAttacked = Time.time;
    }

    private void HandleAnimExit()
    {
        if (comboAttackQueued)
        {
            // Make the anim exit at this frame.
            // The actual state transition with new anim will be set in Coroutine
            anim.SetBool("basicAttack", false);
            player.EnterAttackWithDelay();
        }
        else
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    private void HandleAttackVelocity()
    {
        attackVelocityTimer -= Time.deltaTime;

        if (attackVelocityTimer < 0)
        {
            player.SetVelocity(0, rb.linearVelocityY);
        }
    }

    private void ApplyAttackVelocity()
    {
        attackVelocityTimer = player.attackVelocityDuration;
        player.SetVelocity(player.attackVelocity[comboIndex - 1].x * attackDir, player.attackVelocity[comboIndex - 1].y);
    }

    private void QueueNextAttack()
    {
        if (comboIndex < comboIndexLimit)
        {
            comboAttackQueued = true;
        }
    }

    private void ResetComboIndexIfNeeded()
    {
        if (Time.time - lastTimeAttacked > player.comboResetTime
            || comboIndex > comboIndexLimit)
        {
            comboIndex = FirstComboIndex;
        }
    }
}
