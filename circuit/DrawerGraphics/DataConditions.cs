namespace circuit;

public class DataConditions
{
    private List<List<double>> conditionsX;
    private List<List<double>> conditionsY;
    private List<double> times;

    public DataConditions()
    {
        conditionsX = new();
        conditionsY = new();
        times = new();
    }

    public static List<List<double>> ConvertToColumns(List<List<double>> rows)
    {
        if (rows == null || rows.Count == 0)
            return new List<List<double>>();

        int columnCount = rows.Max(row => row.Count);
        var columns = new List<List<double>>(columnCount);

        for (int i = 0; i < columnCount; i++)
        {
            columns.Add(new List<double>());
        }

        foreach (var row in rows)
        {
            for (int col = 0; col < columnCount; col++)
            {
                if (col < row.Count)
                    columns[col].Add(row[col]);
            }
        }

        return columns;
    }

    public List<List<double>> GetConvertedX()
    {
        return ConvertToColumns(conditionsX);
    }

    public List<List<double>> GetConvertedY()
    {
        return ConvertToColumns(conditionsY);
    }

    public List<double> GetTimes()
    {
        return times;
    }

    public void AddCondition(double time, List<double> xValues, List<double> yValues)
    {
        times.Add(time);
        conditionsX.Add(xValues);
        conditionsY.Add(yValues);
    }
}
