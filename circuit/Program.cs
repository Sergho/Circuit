namespace circuit;

internal class Program
{
    private static readonly double step = 0.01;
    private static readonly int stepsCount = 10000;

    public static void Main()
    {
        ISchema schema = new Schema();

        List<INode> nodes = new List<INode>() { new Node(), new Node(), new Node(), new Node(), new Node(), new Node() };
        //List<IEdge> edges = new List<IEdge>()
        //{
        //    new Edge(nodes[0], nodes[1], new VoltagePowerSource("Jzn", 2)),
        //    new Edge(nodes[1], nodes[0], new Capacitor("Czn", 3)),
        //    new Edge(nodes[2], nodes[1], new Capacitor("Czs", 4)),
        //    new Edge(nodes[2], nodes[0], new Resistor("Rcn", 5)),
        //    new Edge(nodes[2], nodes[0], new Capacitor("Ccn", 6)),
        //    new Edge(nodes[2], nodes[3], new Resistor("R", 7)),
        //    new Edge(nodes[3], nodes[0], new VoltagePowerSource("Jn", 8)),
        //    new Edge(nodes[2], nodes[0], new Capacitor("Cn", 9)),
        //    new Edge(nodes[2], nodes[0], new CurrentPowerSource("CUzn", 10))
        //};

        List<IEdge> edges = new List<IEdge>()
        {
            new Edge(nodes[0], nodes[1], new VoltagePowerSource("Uзн1", 4)),
            new Edge(nodes[1], nodes[0], new Capacitor("Cзн1", 5)),
            new Edge(nodes[1], nodes[2], new Capacitor("Cзс1", 5)),
            new Edge(nodes[2], nodes[0], new CurrentPowerSource("SUзн1", 0)),
            new Edge(nodes[2], nodes[0], new Capacitor("Cсн1", 5)),
            new Edge(nodes[2], nodes[3], new Capacitor("Cсн2", 5)),
            new Edge(nodes[3], nodes[2], new CurrentPowerSource("SUзн2", 4)),
            new Edge(nodes[4], nodes[2], new Capacitor("Cзн2", 5)),
            new Edge(nodes[2], nodes[4], new VoltagePowerSource("Uзн2", 0)),
            new Edge(nodes[3], nodes[4], new Capacitor("Cзс2", 5)),
            new Edge(nodes[3], nodes[0], new Capacitor("Cn", 5)),
            new Edge(nodes[3], nodes[5], new Capacitor("Cсн3", 5)),
            new Edge(nodes[5], nodes[3], new CurrentPowerSource("SUзн3", 4)),
            new Edge(nodes[3], nodes[5], new Capacitor("Cзн3", 5)),
            new Edge(nodes[0], nodes[5], new VoltagePowerSource("Un", 2)),
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
            { edges[1].Component.Voltage, 0 },
            { edges[2].Component.Voltage, 0 },
            { edges[4].Component.Voltage, 0 },
            { edges[5].Component.Voltage, 0 },
            { edges[7].Component.Voltage, 0 },
            { edges[9].Component.Voltage, 0 },
            { edges[10].Component.Voltage, 0 },
            { edges[11].Component.Voltage, 0 },
            { edges[13].Component.Voltage, 0 },
        };
        ISolution solution = new EulerSolution(system, start);
        ISolutionLogger consoleSolutionLogger = new ConsoleSolutionLogger();
        ISolutionLogger graphSolutionLogger = new GraphSolutionLogger();

        Console.WriteLine();

        consoleSolutionLogger.Log(solution, step, stepsCount);
        graphSolutionLogger.Log(solution, step, stepsCount);
    }
}