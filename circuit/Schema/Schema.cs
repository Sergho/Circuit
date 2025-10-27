namespace circuit;

internal class Schema : ISchema
{
    private int nextEdgeId;
    private HashSet<IEdge> edges;
    private Dictionary<int, INode> nodes;

    public Schema(List<INode> nodes)
    {
        nextEdgeId = 1;
        edges = new HashSet<IEdge>();
        this.nodes = new Dictionary<int, INode>();
        foreach(INode node in nodes)
        {
            this.nodes.Add(node.Id, node);
        } 
    }
    public INode getNode(int id)
    {
        if(!nodes.ContainsKey(id))
        {
            throw new Exception("Node not found");
        }

        return nodes[id];
    }
    public void LinkNode(int fromId, int toId, IComponent component, Direction direction = Direction.Forward)
    {
        INode from = getNode(fromId);
        INode to = getNode(toId);

        IEdge edge = new Edge(nextEdgeId, from, to, component, direction);
        from.AddEdge(edge);

        Direction reversedDirection = direction == Direction.Forward ? Direction.Backward : Direction.Forward;
        IEdge reversedEdge = new Edge(nextEdgeId, to, from, component, reversedDirection);
        to.AddEdge(reversedEdge);

        edges.Add(edge);
        nextEdgeId++;
    }
    public Dictionary<IEdge, Dictionary<IEdge, int>> getMatrix()
    {
        var matrix = new Dictionary<IEdge, Dictionary<IEdge, int>>();
        var tree = buildTree();
        var additionEdges = getAdditionEdges(tree).Where(edge => edge.Component is not Capacitor).ToList();

        foreach(IEdge addition in additionEdges)
        {
            if (!matrix.ContainsKey(addition))
            {
                matrix.Add(addition, new Dictionary<IEdge, int>());
            }

            foreach (IEdge edge in tree)
            {
                Direction? cell = getCircuitDirection(tree, addition, edge);

                if (cell == null) matrix[addition].Add(edge, 0);
                if (cell == Direction.Forward) matrix[addition].Add(edge, 1);
                if (cell == Direction.Backward) matrix[addition].Add(edge, -1);
            }
        }

        return matrix;
    }

    private List<IEdge> getEdges()
    {
        return edges.OrderByDescending(edge => edge.Component.getPriority()).ToList();
    }
    private List<IEdge> getAdditionEdges(List<IEdge> tree)
    {
        var excluded = new HashSet<IEdge>(edges);
        foreach (IEdge edge in tree)
        {
            excluded.Remove(edge);
        }

        return excluded.OrderByDescending(edge => edge.Component.getPriority()).ToList();
    }
    private List<IEdge> buildTree()
    {
        var edges = getEdges().Where(edge => edge.Component is not Inductance).ToList();
        var result = new LinkedList<IEdge>();

        foreach (IEdge edge in edges)
        {
            result.AddLast(edge);

            if (getLoop(result.ToList()) != null)
            {
                if (edge.Component.isDisplacing()) result.RemoveFirst();
                else result.RemoveLast();
            }
        }

        return result.ToList();
    }
    private Dictionary<INode, Dictionary<IEdge, INode>> getTravelMap(List<IEdge> edges)
    {
        var map = new Dictionary<INode, Dictionary<IEdge, INode>>();

        foreach (IEdge edge in edges)
        {
            Direction reversedDirection = edge.Direction == Direction.Forward ? Direction.Backward : Direction.Forward;
            Edge reversedEdge = new Edge(edge.Id, edge.To, edge.From, edge.Component, reversedDirection);

            if (!map.ContainsKey(edge.From)) map.Add(edge.From, new Dictionary<IEdge, INode>());
            if (!map.ContainsKey(edge.To)) map.Add(edge.To, new Dictionary<IEdge, INode>());

            if (!map[edge.From].ContainsKey(edge)) map[edge.From].Add(edge, edge.To);
            if (!map[edge.To].ContainsKey(edge)) map[edge.To].Add(reversedEdge, edge.From);
        }

        return map;
    }
    private List<IEdge>? getLoop(List<IEdge> edges)
    {
        var map = getTravelMap(edges);
        var stack = new Stack<(List<IEdge>, HashSet<INode>, INode)>();
        stack.Push((new List<IEdge>(), new HashSet<INode>(), nodes.First().Value));

        while(stack.Count > 0)
        {
            (List<IEdge> path, HashSet<INode> visited, INode node) = stack.Pop();
            visited.Add(node);
            if (!map.ContainsKey(node)) continue;

            foreach((IEdge edge, INode variant) in map[node])
            {
                var newPath = new List<IEdge>(path) { edge };
                if (!visited.Contains(variant))
                {
                    var newVisited = new HashSet<INode>(visited);
                    stack.Push((newPath, newVisited, variant));
                } else
                {
                    if (!edge.Equals(path.Last())) return cutPath(newPath);
                }
            }
        }

        return null;
    }
    private List<IEdge> cutPath(List<IEdge> path)
    {
        bool cutted = false;
        IEdge last = path.Last();
        INode node = last.To;
        var result = new List<IEdge>(); 

        foreach(IEdge edge in path)
        {
            if (!cutted && edge.From != node) continue;
            result.Add(edge);
            cutted = true;
        }

        return result;
    }
    private Direction? getCircuitDirection(List<IEdge> tree, IEdge addition, IEdge target)
    {
        bool flipped = false;
        bool includes = false;
        var loop = getLoop(new List<IEdge>(tree) { addition });

        if (loop == null) return null;

        foreach(IEdge edge in loop)
        {
            if (edge.Equals(target))
            {
                includes = true;
                if (edge.Direction != target.Direction) flipped = !flipped;
            }

            if (edge.Equals(addition))
            {
                if (edge.Direction != target.Direction) flipped = !flipped;
            }
        }

        if (!includes) return null;

        return flipped ? Direction.Backward : Direction.Forward;
    }
}
