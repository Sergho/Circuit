namespace circuit;

internal class CircuitSolver
{
    private int currentNodeId = 1;
    private int currentEdgeId = 1;
    private ISchema schema;

    public CircuitSolver()
    {
        schema = new Schema();
    }

    public INode CreateNode()
    {
        INode node = new Node(currentNodeId);
        schema.AddNode(node);
        currentNodeId++;

        return node;
    }
    public void RemoveNode(int id)
    {
        schema.RemoveNode(id);
    }
    public IEdge LinkNodes(int fromId, int toId, IComponent component)
    {
        INode from = schema.GetNode(fromId);
        INode to = schema.GetNode(toId);
        IEdge edge = new Edge(currentEdgeId, from, to, component, Direction.Forward);
        schema.AddEdge(edge);
        currentEdgeId++;

        return edge;
    }
    public void Solve()
    {
        var matrix = schema.GetMatrix();

        foreach (IEdge addition in matrix.GetRows())
        {
            foreach (IEdge edge in matrix.GetCols())
            {
                Console.WriteLine($"Addition: {addition.Id}, Edge: {edge.Id}, Value: {matrix.Get(addition, edge)}");
            }
        }
    }
}
