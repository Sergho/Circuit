namespace circuit;

internal class Matrix : IMatrix
{
    private Dictionary<IEdge, Dictionary<IEdge, MatrixCell>> data;

    public Matrix(Dictionary<IEdge, Dictionary<IEdge, MatrixCell>> data)
    {
        this.data = data;
    }

    public bool HasRow(IEdge row)
    {
        if (!data.ContainsKey(row)) { return false; }

        return true;
    }
    public bool Has(IEdge row, IEdge col)
    {
        if (!HasRow(row)) { return false; }
        if (!data[row].ContainsKey(col)) { return false; }

        return true;
    }
    public MatrixCell Get(IEdge row, IEdge col)
    {
        if(!Has(row, col))
        {
            throw new Exception("Matrix element not found");
        }

        return data[row][col];
    }
    public void Set(IEdge row, IEdge col, MatrixCell value)
    {
        if (!HasRow(row))
        {
            data.Add(row, new());
        }

        data[row].Add(col, value);
    }

    public IEnumerable<IEdge> GetRows()
    {
        return data.Keys;
    }
    public IEnumerable<IEdge> GetCols()
    {
        HashSet<IEdge> cols = new();
        foreach((IEdge row, var dict) in data)
        {
            foreach(IEdge col in dict.Keys)
            {
                cols.Add(col);
            }
        }

        return cols;
    }
}
