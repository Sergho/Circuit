using System.Collections.Specialized;

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
    public IEdge LinkNodes(int fromId, int toId, IComponent component, int currentId)
    {
        INode from = schema.GetNode(fromId);
        INode to = schema.GetNode(toId);
        ICurrent current = new Current(currentId, Direction.Forward);
        IEdge edge = new Edge(currentEdgeId, from, to, component, current);
        schema.AddEdge(edge);
        currentEdgeId++;

        return edge;
    }
    public void Solve()
    {
        ISystemBuilder builder = new SystemBuilder(schema);
        builder.Init();

        ISystemMatrix matrix = builder.GetMatrix();

        foreach (int col in matrix.GetCols())
        {
            Console.Write($"{col}\t");
        }
        Console.WriteLine();

        foreach (int row in matrix.GetRows())
        {
            foreach(int col in matrix.GetCols())
            {
                Console.Write($"{matrix.Get(row, col)}\t");
            }
            Console.WriteLine();
        }
    }
}
