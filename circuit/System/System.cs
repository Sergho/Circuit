
namespace circuit;

public class System : ISystem
{
    private ISystemSolver solver;
    private INumberMatrix matrix;

    private Dictionary<int, int> xColsMap;
    private Dictionary<int, int> dxColsMap;
    private Dictionary<int, int> yColsMap;
    private Dictionary<int, int> vColsMap;

    private Dictionary<int, double> vValuesMap;

    public System(INumberMatrix matrix)
    {
        this.matrix = matrix;
        solver = new GausSystemSolver(matrix);

        dxColsMap = new();
        xColsMap = new();
        yColsMap = new();
        vColsMap = new();

        vValuesMap = new();
    }

    public void AddDXCol(int col, int order)
    {
        dxColsMap.Add(order, col);
    }
    public void AddXCol(int col, int order)
    {
        xColsMap.Add(order, col);
    }
    public void AddYCol(int col, int order)
    {
        yColsMap.Add(order, col);
    }
    public void AddVCol(int col, int order)
    {
        vColsMap.Add(order, col);
    }

    public void AddVValue(double value, int order)
    {
        vValuesMap.Add(order, value);
    }

    public IEnumerable<int> GetDXCols()
    {
        List<int> keys = dxColsMap.Keys.ToList();
        keys.Sort();

        return keys.Select(key => dxColsMap[key]);
    }
    public IEnumerable<int> GetXCols()
    {
        List<int> keys = xColsMap.Keys.ToList();
        keys.Sort();

        return keys.Select(key => xColsMap[key]);
    }
    public IEnumerable<int> GetYCols()
    {
        List<int> keys = yColsMap.Keys.ToList();
        keys.Sort();

        return keys.Select(key => yColsMap[key]);
    }
    public IEnumerable<int> GetVCols()
    {
        List<int> keys = vColsMap.Keys.ToList();
        keys.Sort();

        return keys.Select(key => vColsMap[key]);
    }

    public IEnumerable<double> GetVValues()
    {
        List<int> keys = vValuesMap.Keys.ToList();
        keys.Sort();

        return keys.Select(key => vValuesMap[key]);
    }

    public IEnumerable<int> GetRows()
    {
        return matrix.GetRows();
    }
    public IEnumerable<int> GetCols()
    {
        return matrix.GetCols();
    }

    public double Get(int row, int col)
    {
        return matrix.GetElem(row, col);
    }
    public void Set(int row, int col, double value)
    {
        matrix.SetElem(row, col, value);
    }

    public void Solve()
    {
        HashSet<int> cols = new(GetDXCols());
        cols.UnionWith(GetYCols());

        solver.Solve(cols);
    }
}
