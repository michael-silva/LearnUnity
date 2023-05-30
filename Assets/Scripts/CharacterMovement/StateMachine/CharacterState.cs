public abstract class CharacterState<T> : ICharacterState<T>
{
    public virtual ICharacterState<T> Execute(T context)
    {
        return this;
    }
    public virtual void OnEnterState(T context) { }
    public virtual void OnExitState(T context) { }
}