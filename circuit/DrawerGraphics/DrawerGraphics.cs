using ScottPlot;

namespace circuit;

public class DrawerGraphics
{
    public DataConditions Conditions { get; private set; }

    public DrawerGraphics(DataConditions conditions)
    {
        Conditions = conditions;
    }

    public void GenGraphics()
    {
        var time = Conditions.GetTimes();

        var X = Conditions.GetConvertedX();
        for (int i = 0; i < X.Count; i++)
        {
            string condName = $"X{i}";
            SaveGraphic(condName, time, X[i]);
        }

        var Y = Conditions.GetConvertedY();
        for (int i = 0; i < Y.Count; i++)
        {
            string condName = $"Y{i}";
            SaveGraphic(condName, time, Y[i]);
        }
    }

    private void SaveGraphic(
        string conditionName,
        List<double> x,
        List<double> y)
    {
        var plot = new Plot();

        plot.Add.Scatter(x, y);

        plot.Title(conditionName);
        plot.XLabel("time(sec)");
        plot.YLabel($"{conditionName}(unit measur)");
        plot.ShowLegend();

        Directory.CreateDirectory("graphics");

        plot.SavePng($"graphics/{conditionName}.png", 800, 600);
    }
}
