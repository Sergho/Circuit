namespace circuit;

internal interface INode : IEquatable<INode>
{
    int Id { get; }

    void AddEdge(IEdge edge);
}