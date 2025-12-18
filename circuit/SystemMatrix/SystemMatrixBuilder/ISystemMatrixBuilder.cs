namespace circuit;

public interface ISystemMatrixBuilder
{
    ISystemMatrix Build(IComponentMatrix componentMatrix);
}
