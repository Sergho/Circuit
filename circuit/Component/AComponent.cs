namespace circuit;

internal abstract class AComponent : IComponent
{
    private double value;

    public AComponent(double value)
    {
        this.value = value;
    }
    public bool IsDisplacing()
    {
        return GetPriority() > 1;
    }
    public abstract int GetPriority();
    public abstract StateType GetStateType();
    public abstract bool IsExternal();
}
