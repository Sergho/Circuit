namespace DynamicSystemSolver.ComputationEngine;

public class ComputationResult
{
    public double[] TimePoints { get; private set; }
    public double[] State1 { get; private set; }
    public double[] State2 { get; private set; }
    public double[] Output1 { get; private set; }
    public double[] Output2 { get; private set; }

    public ComputationResult(int dataPoints)
    {
        TimePoints = new double[dataPoints];
        State1 = new double[dataPoints];
        State2 = new double[dataPoints];
        Output1 = new double[dataPoints];
        Output2 = new double[dataPoints];
    }

    public void DisplaySummary()
    {
        Console.WriteLine("РЕЗУЛЬТАТЫ ВЫЧИСЛЕНИЙ:");
        Console.WriteLine("Время(с) U_c I_l i2 i3");
        Console.WriteLine(new string('-', 60));

        int displayInterval = Math.Max(1, TimePoints.Length / 15);

        for (int idx = 0; idx < TimePoints.Length; idx += displayInterval)
        {
            Console.WriteLine($"{TimePoints[idx]:F3}\t" +
                            $"{State1[idx]:F4}\t" +
                            $"{State2[idx]:F4}\t" +
                            $"{Output1[idx]:F4}\t" +
                            $"{Output2[idx]:F4}");
        }
    }
}













































    // public void SaveToFile(string filePath)
    // {
    //     using var writer = new StreamWriter(filePath);
    //     writer.WriteLine("Time,State1,State2,Output1,Output2");
        
    //     for (int idx = 0; idx < TimePoints.Length; idx++)
    //     {
    //         writer.WriteLine($"{TimePoints[idx]:F6},{State1[idx]:F6},{State2[idx]:F6}," +
    //                        $"{Output1[idx]:F6},{Output2[idx]:F6}");
    //     }
    // }