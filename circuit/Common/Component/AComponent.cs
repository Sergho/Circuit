namespace circuit;

public abstract class AComponent : IComponent
{
    public IState? State { get; private set; }
    public double Value { get; private set; }

    public AComponent(double value, IState? state = null)
    {
        Value = value;
        State = state;
    }
    public bool IsDisplacing()
    {
        return GetPriority() > 1;
    }
    public abstract int GetPriority();
    public abstract bool IsExternal();
}
