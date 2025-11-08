
namespace circuit;

public class System : ISystem
{
    private ISystemMatrix matrix;
    private HashSet<int> xCols;
    private HashSet<int> yCols;
    private HashSet<int> vCols;

    public System(ISystemMatrix matrix)
    {
        this.matrix = matrix;

        xCols = new();
        yCols = new();
        vCols = new();
    }

    public void SetX(IEnumerable<int> cols)
    {
        xCols = new(cols);
    }
    public void SetY(IEnumerable<int> cols)
    {
        yCols = new(cols);
    }
    public void SetV(IEnumerable<int> cols)
    {
        vCols = new(cols);
    }
}
