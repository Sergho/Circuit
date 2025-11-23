namespace circuit;

internal class Program
{
    private static readonly List<double> start = new() { 18, 0 };
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

        //ISolution solution = new EulerSolution(system, step, start);

        //DataConditions dataConditions = new();

        //for (int i = 0; i < 0; i++)
        //{
        //    double time = solution.GetTime();
        //    List<double> listX = solution.GetX().ToList();
        //    List<double> listY = solution.GetY().ToList();

        //    dataConditions.AddCondition(time, listX, listY);

        //    Console.WriteLine($"{i}. Time: {time}");

        //    Console.Write("X: ");
        //    foreach (double value in listX)
        //    {
        //        Console.Write($"{value}\t");
        //    }
        //    Console.WriteLine();

        //    Console.Write("Y: ");
        //    foreach (double value in listY)
        //    {
        //        Console.Write($"{value}\t");
        //    }
        //    Console.WriteLine();

        //    solution.Next();
        //}

        //DrawerGraphics drawerGraphics = new(dataConditions);
        //drawerGraphics.GenGraphics();
    }
}