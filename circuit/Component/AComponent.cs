namespace circuit;

internal abstract class AComponent : IComponent
{
    private double value;

    public AComponent(double value)
    {
        this.value = value;
    }
    public bool isDisplacing()
    {
        return getPriority() > 1;
    }
    public abstract int getPriority();
}
