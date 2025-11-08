namespace circuit;

public interface INode : IEquatable<INode>
{
    int Id { get; }
}