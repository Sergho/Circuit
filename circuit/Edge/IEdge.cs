namespace circuit;

internal interface IEdge : IEquatable<IEdge>
{
    int Id { get; }
    INode From { get; }
    INode To { get; }
    IComponent Component { get; }
    ICurrent Current { get; }

    IEdge GetReversed();
}
