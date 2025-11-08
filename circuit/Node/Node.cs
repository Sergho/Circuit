namespace circuit;

public class Node : INode
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
}
