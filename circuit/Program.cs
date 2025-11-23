namespace circuit;

internal class Program
{
    private static readonly double step = 0.1;
    private static readonly int stepsCount = 1000;

    public static void Main()
    {
        ISchema schema = new Schema();

        List<INode> nodes = new List<INode>() { new Node(), new Node(), new Node() };
        List<IEdge> edges = new List<IEdge>()
        {
            new Edge(nodes[0], nodes[1], new Capacitor("C", 4)),
            new Edge(nodes[0], nodes[1], new Resistor("R2", 2)),
            new Edge(nodes[1], nodes[0], new PowerSource("J", 6)),
            new Edge(nodes[0], nodes[2], new Inductance("L", 5)),
            new Edge(nodes[2], nodes[1], new Resistor("R1", 3)),
        };

        foreach (INode node in nodes)
        {
            schema.AddNode(node);
        }

        foreach (IEdge edge in edges)
        {
            schema.AddEdge(edge);
        }

        ISchemaLogger schemaLogger = new SchemaLogger();
        schemaLogger.Log(schema);

        Console.WriteLine();

        IComponentMatrixBuilder componentMatrixBuilder = new ComponentMatrixBuilder();
        IComponentMatrixLogger componentMatrixLogger = new ComponentMatrixLogger();
        IComponentMatrix componentMatrix = componentMatrixBuilder.BuildMatrix(schema);
        componentMatrixLogger.Log(componentMatrix);

        Console.WriteLine();

        ISystemMatrixBuilder systemMatrixBuilder = new SystemMatrixBuilder();
        ISystemMatrixLogger systemMatrixLogger = new SystemMatrixLogger();
        ISystemMatrix system = systemMatrixBuilder.Build(componentMatrix);
        systemMatrixLogger.Log(system);

        Console.WriteLine();

        ISystemMatrixSolver systemMatrixSolver = new GausSystemMatrixSolver();
        systemMatrixSolver.Solve(system);
        systemMatrixLogger.Log(system);

        Dictionary<IVariable, double> start = new()
        {
            { edges[0].Component.Voltage, 18 },
            { edges[3].Component.Current, 0 },
        };
        ISolution solution = new EulerSolution(system, start);
        ISolutionLogger consoleSolutionLogger = new ConsoleSolutionLogger();
        ISolutionLogger graphSolutionLogger = new GraphSolutionLogger();

        consoleSolutionLogger.Log(solution, step, stepsCount);
        graphSolutionLogger.Log(solution, step, stepsCount);
    }
}