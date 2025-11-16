namespace circuit;

public interface IMatrix<R, C, E>
{
    bool HasRow(R row);
    bool HasCol(C col);
    bool HasElem(R row, C col);

    E GetElem(R row, C col);
    void SetElem(R row, C col, E value);

    IEnumerable<E> GetRow(R row);
    IEnumerable<E> GetCol(C col);

    IEnumerable<R> GetRows();
    IEnumerable<C> GetCols();

    int GetRowsCount();
    int GetColsCount();

    void DeleteRow(R row);
    void DeleteCol(C col);
    void DeleteElem(R row, C col);
}
