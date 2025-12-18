namespace circuit;

public interface IEdge : IEquatable<IEdge>
{
    int Id { get; }
    INode From { get; }
    INode To { get; }
    IComponent Component { get; }

    IEdge GetReversed();
    Direction? GetDirectionWith(IEdge other);
}
