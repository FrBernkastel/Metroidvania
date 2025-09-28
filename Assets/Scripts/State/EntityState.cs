using UnityEngine;

public abstract class EntityState
{
    protected StateMachine stateMachine;
    protected string animBoolName;

    protected Rigidbody2D rb;
    protected Animator anim;

    protected float stateTimer;
    protected bool triggeredCall;

    public EntityState(StateMachine stateMachine, string animBoolName)
    {
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        this.anim.SetBool(animBoolName, true);
        triggeredCall = false;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
        UpdateAnimationParameters();
    }

    public virtual void Exit()
    {
        this.anim.SetBool(animBoolName, false);
    }

    public void AnimationTrigger()
    {
        triggeredCall = true;
    }

    public virtual void UpdateAnimationParameters() 
    { 
    
    }
}
