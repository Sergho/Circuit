using System;

namespace StateVariableMethod
{

    // Остальные классы остаются без изменений...
    public class CircuitModel
    {
        private double R1, R2, L, C, J;
        private double[] X0;

        private double[,] A;
        private double[,] B;
        private double[,] C_mat;
        private double[,] D;

        public CircuitModel(double r1, double r2, double l, double c, double j, double[] x0)
        {
            R1 = r1;
            R2 = r2;
            L = l;
            C = c;
            J = j;
            X0 = x0;

            InitializeMatrices();
        }

        private void InitializeMatrices()
        {
            // Матрица A (из документа)
            A = new double[2, 2]
            {
                { -1/(C*R2), -1/C },
                { 1/L,       -R1/L }
            };

            // Матрица B (из документа)
            B = new double[2, 1]
            {
                { 1/C },
                { 0   }
            };

            // Матрица C (из документа)
            C_mat = new double[2, 2]
            {
                { 1/R2,   0 },
                { -1/R2, -1 }
            };

            // Матрица D (из документа)
            D = new double[2, 1]
            {
                { 0 },
                { 1 }
            };
        }

        public SimulationResult Simulate(double t0, double tEnd, double h)
        {
            int steps = (int)((tEnd - t0) / h) + 1;
            SimulationResult result = new SimulationResult(steps);

            double[] X = { X0[0], X0[1] };
            double t = t0;

            for (int i = 0; i < steps; i++)
            {
                result.Time[i] = t;
                result.UC[i] = X[0];
                result.IL[i] = X[1];

                // Вычисляем выходные величины Y = C*X + D*V
                double i2 = C_mat[0, 0] * X[0] + C_mat[0, 1] * X[1] + D[0, 0] * J;
                double i3 = C_mat[1, 0] * X[0] + C_mat[1, 1] * X[1] + D[1, 0] * J;

                result.I2[i] = i2;
                result.I3[i] = i3;

                // Метод Эйлера: X_{n+1} = X_n + h*(A*X_n + B*V)
                double[] dX = MultiplyMatrixVector(A, X);
                dX[0] += B[0, 0] * J;
                dX[1] += B[1, 0] * J;

                X[0] += h * dX[0];
                X[1] += h * dX[1];

                t += h;
            }

            return result;
        }

        private double[] MultiplyMatrixVector(double[,] matrix, double[] vector)
        {
            double[] result = new double[vector.Length];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < vector.Length; j++)
                {
                    result[i] += matrix[i, j] * vector[j];
                }
            }
            return result;
        }
    }

}
