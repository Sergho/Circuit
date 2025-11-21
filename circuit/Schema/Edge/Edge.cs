namespace circuit;

public class Edge : IEdge
{
    private static int currentId = 1;

    public int Id { get; private set; }
    public INode From { get; private set; }
    public INode To { get; private set; }
    public IComponent Component { get; private set; }
    public ICurrent Current { get; private set; }

    private Edge(int id, INode from, INode to, IComponent component, ICurrent current)
    {
        Id = id;
        From = from;
        To = to;
        Component = component;
        Current = current;
    }

    public Edge(INode from, INode to, IComponent component)
    {
        From = from;
        To = to;
        Component = component;
        Current = component.State?.Type == StateType.Current ?
            new Current(Direction.Forward, component.State.Variable) :
            new Current(Direction.Forward);
        
        Id = currentId;
        currentId++;
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
