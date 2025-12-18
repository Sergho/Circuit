using ScottPlot;

namespace circuit;

public class DrawerGraphics
{
    private readonly string dirName = "graphics";
    public DataConditions Conditions { get; private set; }

    public DrawerGraphics(DataConditions conditions)
    {
        Conditions = conditions;
    }

    public void GenGraphics()
    {
        if (Directory.Exists(dirName))
        {
            Directory.Delete(dirName, true);
        }

        var time = Conditions.GetTimes();

        var X = Conditions.GetConvertedX();
        var Y = Conditions.GetConvertedY();

        foreach ((string name, var values) in X)
        {
            SaveGraphic(name, time, values);
        }

        foreach ((string name, var values) in Y)
        {
            SaveGraphic(name, time, values);
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

        Directory.CreateDirectory(dirName);

        plot.SavePng($"{dirName}/{conditionName}.png", 800, 600);
    }
}
