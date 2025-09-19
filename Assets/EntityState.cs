using UnityEngine;

public abstract class EntityState
{
    protected Player player;
    protected StateMachine stateMachine;
    protected string stateName;

    public EntityState(Player player, StateMachine stateMachine, string stateName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.stateName = stateName;
    }

    public virtual void Enter()
    {
        Debug.Log("I enter state " + this.stateName);
    }

    public abstract void Update();

    public virtual void Exit()
    {
        Debug.Log("I exit state " + this.stateName);
    }
}
