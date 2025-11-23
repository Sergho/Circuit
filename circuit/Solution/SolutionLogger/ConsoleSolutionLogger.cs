namespace circuit;

public class ConsoleSolutionLogger : ISolutionLogger
{
    public void Log(ISolution solution, double step, int stepsCount)
    {
        solution.Init();
        for (int i = 0; i < stepsCount; i++)
        {
            double time = solution.GetCurrentTime();
            Dictionary<IVariable, double> x = solution.GetCurrentX();
            Dictionary<IVariable, double> y = solution.GetCurrentY();

            Console.WriteLine($"{i}. Time: {time}");

            Console.Write("X: ");
            foreach ((IVariable variable, double value) in x)
            {
                Console.Write($"{variable.Name}: {value}\t");
            }
            Console.WriteLine();

            Console.Write("Y: ");
            foreach ((IVariable variable, double value) in y)
            {
                Console.Write($"{variable.Name}: {value}\t");
            }
            Console.WriteLine();

            solution.Next(step);
        }
    }
}
