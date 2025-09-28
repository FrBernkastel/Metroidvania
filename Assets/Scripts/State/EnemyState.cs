using UnityEngine;

public class EnemyState : EntityState
{
    protected Enemy enemy;

    public EnemyState(Enemy enemy, StateMachine stateMachine, string animBoolName) 
        : base(stateMachine, animBoolName)
    {
        this.enemy = enemy;
        this.rb = enemy.rb;
        this.anim = enemy.anim;
    }

    public override void UpdateAnimationParameters()
    {
        base.UpdateAnimationParameters();

        anim.SetFloat("xVelocity", rb.linearVelocityX);

        anim.SetFloat("moveAnimSpeedMultiplier", enemy.moveAnimSpeedMultiplier);
        float battleAnimSpeedRatio = enemy.battleMoveSpeed / enemy.moveSpeed;
        anim.SetFloat("battleAnimSpeedMultiplier", enemy.moveAnimSpeedMultiplier * battleAnimSpeedRatio);
    }
}
