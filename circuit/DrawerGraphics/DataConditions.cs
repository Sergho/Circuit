using System.Xml;

namespace circuit;

public class DataConditions
{
    private List<Dictionary<IVariable, double>> conditionsX;
    private List<Dictionary<IVariable, double>> conditionsY;
    private List<double> times;

    public DataConditions()
    {
        conditionsX = new();
        conditionsY = new();
        times = new();
    }

    public static Dictionary<string, List<double>> ConvertToColumns(List<Dictionary<IVariable, double>> rows)
    {
        if (rows == null || rows.Count == 0)
        {
            return new();
        }

        int columnCount = rows.Max(row => row.Count);
        var columns = new Dictionary<string, List<double>>();

        foreach(var row in rows)
        {
            foreach((IVariable variable, double value) in row)
            {
                string name = variable.Name;

                if (columns.ContainsKey(name))
                {
                    columns[name].Add(value);
                } else
                {
                    columns[name] = new List<double>() { value };
                }
            }
        }

        return columns;
    }

    public Dictionary<string, List<double>> GetConvertedX()
    {
        return ConvertToColumns(conditionsX);
    }

    public Dictionary<string, List<double>> GetConvertedY()
    {
        return ConvertToColumns(conditionsY);
    }

    public List<double> GetTimes()
    {
        return times;
    }

    public void AddCondition(double time, Dictionary<IVariable, double> x, Dictionary<IVariable, double> y)
    {
        times.Add(time);
        conditionsX.Add(x);
        conditionsY.Add(y);
    }
}
