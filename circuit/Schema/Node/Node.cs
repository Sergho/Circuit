namespace circuit;

public class Node : INode
{
    private static int currentId = 1;

    public int Id { get; private set; }

    private Node(int id)
    {
        Id = id;
    }

    public Node()
    {
        Id = currentId;

        currentId++;
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

    public object Clone()
    {
        return new Node(Id);
    }
}
