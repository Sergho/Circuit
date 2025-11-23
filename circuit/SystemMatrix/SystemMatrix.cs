
namespace circuit;

public class SystemMatrix : AMatrix<int, IVariable, double>
{
    public SystemMatrix()
    {
    }

    public override IEnumerable<IVariable> GetCols()
    {
        List<IVariable> cols = base.GetCols().ToList();

        return cols.OrderByDescending(col =>
        {
            if (col.IsExternal) return 1;
            if (col.IsStated && !col.IsDerivative) return 2;
            if (!col.IsStated) return 3;
            if (col.IsStated && col.IsDerivative) return 4;

            return 0;
        });
    }
}
