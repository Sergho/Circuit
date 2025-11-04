namespace circuit;

internal class Edge : IEdge
{
    public int Id { get; private set; }
    public INode From { get; private set; }
    public INode To { get; private set; }
    public IComponent Component { get; private set; }
    public ICurrent Current { get; private set; }

    public Edge(int id, INode from, INode to, IComponent component, ICurrent current)
    {
        Id = id;
        From = from;
        To = to;
        Component = component;
        Current = current;
    }
    
    public IEdge GetReversed()
    {
        return new Edge(Id, To, From, Component, Current.GetReversed());
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
