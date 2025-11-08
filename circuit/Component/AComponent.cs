namespace circuit;

public abstract class AComponent : IComponent
{
    public double Value { get; private set; }

    public AComponent(double value)
    {
        Value = value;
    }
    public bool IsDisplacing()
    {
        return GetPriority() > 1;
    }
    public abstract int GetPriority();
    public abstract StateType GetStateType();
    public abstract bool IsExternal();
}
