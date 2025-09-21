using UnityEngine;

public abstract class EntityState
{
    protected Player player;
    protected Rigidbody2D rb;
    protected Animator anim;
    protected PlayerInputSet input;

    protected StateMachine stateMachine;
    protected string animBoolName;

    public EntityState(Player player, StateMachine stateMachine, string animBoolName)
    {
        this.player = player;
        this.rb = player.rb;
        this.anim = player.anim;
        input = player.input;

        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        this.anim.SetBool(animBoolName, true);
    }

    public virtual void Update()
    {
        anim.SetFloat("yVelocity", rb.linearVelocityY);
    }

    public virtual void Exit()
    {
        this.anim.SetBool(animBoolName, false);
    }
}
