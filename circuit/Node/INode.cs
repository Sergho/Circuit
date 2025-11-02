namespace circuit;

internal interface INode : IEquatable<INode>
{
    int Id { get; }
    void BindEdge(IEdge edge);
}