namespace circuit;

public interface ISolutionLogger
{
    void Log(ISolution solution, double step, int stepsCount);
}
