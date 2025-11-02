using DynamicSystemSolver.Core;
using DynamicSystemSolver.ComputationEngine;


class Program
{
    static void Main()
    {
        Console.WriteLine("Формат: R1 R2 L C J t0 t_end h");
        Console.Write("> ");

        string input = Console.ReadLine();
        string[] parameters = input.Split(' ');

        if (parameters.Length != 8)
        {
            Console.WriteLine("Ошибка: необходимо ввести 8 параметров через пробел");
            return;
        }

        double R1 = double.Parse(parameters[0]);
        double R2 = double.Parse(parameters[1]);
        double L = double.Parse(parameters[2]);
        double C = double.Parse(parameters[3]);
        double J = double.Parse(parameters[4]);
        double t0 = double.Parse(parameters[5]);
        double tEnd = double.Parse(parameters[6]);
        double h = double.Parse(parameters[7]);

        double[] X0 = { J * R2, 0 };

        ElectricalNetworkSolver model = new ElectricalNetworkSolver(R1, R2, L, C, J, X0);
        ComputationResult result = model.ExecuteSimulation(t0, tEnd, h);

        result.DisplaySummary();
    }
}
