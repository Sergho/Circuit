namespace circuit;

internal class Program
{
    public static void Main()
    {
        CircuitSolver solver = new CircuitSolver();

        solver.CreateNode();
        solver.CreateNode();
        solver.CreateNode();

        solver.LinkNodes(1, 2, new Capacitor(4), 3);
        solver.LinkNodes(1, 2, new Resistor(3), 2);
        solver.LinkNodes(2, 1, new PowerSource(6), 4);
        solver.LinkNodes(1, 3, new Inductance(5), 1);
        solver.LinkNodes(3, 2, new Resistor(2), 1);

        solver.Solve();
    }
}