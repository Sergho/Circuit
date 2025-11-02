namespace DynamicSystemSolver.Core;

using DynamicSystemSolver.ComputationEngine;

public class ElectricalNetworkSolver
{
    private  double _paramA, _paramB, _paramC, _paramD, _paramE;
    private  double[] _initialConditions;
    private  double[,] _systemMatrix;
    private  double[,] _inputMatrix;
    private  double[,] _outputMatrix;
    private  double[,] _feedthroughMatrix;

    public ElectricalNetworkSolver(double resistance1, double resistance2,
        double inductance, double capacitance, double sourceCurrent, double[] initialState)
    {
        _paramA = resistance1;
        _paramB = resistance2;
        _paramC = inductance;
        _paramD = capacitance;
        _paramE = sourceCurrent;
        _initialConditions = (double[])initialState.Clone();

        SetupSystemMatrices();
    }

    private void SetupSystemMatrices()
    {
        double rc2 = _paramD * _paramB;

        _systemMatrix = new double[2, 2]
        {
            { -1.0/rc2, -1.0/_paramD },
            { 1.0/_paramC, -_paramA/_paramC }
        };

        _inputMatrix = new double[2, 1]
        {
            { 1.0/_paramD },
            { 0 }
        };

        _outputMatrix = new double[2, 2]
        {
            { 1.0/_paramB, 0 },
            { -1.0/_paramB, -1 }
        };

        _feedthroughMatrix = new double[2, 1]
        {
            { 0 },
            { 1 }
        };
    }

    private double[] MatrixVectorProduct(double[,] matrix, double[] vector)
    {
        double[] product = new double[vector.Length];
        for (int row = 0; row < matrix.GetLength(0); row++)
        {
            double sum = 0;
            for (int col = 0; col < vector.Length; col++)
            {
                sum += matrix[row, col] * vector[col];
            }
            product[row] = sum;
        }
        return product;
    }

    public ComputationResult ExecuteSimulation(double startTime, double endTime, double stepSize)
    {
        int iterationCount = (int)((endTime - startTime) / stepSize) + 1;
        ComputationResult output = new ComputationResult(iterationCount);

        double[] stateVector = { _initialConditions[0], _initialConditions[1] };
        double currentTime = startTime;

        for (int iteration = 0; iteration < iterationCount; iteration++)
        {
            output.TimePoints[iteration] = currentTime;
            output.State1[iteration] = stateVector[0];
            output.State2[iteration] = stateVector[1];

            double output1 = _outputMatrix[0, 0] * stateVector[0] +
                           _outputMatrix[0, 1] * stateVector[1] +
                           _feedthroughMatrix[0, 0] * _paramE;
            double output2 = _outputMatrix[1, 0] * stateVector[0] +
                           _outputMatrix[1, 1] * stateVector[1] +
                           _feedthroughMatrix[1, 0] * _paramE;

            output.Output1[iteration] = output1;
            output.Output2[iteration] = output2;

            double[] derivative = MatrixVectorProduct(_systemMatrix, stateVector);
            derivative[0] += _inputMatrix[0, 0] * _paramE;
            derivative[1] += _inputMatrix[1, 0] * _paramE;

            stateVector[0] += stepSize * derivative[0];
            stateVector[1] += stepSize * derivative[1];

            currentTime += stepSize;
        }

        return output;
    }
}