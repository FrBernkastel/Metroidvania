using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Enemy_BattleState : EnemyState
{
    private Transform player;
    public float lastTimeWasInBattle;

    public Enemy_BattleState(Enemy enemy, StateMachine stateMachine, string animBoolName)
        : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if (player == null)
        {
            player = enemy.PlayerDetection().transform;
        }

        if (ShouldRetreat())
        {
            // Cannot flip so set speed directly
            rb.linearVelocity = new Vector2(enemy.retreatVelocity.x * -DirectionToPlayer(), enemy.retreatVelocity.y);
            enemy.HandleFlip(DirectionToPlayer());
        }
    }

    public override void Update()
    {
        base.Update();


        if (enemy.PlayerDetection())
        {
            lastTimeWasInBattle = Time.time;
        }

        if (BattleOverTime())
        {
            stateMachine.ChangeState(enemy.idleState);
            return;
        }

        if (withinAttackRange() && enemy.PlayerDetection())
        {
            stateMachine.ChangeState(enemy.attackState);
        }
        else
        {
            enemy.SetVelocity(enemy.battleMoveSpeed * DirectionToPlayer(), rb.linearVelocityY);
        }
    }

    private bool BattleOverTime() => Time.time - lastTimeWasInBattle > enemy.battleTimeDuration;

    private bool withinAttackRange() => DistanceToPlayer() < enemy.attackDistance;

    private float DistanceToPlayer()
    {
        if (player == null)
        {
            return float.MaxValue;
        }

        return Mathf.Abs(player.position.x - enemy.transform.position.x);
    }

    private int DirectionToPlayer()
    {
        if (player == null)
        {
            return 0;
        }

        return player.position.x > enemy.transform.position.x ? 1 : -1;
    }

    private bool ShouldRetreat() => DistanceToPlayer() < enemy.minRetreatDistance;
}
