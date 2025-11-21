namespace circuit;

public class Schema : ISchema
{
    private Dictionary<INode, Dictionary<IEdge, INode>> edges;
    private Dictionary<int, INode> nodes;

    public Schema()
    {
        edges = new();
        nodes = new();
    }

    public void AddNode(INode node)
    {
        if(nodes.ContainsKey(node.Id))
        {
            throw new Exception($"Node with id: {node.Id} already exists in schema");
        }

        nodes.Add(node.Id, node);
    }
    public void AddEdge(IEdge edge)
    {
        INode from = nodes[edge.From.Id];
        INode to = nodes[edge.To.Id];
        if (edges.ContainsKey(from) && edges[from].ContainsKey(edge))
        {
            throw new Exception($"Edge with id: {edge.Id} already exists in schema");
        }

        if(!edges.ContainsKey(from))
        {
            edges.Add(from, new());
        }

        if (!edges.ContainsKey(to))
        {
            edges.Add(to, new());
        }

        edges[from].Add(edge, to);
        edges[to].Add(edge.GetReversed(), from);
    }
    public bool HasEdge(IEdge edge)
    {
        foreach ((INode from, var dict) in edges)
        {
            if (dict.ContainsKey(edge)) return true;
        }

        return false;
    }
    public void RemoveEdge(IEdge edge)
    {
        Exception ex = new Exception($"Edge with id: {edge.Id} not found to be deleted");

        if (!edges.ContainsKey(edge.From) || !edges.ContainsKey(edge.To))
        {
            throw ex;
        }

        if (!edges[edge.From].ContainsKey(edge) || !edges[edge.To].ContainsKey(edge))
        {
            throw ex;
        }

        edges[edge.From].Remove(edge);
        edges[edge.To].Remove(edge);
    }

    public IEnumerable<INode> GetNodes(Func<INode, bool>? filter = null)
    {
        HashSet<INode> nodeSet = new();
        foreach ((int id, INode node) in nodes)
        {
            if (filter != null && !filter(node)) continue;
            nodeSet.Add(node);
        }

        return nodeSet.OrderBy(node => node.Id);
    }
    public IEnumerable<IEdge> GetEdges(Func<IEdge, bool>? filter = null)
    {
        HashSet<IEdge> edgeSet = new HashSet<IEdge>();
        foreach ((INode from, var dict) in edges)
        {
            foreach ((IEdge edge, INode to) in dict)
            {
                if (edge.Current.Direction == Direction.Backward) continue;
                if (filter != null && !filter(edge)) continue;
                edgeSet.Add(edge);
            }
        }

        return edgeSet.OrderByDescending(edge => edge.Component.GetPriority()).ToList();
    }

    public ISchema GetTree()
    {
        ISchema schema = GetOnlyNodes();

        var candidats = GetEdges(edge => edge.Component.State?.Type != StateType.Current);
        Queue<IEdge> queue = new Queue<IEdge>();

        foreach (IEdge edge in candidats)
        {
            INode from = edge.From;
            INode to = edge.To;
            schema.AddEdge(edge);
            queue.Enqueue(edge);

            if (schema.GetLoop() != null)
            {
                if (edge.Component.IsDisplacing()) schema.RemoveEdge(queue.Dequeue());
                else schema.RemoveEdge(edge);
            }
        }

        return schema;
    }
    public ISchema GetDiff(ISchema other)
    {
        ISchema schema = GetOnlyNodes();

        foreach(IEdge edge in GetEdges())
        {
            if (other.HasEdge(edge)) continue;

            INode from = edge.From;
            INode to = edge.To;
            schema.AddEdge(edge);
        }

        return schema;
    }
    public ILoop? GetLoop()
    {
        var stack = new Stack<(List<IEdge>, HashSet<INode>, INode)>();
        stack.Push((new List<IEdge>(), new HashSet<INode>(), nodes.First().Value));

        while (stack.Count > 0)
        {
            (List<IEdge> path, HashSet<INode> visited, INode node) = stack.Pop();
            visited.Add(node);
            if (!edges.ContainsKey(node)) continue;

            foreach ((IEdge edge, INode variant) in edges[node])
            {
                var newPath = new List<IEdge>(path) { edge };
                if (!visited.Contains(variant))
                {
                    var newVisited = new HashSet<INode>(visited);
                    stack.Push((newPath, newVisited, variant));
                }
                else
                {
                    if (!edge.Equals(path.Last())) return new Loop(newPath);
                }
            }
        }

        return null;
    }

    public object Clone()
    {
        return GetDiff(GetOnlyNodes());
    }

    private ISchema GetOnlyNodes()
    {
        ISchema schema = new Schema();
        foreach ((int id, INode node) in nodes)
        {
            schema.AddNode((INode)node.Clone());
        }

        return schema;
    }
}
