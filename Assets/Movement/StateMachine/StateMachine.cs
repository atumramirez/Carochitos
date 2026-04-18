public class StateMachine<T> where T : GenericController
{
    public State<T> currentState;

    public void Initialize(State<T> startingState)
    {
        currentState = startingState;
        startingState.Enter();
    }

    public void ChangeState(State<T> newState)
    {
        currentState.Exit();

        currentState = newState;
        newState.Enter();
    }
}
