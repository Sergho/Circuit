ing System;

namespace StateVariablesMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите параметры схемы:");

            Console.Write("Сопротивление R1 (Ом): ");
            double R1 = double.Parse(Console.ReadLine());

            Console.Write("Сопротивление R2 (Ом): ");
            double R2 = double.Parse(Console.ReadLine());

            Console.Write("Индуктивность L (Гн): ");
            double L = double.Parse(Console.ReadLine());

            Console.Write("Емкость C (Ф): ");
            double C = double.Parse(Console.ReadLine());

            Console.Write("Ток источника J (А): ");
            double J = double.Parse(Console.ReadLine());

            double[] X0 = { J * R2, 0 };

            Console.Write("\nНачальное время t0 (с): ");
            double t0 = double.Parse(Console.ReadLine());

            Console.Write("Конечное время t_end (с): ");
            double tEnd = double.Parse(Console.ReadLine());

            Console.Write("Шаг времени h (с): ");
            double h = double.Parse(Console.ReadLine());

            CircuitModel model = new CircuitModel(R1, R2, L, C, J, X0);
            SimulationResult result = model.Simulate(t0, tEnd, h);

            result.PrintResults();

            Console.WriteLine("\nМоделирование завершено!");
        }
    }
}