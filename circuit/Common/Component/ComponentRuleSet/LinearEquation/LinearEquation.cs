
namespace circuit;

public class LinearEquation : ILinearEquation
{
    private Dictionary<IVariable, double> data;

    public LinearEquation()
    {
        data = new();
    }

    public void Add(IVariable variable, double coefficient)
    {
        if(data.ContainsKey(variable))
        {
            data[variable] += coefficient;
        } else
        {
            data.Add(variable, coefficient);
        }
    }
    public IEnumerable<IVariable> GetVariables()
    {
        return data.Keys;
    }
    public double GetCoefficient(IVariable variable)
    {
        if(!data.ContainsKey(variable))
        {
            throw new Exception($"Variable {variable.Name} not found in equation");
        }

        return data[variable];
    }
}
