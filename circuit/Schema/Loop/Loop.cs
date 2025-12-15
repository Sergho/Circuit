namespace circuit;

public class Loop : ILoop
{
    private List<IEdge> path;

    public Loop(List<IEdge> path)
    {
        this.path = new();
        SetPath(path);
    }

    public bool Includes(IEdge edge)
    {
        return path.Contains(edge);
    }
    public Direction? CompareDirections(IEdge first, IEdge second)
    {
        if(!Includes(first) || !Includes(second))
        {
            return null;
        }

        bool flipped = false;

        foreach (IEdge edge in path)
        {
            if (edge.Equals(first))
            {
                if (edge.GetDirectionWith(first) == Direction.Backward) flipped = !flipped;
            }

            if (edge.Equals(second))
            {
                if (edge.GetDirectionWith(second) == Direction.Backward) flipped = !flipped;
            }
        }

        return flipped ? Direction.Backward : Direction.Forward;
    }
    public IEnumerable<IEdge> GetPath()
    {
        return path;
    }

    private void SetPath(List<IEdge> path)
    {
        HashSet<INode> visited = new();
        List<IEdge> trimmedRight = new();

        foreach(IEdge edge in path)
        {
            visited.Add(edge.From);
            trimmedRight.Add(edge);
            if (visited.Contains(edge.To)) break;
        }

        bool cutted = false;
        IEdge last = trimmedRight.Last();
        INode node = last.To;

        foreach (IEdge edge in trimmedRight)
        {
            if (!cutted && !edge.From.Equals(node)) continue;
            this.path.Add(edge);
            cutted = true;
        }
    }
}
