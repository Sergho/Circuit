using System.Reflection.Metadata;
using System.Runtime.InteropServices.Marshalling;

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

            if (hasLoop(result.ToList()))
            {
                if (edge.Component.isDisplacing()) result.RemoveFirst();
                else result.RemoveLast();
            }
        }

        return result.ToList();
    }
    private bool hasLoop(List<IEdge> edges)
    {
        var map = getTravelMap(edges);
        var visited = new HashSet<INode>(); 

        var stack = new Stack<(IEdge?, INode)>();
        stack.Push((null, nodes.First().Value));

        while(stack.Count > 0)
        {
            (IEdge? prevEdge, INode node) = stack.Pop();
            visited.Add(node);
            if (!map.ContainsKey(node)) continue;

            foreach((IEdge edge, INode variant) in map[node])
            {
                if(!visited.Contains(variant))
                {
                    stack.Push((edge, variant));
                } else
                {
                    if (edge != prevEdge) return true;
                }
            }
        }

        return false;
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
    private Direction? getCircuitDirection(List<IEdge> tree, IEdge addition, IEdge target)
    {
        var map = getTravelMap(tree);

        var stack = new Stack<(IEdge, INode)>();
        if(addition.Direction == Direction.Forward)
        {
            stack.Push((addition, addition.To));
        } else
        {
            stack.Push((addition, addition.From));
        }

        while (stack.Count > 0)
        {
            (IEdge? prevEdge, INode node) = stack.Pop();
            if (!map.ContainsKey(node)) continue;

            foreach ((IEdge edge, INode variant) in map[node])
            {
                if (edge == prevEdge) continue;
                if (edge == target) return target.Direction;

                stack.Push((edge, variant));
            }
        }

        return null;
    }
}
