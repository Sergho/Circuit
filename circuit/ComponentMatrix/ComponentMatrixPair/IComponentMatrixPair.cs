namespace circuit;

public interface IComponentMatrixPair : IEquatable<IComponentMatrixPair>
{
    IComponent Component { get; }
    ICurrent Current { get; }
}
