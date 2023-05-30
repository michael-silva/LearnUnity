public interface ICharacterState<T>
{
    ICharacterState<T> Execute(T context);
    void OnEnterState(T context);
    void OnExitState(T context);
}

public interface ICharacterStateMachine<T>
{
    void ChangeState(ICharacterState<T> newState);
}