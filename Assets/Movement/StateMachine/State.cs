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

    }

    public virtual void LogicUpdate() // Used in Update()
    {

    }

    public virtual void PhysicsUpdate() // Used in FixedUpdate()
    {

    }

    public virtual void Exit()
    {

    }
}

