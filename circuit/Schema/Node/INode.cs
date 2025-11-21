namespace circuit;

public interface INode : IEquatable<INode>, ICloneable
{
    int Id { get; }
}