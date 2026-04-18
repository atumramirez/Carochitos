using UnityEngine;

public class State<T> where T : GenericController
{
    public T character;
    public StateMachine<T> stateMachine;

    protected Vector3 gravityVelocity;
    protected Vector3 velocity;
    protected Vector2 input;

    public State(T _character, StateMachine<T> _stateMachine)
	{
        character = _character;
        stateMachine = _stateMachine;
    }

    public virtual void Enter()
    {
        Debug.Log("enter state: " + this.ToString());
    }

    public virtual void LogicUpdate() // O mesmo que Update()
    {

    }

    public virtual void PhysicsUpdate() // O mesmo que FixedUpdate()
    {

    }

    public virtual void Exit()
    {

    }
}

