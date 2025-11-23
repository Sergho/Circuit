
namespace circuit;

public class AMatrix<R, C, E> : IMatrix<R, C, E>
    where R : notnull
    where C : notnull
    where E : notnull
{
    private HashSet<R> rows;
    private HashSet<C> cols;
    private Dictionary<(R, C), E> data;

    public AMatrix()
    {
        rows = new();
        cols = new();

        data = new();
    }

    public bool HasRow(R row)
    {
        return rows.Contains(row);
    }
    public bool HasCol(C col)
    {
        return cols.Contains(col);
    }
    public bool HasElem(R row, C col)
    {
        return data.ContainsKey((row, col));
    }

    public E GetElem(R row, C col)
    {
        if (!HasElem(row, col))
        {
            throw new Exception("Matrix element not found");
        }

        return data[(row, col)];
    }
    public void SetElem(R row, C col, E value)
    {
        if (!HasRow(row))
        {
            rows.Add(row);
        }

        if (!HasCol(col))
        {
            cols.Add(col);
        }

        data[(row, col)] = value;
    }

    public IEnumerable<E> GetRow(R row)
    {
        if (!HasRow(row))
        {
            throw new Exception("Matrix row not found");
        }

        foreach(C col in GetCols())
        {
            if (!HasElem(row, col)) continue;
            yield return data[(row, col)];
        }
    }
    public IEnumerable<E> GetCol(C col)
    {
        if (!HasCol(col))
        {
            throw new Exception("Matrix col not found");
        }

        foreach (R row in GetRows())
        {
            if (!HasElem(row, col)) continue;
            yield return data[(row, col)];
        }
    }

    public IEnumerable<R> GetRows()
    {
        return rows;
    }
    public virtual IEnumerable<C> GetCols()
    {
        return cols;
    }

    public int GetRowsCount()
    {
        return rows.Count;
    }
    public int GetColsCount()
    {
        return cols.Count;
    }

    public void DeleteRow(R row)
    {
        if (!HasRow(row))
        {
            throw new Exception("Matrix row not found");
        }

        rows.Remove(row);

        foreach(C col in GetCols())
        {
            if (!HasElem(row, col)) continue;
            DeleteElem(row, col);
        }
    }
    public void DeleteCol(C col)
    {
        if (!HasCol(col))
        {
            throw new Exception("Matrix col not found");
        }

        cols.Remove(col);

        foreach (R row in GetRows())
        {
            if (!HasElem(row, col)) continue;
            DeleteElem(row, col);
        }
    }
    public void DeleteElem(R row, C col)
    {
        if (!HasElem(row, col))
        {
            throw new Exception("Matrix element not found");
        }

        data.Remove((row, col));

        bool hasRowElems = false;
        foreach(C currentCol in GetCols())
        {
            if(HasElem(row, currentCol))
            {
                hasRowElems = true;
                break;
            }
        }

        bool hasColElems = false;
        foreach (R currentRow in GetRows())
        {
            if (HasElem(currentRow, col))
            {
                hasColElems = true;
                break;
            }
        }

        if (!hasColElems && HasCol(col))
        {
            DeleteCol(col);
        }

        if (!hasRowElems && HasRow(row))
        {
            DeleteRow(row);
        }
    }
}
