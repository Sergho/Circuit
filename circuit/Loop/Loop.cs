namespace circuit;

internal class Loop : ILoop
{
    private List<IEdge> path;

    public Loop(List<IEdge> path)
    {
        this.path = new();
        SetPath(path);
    }

    public void SetPath(List<IEdge> path)
    {
        bool cutted = false;
        IEdge last = path.Last();
        INode node = last.To;

        foreach (IEdge edge in path)
        {
            if (!cutted && !edge.From.Equals(node)) continue;
            this.path.Add(edge);
            cutted = true;
        }
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
                if (edge.Current.Direction != first.Current.Direction) flipped = !flipped;
            }

            if (edge.Equals(second))
            {
                if (edge.Current.Direction != second.Current.Direction) flipped = !flipped;
            }
        }

        return flipped ? Direction.Backward : Direction.Forward;
    }
}
