using UnityEngine;

public abstract class EntityState
{
    protected Player player;
    protected Rigidbody2D rb;
    protected Animator anim;
    protected StateMachine stateMachine;
    protected string animBoolName;

    public EntityState(Player player, StateMachine stateMachine, string stateName)
    {
        this.player = player;
        this.rb = player.rb;
        this.anim = player.anim;
        this.stateMachine = stateMachine;
        this.animBoolName = stateName;
    }

    public virtual void Enter()
    {
        this.anim.SetBool(animBoolName, true);
    }

    public abstract void Update();

    public virtual void Exit()
    {
        this.anim.SetBool(animBoolName, false);
    }
}
