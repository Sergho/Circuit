namespace circuit;

internal class Node : INode
{
    private List<IEdge> edges;

    public int Id { get; private set; }

    public Node(int id)
    {
        edges = new List<IEdge>();
        Id = id;
    }

    public bool Equals(INode? other)
    {
        if(other == null) return false;

        return Id == other.Id;
    }
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
    public void AddEdge(IEdge edge)
    {
        if (edge.From != this) return;
        edges.Add(edge);
    }
}
