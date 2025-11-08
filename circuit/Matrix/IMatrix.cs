namespace circuit;

public interface IMatrix<R, C, E>
{
    bool HasRow(R row);
    bool HasCol(C col);
    bool HasElem(R row, C col);

    E Get(R row, C col);
    void Set(R row, C col, E value);

    IEnumerable<E> GetRow(R row);
    IEnumerable<E> GetCol(C col);

    void DeleteRow(R row);

    IEnumerable<R> GetRows();
    IEnumerable<C> GetCols();

    int GetRowsCount();
    int GetColsCount();
}
