namespace circuit;

internal class EulerSolution : ISolution
{
    private ISystem system;
    private double step;

    private double time;
    private List<double> X;
    private List<double> Y;
    private List<double> V;

    private INumberMatrix A;
    private INumberMatrix B;
    private INumberMatrix C;
    private INumberMatrix D;

    public EulerSolution(ISystem system, double step, IEnumerable<double> start)
    {
        this.system = system;
        this.step = step;

        time = 0;
        V = new List<double>(system.GetVValues());
        X = new List<double>(start);
        Y = new List<double>();

        A = new NumberMatrix();
        B = new NumberMatrix();
        C = new NumberMatrix();
        D = new NumberMatrix();

        InitMatrices();
        CalcY();
    }

    public IEnumerable<double> GetX()
    {
        return new List<double>(X);
    }
    public IEnumerable<double> GetY()
    {
        return new List<double>(Y);
    }
    public double GetTime()
    {
        return time;
    }

    public void Next()
    {
        IEnumerable<double> first = Multiply(A, X);
        IEnumerable<double> second = Multiply(B, V);
        IEnumerable<double> sum = Add(first, second);
        IEnumerable<double> scaled = Scale(sum, step);

        X = new(Add(X, scaled));

        CalcY();
        time += step;
    }

    private void CalcY()
    {
        IEnumerable<double> first = Multiply(C, X);
        IEnumerable<double> second = Multiply(D, V);

        Y = new (Add(first, second));
    }

    private void InitMatrices()
    {
        HashSet<int> dRows = new();
        HashSet<int> yRows = new();

        foreach(int row in system.GetRows())
        {
            bool nulled = true;
            foreach(int col in system.GetDXCols())
            {
                if(system.Get(row, col) != 0)
                {
                    nulled = false;
                    break;
                }
            }

            if(nulled)
            {
                yRows.Add(row);
            } else
            {
                dRows.Add(row);
            }
        }

        FillMatrix(A, dRows, system.GetXCols());
        FillMatrix(B, dRows, system.GetVCols());
        FillMatrix(C, yRows, system.GetXCols());
        FillMatrix(D, yRows, system.GetVCols());
    }
    private void FillMatrix(INumberMatrix matrix, IEnumerable<int> rows, IEnumerable<int> cols)
    {
        int rowIndex = 0;
        foreach(int row in rows)
        {
            int colIndex = 0;
            foreach(int col in cols)
            {
                double value = system.Get(row, col);
                matrix.Set(rowIndex, colIndex, -value);

                colIndex++;
            }

            rowIndex++;
        }
    }

    private IEnumerable<double> Multiply(INumberMatrix matrix, IEnumerable<double> vec)
    {
        List<double> list = new(vec);

        if (matrix.GetColsCount() != list.Count)
        {
            throw new Exception("Incorrect sizes for multiplication");
        }

        List<double> result = new();

        foreach(int row in matrix.GetRows())
        {
            double sum = 0;
            for(int i = 0; i < list.Count; i++)
            {
                sum += matrix.Get(row, i) * list[i];
            }
            
            result.Add(sum);
        }

        return result;
    }
    private IEnumerable<double> Add(IEnumerable<double> first, IEnumerable<double> second)
    {
        List<double> firstList = new(first);
        List<double> secondList = new(second);

        if (firstList.Count == 0)
        {
            return secondList;
        }

        if (secondList.Count == 0)
        {
            return firstList;
        }

        if (firstList.Count != secondList.Count)
        {
            throw new Exception("Incorrect vector sizes for addition");
        }

        List<double> result = new();

        for (int i = 0; i < first.Count(); i++)
        {
            double sum = firstList[i] + secondList[i];
            result.Add(sum);
        }

        return result;
    }
    private IEnumerable<double> Scale(IEnumerable<double> vec, double factor)
    {
        List<double> list = new(vec);
        List<double> result = new();

        for (int i = 0; i < list.Count; i++)
        {
            result.Add(list[i] * factor);
        }

        return result;
    }
}
