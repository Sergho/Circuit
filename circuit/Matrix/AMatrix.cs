
namespace circuit;

public class AMatrix<R, C, E> : IMatrix<R, C, E>
    where R : notnull
    where C : notnull
    where E : notnull
{
    private Dictionary<R, Dictionary<C, E>> data;

    public AMatrix()
    {
        data = new();
    }

    public bool HasRow(R row)
    {
        return data.ContainsKey(row);
    }
    public bool HasCol(C col)
    {
        foreach(var row in data.Values)
        {
            if (row.ContainsKey(col)) return true;
        }

        return false;
    }
    public bool HasElem(R row, C col)
    {
        if (!HasRow(row)) { return false; }
        if (!data[row].ContainsKey(col)) { return false; }

        return true;
    }

    public E Get(R row, C col)
    {
        if (!HasElem(row, col))
        {
            throw new Exception("Matrix element not found");
        }

        return data[row][col];
    }
    public void Set(R row, C col, E value)
    {
        if (!HasRow(row))
        {
            data.Add(row, new());
        }

        if(HasElem(row, col))
        {
            data[row][col] = value;
        } else
        {
            data[row].Add(col, value);
        }
    }

    public IEnumerable<E> GetRow(R row)
    {
        if (!HasRow(row))
        {
            throw new Exception("Matrix row not found");
        }

        return data[row].Values;
    }
    public IEnumerable<E> GetCol(C col)
    {
        List<E> list = new();
        foreach(var row in data.Values)
        {
            if(row.ContainsKey(col))
            {
                list.Add(row[col]);
            }
        }

        return list;
    }
    
    public void DeleteRow(R row)
    {
        if (!HasRow(row))
        {
            throw new Exception("Matrix row not found");
        }

        data.Remove(row);
    }

    public IEnumerable<R> GetRows()
    {
        return data.Keys;
    }
    public IEnumerable<C> GetCols()
    {
        HashSet<C> cols = new();
        foreach (var dict in data.Values)
        {
            foreach (C col in dict.Keys)
            {
                cols.Add(col);
            }
        }

        return cols;
    }

    public int GetRowsCount()
    {
        return data.Keys.Count;
    }
    public int GetColsCount()
    {
        HashSet<C> cols = new();
        foreach (var dict in data.Values)
        {
            foreach (C col in dict.Keys)
            {
                cols.Add(col);
            }
        }

        return cols.Count;
    }
}
