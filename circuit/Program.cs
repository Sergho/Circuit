namespace circuit;

internal class Program
{
    public static void Main()
    {
        CircuitSolver solver = new CircuitSolver();

        solver.CreateNode();
        solver.CreateNode();
        solver.CreateNode();

        solver.LinkNodes(1, 2, new Capacitor(0));
        solver.LinkNodes(1, 2, new Resistor(0));
        solver.LinkNodes(2, 1, new PowerSource(0));
        solver.LinkNodes(1, 3, new Inductance(0));
        solver.LinkNodes(3, 2, new Resistor(0));

        solver.Solve();
    }
}