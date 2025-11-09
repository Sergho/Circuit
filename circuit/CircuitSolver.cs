namespace circuit;

internal class CircuitSolver
{
    private int currentNodeId = 1;
    private int currentEdgeId = 1;
    private ISchema schema;

    private IEnumerable<double> start;
    private double step;

    public CircuitSolver(IEnumerable<double> start, double step)
    {
        schema = new Schema();

        this.start = start;
        this.step = step;
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

        ISystem system = builder.GetSystem();
        system.Solve();

        ISolution solution = new EulerSolution(system, step, start);

        for(int i = 0; i < 1000; i++)
        {
            Console.WriteLine($"{i}. Time: {solution.GetTime()}");

            Console.Write("X: ");
            foreach(double value in solution.GetX())
            {
                Console.Write($"{value}\t");
            }
            Console.WriteLine();

            Console.Write("Y: ");
            foreach (double value in solution.GetY())
            {
                Console.Write($"{value}\t");
            }
            Console.WriteLine();

            solution.Next();
        }
    }
}
