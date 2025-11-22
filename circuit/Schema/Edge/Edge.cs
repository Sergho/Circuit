namespace circuit;

public class Edge : IEdge
{
    private static int currentId = 1;

    public int Id { get; private set; }
    public INode From { get; private set; }
    public INode To { get; private set; }
    public IComponent Component { get; private set; }

    private Edge(int id, INode from, INode to, IComponent component)
    {
        Id = id;
        From = from;
        To = to;
        Component = component;
    }

    public Edge(INode from, INode to, IComponent component)
    {
        From = from;
        To = to;
        Component = component;
        
        Id = currentId;
        currentId++;
    }
    
    public IEdge GetReversed()
    {
        return new Edge(Id, To, From, Component);
    }
    public Direction? GetDirectionWith(IEdge other)
    {
        if (From == other.From && To == other.To) return Direction.Forward;
        if (From == other.To && To == other.From) return Direction.Backward;

        return null;
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
