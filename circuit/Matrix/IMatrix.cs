namespace circuit;

internal interface IMatrix
{
    bool HasRow(IEdge row);
    bool Has(IEdge row, IEdge col);
    MatrixCell Get(IEdge row, IEdge col);
    void Set(IEdge row, IEdge col, MatrixCell value);

    IEnumerable<IEdge> GetRows();
    IEnumerable<IEdge> GetCols();
}
