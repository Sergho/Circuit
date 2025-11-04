namespace circuit;

internal class System : ISystem
{
    private ISystemMatrix matrix;

    public System(ISystemMatrix matrix)
    {
        this.matrix = matrix;
    }
}
