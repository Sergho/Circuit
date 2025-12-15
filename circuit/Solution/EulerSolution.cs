using SkiaSharp;

namespace circuit;

public class EulerSolution : ISolution
{
    private ISystemMatrix matrix;

    private double time;
    private Dictionary<IVariable, double> X;
    private Dictionary<IVariable, double> Y;
    private Dictionary<IVariable, double> V;
    private Dictionary<IVariable, double> startX;

    private ISolutionMatrix A;
    private ISolutionMatrix B;
    private ISolutionMatrix C;
    private ISolutionMatrix D;

    public EulerSolution(ISystemMatrix matrix, Dictionary<IVariable, double> startX)
    {
        this.matrix = matrix;
        this.startX = startX;

        X = new();
        Y = new();
        V = new();

        A = new SolutionMatrix();
        B = new SolutionMatrix();
        C = new SolutionMatrix();
        D = new SolutionMatrix();

        Init();
    }

    public Dictionary<IVariable, double> GetCurrentX()
    {
        return X;
    }
    public Dictionary<IVariable, double> GetCurrentY()
    {
        return Y;
    }
    public double GetCurrentTime()
    {
        return time;
    }

    public void Next(double step)
    {
        Dictionary<IVariable, double> first = Multiply(A, X);
        Dictionary<IVariable, double> second = Multiply(B, V);
        Dictionary<IVariable, double> sum = Add(first, second);
        Dictionary<IVariable, double> scaled = Scale(sum, step);

        X = Add(X, scaled);

        CalcY();

        time += step;
    }
    public void Init()
    {
        time = 0;
        X = new(startX);

        InitMatrices();
        CalcV();
        CalcY();
    }

    private void CalcY()
    {
        Dictionary<IVariable, double> first = Multiply(C, X);
        Dictionary<IVariable, double> second = Multiply(D, V);

        Y = Add(first, second);
    }

    private void CalcV()
    {
        V = new();
        foreach(IVariable col in matrix.GetCols())
        {
            if (col.ExternalValue == null) continue;
            V.Add(col, (double)col.ExternalValue);
        }
    }

    private void InitMatrices()
    {
        Dictionary<IVariable, int> xRows = new();
        Dictionary<IVariable, int> yRows = new();

        foreach (IVariable col in matrix.GetCols())
        {
            if (col.ExternalValue != null)
            {
                foreach ((IVariable rowVar, int row) in xRows)
                {
                    double value = matrix.GetElem(row, col);
                    B.SetElem(rowVar, col, -value);
                }

                foreach ((IVariable rowVar, int row) in yRows)
                {
                    double value = matrix.GetElem(row, col);
                    D.SetElem(rowVar, col, -value);
                }

                continue;
            }
            if (col.IsStated && !col.IsDerivative)
            {
                foreach ((IVariable rowVar, int row) in xRows)
                {
                    double value = matrix.GetElem(row, col);
                    A.SetElem(rowVar, col, -value);
                }

                foreach ((IVariable rowVar, int row) in yRows)
                {
                    double value = matrix.GetElem(row, col);
                    C.SetElem(rowVar, col, -value);
                }

                continue;
            }
            if (!col.IsStated)
            {
                int rowIndex = 0;
                foreach (int row in matrix.GetRows())
                {
                    if (matrix.GetElem(row, col) != 0)
                    {
                        rowIndex = row;
                        break;
                    }
                }

                yRows.Add(col, rowIndex);
                continue;
            }
            if (col.IsStated && col.IsDerivative)
            {
                int rowIndex = 0;
                foreach (int row in matrix.GetRows())
                {
                    if (matrix.GetElem(row, col) != 0)
                    {
                        rowIndex = row;
                        break;
                    }
                }

                IVariable xCol = new Variable(col.BaseName, col.Type, false, col.IsStated, col.ExternalValue);
                xRows.Add(xCol, rowIndex);
                continue;
            }
        }
    }

    private Dictionary<IVariable, double> Multiply(ISolutionMatrix matrix, Dictionary<IVariable, double> vec)
    {
        if (matrix.GetColsCount() != vec.Count)
        {
            throw new Exception("Incorrect sizes for multiplication");
        }

        Dictionary<IVariable, double> result = new();

        foreach (IVariable row in matrix.GetRows())
        {
            double sum = 0;
            foreach (IVariable col in matrix.GetCols())
            {
                if(!vec.ContainsKey(col))
                {
                    throw new Exception("Incorrect matrix and vector for multiplication");
                }

                sum += matrix.GetElem(row, col) * vec[col];
            }

            result.Add(row, sum);
        }

        return result;
    }
    private Dictionary<IVariable, double> Add(Dictionary<IVariable, double> first, Dictionary<IVariable, double> second)
    {
        if (first.Count == 0)
        {
            return second;
        }

        if (second.Count == 0)
        {
            return first;
        }

        Dictionary<IVariable, double> result = new();

        foreach (IVariable variable in first.Keys)
        {
            if(!second.ContainsKey(variable))
            {
                throw new Exception("Incorrect vectors for addition");
            }

            result.Add(variable, first[variable] + second[variable]);
        }

        return result;
    }
    private Dictionary<IVariable, double> Scale(Dictionary<IVariable, double> vec, double factor)
    {
        Dictionary<IVariable, double> result = new();

        foreach (IVariable variable in vec.Keys)
        {
            result.Add(variable, factor * vec[variable]);
        }

        return result;
    }
}
