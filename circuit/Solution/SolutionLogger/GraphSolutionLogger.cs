namespace circuit;

public class GraphSolutionLogger : ISolutionLogger
{
    public void Log(ISolution solution, double step, int stepsCount)
    {
        DataConditions dataConditions = new();

        solution.Init();
        for (int i = 0; i < stepsCount; i++)
        {
            double time = solution.GetCurrentTime();
            Dictionary<IVariable, double> x = solution.GetCurrentX();
            Dictionary<IVariable, double> y = solution.GetCurrentY();

            dataConditions.AddCondition(time, x.Values.ToList(), y.Values.ToList());

            solution.Next(step);
        }

        DrawerGraphics drawerGraphics = new(dataConditions);
        drawerGraphics.GenGraphics();
    }
}
