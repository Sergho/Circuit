namespace circuit;

internal class Edge : IEdge
{
    public int Id { get; private set; }
    public INode From { get; private set; }
    public INode To { get; private set; }
    public IComponent Component { get; private set; }
    public Direction Direction { get; private set; }

    public Edge(int id, INode from, INode to, IComponent component, Direction direction)
    {
        Id = id;
        From = from;
        To = to;
        Component = component;
        Direction = direction;
    }
    public bool Equals(IEdge? other)
    {
        if(other == null) return false;

        return Id == other.Id;
    }
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
