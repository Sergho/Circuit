
namespace circuit;

public class System : ISystem
{
    private ISystemSolver solver;
    private ISystemMatrix matrix;

    private HashSet<int> xCols;
    private HashSet<int> dxCols;
    private HashSet<int> yCols;
    private HashSet<int> vCols;

    public System(ISystemMatrix matrix)
    {
        this.matrix = matrix;
        solver = new GausSystemSolver(matrix);

        dxCols = new();
        xCols = new();
        yCols = new();
        vCols = new();
    }

    public void AddDXCol(int col)
    {
        dxCols.Add(col);
    }
    public void AddXCol(int col)
    {
        xCols.Add(col);
    }
    public void AddYCol(int col)
    {
        yCols.Add(col);
    }
    public void AddVCol(int col)
    {
        vCols.Add(col);
    }

    public double Get(int row, int col)
    {
        return matrix.Get(row, col);
    }
    public void Set(int row, int col, double value)
    {
        matrix.Set(row, col, value);
    }

    public void Solve()
    {
        HashSet<int> cols = new(dxCols);
        cols.UnionWith(yCols);

        solver.Solve(cols);
    }
}
