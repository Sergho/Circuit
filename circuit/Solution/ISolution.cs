namespace circuit;

public interface ISolution
{
    Dictionary<IVariable, double> GetCurrentX();
    Dictionary<IVariable, double> GetCurrentY();
    double GetCurrentTime();

    void Next(double step);
    void Init();
}
