namespace circuit;

public interface ILinearEquation
{
    public void Add(IVariable variable, double coefficient);
    public IEnumerable<IVariable> GetVariables();
    public double GetCoefficient(IVariable variable);
}
