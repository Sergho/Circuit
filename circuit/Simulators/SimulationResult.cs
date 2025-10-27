using System;

namespace StateVariableMethod
{
    public class SimulationResult
    {
        public double[] Time { get; set; }
        public double[] UC { get; set; }
        public double[] IL { get; set; }
        public double[] I2 { get; set; }
        public double[] I3 { get; set; }

        public SimulationResult(int steps)
        {
            Time = new double[steps];
            UC = new double[steps];
            IL = new double[steps];
            I2 = new double[steps];
            I3 = new double[steps];
        }

        public void PrintResults()
        {
            Console.WriteLine("РЕЗУЛЬТАТЫ МОДЕЛИРОВАНИЯ:");
            Console.WriteLine("Время(с)\tu_C(В)\ti_L(А)\ti2(А)\ti3(А)");
            Console.WriteLine("---------------------------------------------------");

            for (int i = 0; i < Time.Length; i += Math.Max(1, Time.Length / 20))
            {
                Console.WriteLine($"{Time[i]:F3}\t{UC[i]:F4}\t{IL[i]:F4}\t{I2[i]:F4}\t{I3[i]:F4}");
            }
        }
    }
}