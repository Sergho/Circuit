namespace circuit;

public class SystemBuilder : ISystemBuilder
{
    private ISchema schema;
    private IEdgeMatrix edgeMatrix;
    private Dictionary<ICurrent, int> currentIndex;
    private Dictionary<IEdge, (int, double)> rowTraverseMap;
    private Dictionary<IEdge, (int, double)> colTraverseMap;
    private int rowSize;

    public SystemBuilder(ISchema schema)
    {
        this.schema = schema;
        edgeMatrix = schema.GetEdgeMatrix();

        currentIndex = new();
        rowTraverseMap = new();
        colTraverseMap = new();

        rowSize = 0;
    }

    public void Init()
    {
        int index = 0;
        foreach (IEdge edge in schema.GetEdges())
        {
            bool isDuplicate = currentIndex.ContainsKey(edge.Current);
            int edgeIndex = isDuplicate ? currentIndex[edge.Current] : index;

            switch (edge.Component.GetStateType())
            {
                case StateType.Voltage:
                    if(!isDuplicate) index++;
                    colTraverseMap.Add(edge, (index++, edge.Component.Value));
                    rowTraverseMap.Add(edge, (index++, 1));
                    break;
                case StateType.Current:
                    colTraverseMap.Add(edge, (edgeIndex, 1));
                    if (!isDuplicate) index++;
                    rowTraverseMap.Add(edge, (index++, edge.Component.Value));
                    break;
                case StateType.None:
                    colTraverseMap.Add(edge, (edgeIndex, 1));
                    if (!isDuplicate) index++;
                    if (edge.Component.IsExternal()) break;
                    rowTraverseMap.Add(edge, (edgeIndex, edge.Component.Value));
                    break;
            }
 
            if (!isDuplicate)
            {
                currentIndex.Add(edge.Current, edgeIndex);
            }
        }

        rowSize = index;
    }
    public ISystem GetSystem()
    {
        ISystem system = new System(GetMatrix());
        
        HashSet<ICurrent> visited = new();

        foreach((IEdge edge, var pair) in colTraverseMap)
        {
            int colIndex = pair.Item1;
            
            if(edge.Component.IsExternal())
            {
                system.AddVCol(colIndex, edge.Id);
                system.AddVValue(edge.Component.Value, edge.Id);
                visited.Add(edge.Current);
            }

            switch (edge.Component.GetStateType())
            {
                case StateType.Voltage:
                    system.AddDXCol(colIndex, edge.Id);
                    break;
                case StateType.Current:
                    system.AddXCol(colIndex, edge.Id);
                    visited.Add(edge.Current);
                    break;
            }
        }

        foreach ((IEdge edge, var pair) in rowTraverseMap)
        {
            int colIndex = pair.Item1;

            if (edge.Component.IsExternal())
            {
                system.AddVCol(colIndex, edge.Id);
                system.AddVValue(edge.Component.Value, edge.Id);
                visited.Add(edge.Current);
            }

            switch (edge.Component.GetStateType())
            {
                case StateType.Voltage:
                    system.AddXCol(colIndex, edge.Id);
                    break;
                case StateType.Current:
                    system.AddDXCol(colIndex, edge.Id);
                    break;
            }
        }

        foreach ((ICurrent current, int colIndex) in currentIndex)
        {
            if (visited.Contains(current)) continue;
            system.AddYCol(colIndex, current.Id);
        }

        return system;
    }

    private INumberMatrix GetMatrix()
    {
        INumberMatrix matrix = GetRawMatrix();
        SaturateMatrix(matrix);
        TrimMatrix(matrix);
        StackMatrix(matrix);

        return matrix;
    }
    private INumberMatrix GetRawMatrix()
    {
        INumberMatrix matrix = new NumberMatrix();
        int rowIndex = 0;

        foreach (IEdge row in edgeMatrix.GetRows())
        {
            var systemRow = Traverse(row, true);
            if (systemRow.Count == 0) continue;

            foreach ((int colIndex, double value) in systemRow)
            {
                matrix.SetElem(rowIndex, colIndex, value);
            }

            rowIndex++;
        }

        foreach (IEdge col in edgeMatrix.GetCols())
        {
            var systemRow = Traverse(col, false);
            if (systemRow.Count == 0) continue;

            foreach ((int colIndex, double value) in systemRow)
            {
                matrix.SetElem(rowIndex, colIndex, value);
            }

            rowIndex++;
        }

        return matrix;
    }
    private Dictionary<int, double> Traverse(IEdge edge, bool rowTraverse)
    {
        Dictionary<int, double> systemRow = new();

        var edgePart = TraverseItem(edge, rowTraverse);
        if (edgePart == null) return new();

        int multiplier = 1;
        if (rowTraverse) multiplier = -1;

        systemRow.Add(edgePart.Value.Item1, multiplier * edgePart.Value.Item2);

        var items = rowTraverse ? edgeMatrix.GetCols() : edgeMatrix.GetRows();
        foreach (IEdge item in items)
        {
            IEdge edgeMatrixRow = rowTraverse ? edge : item;
            IEdge edgeMatrixCol = rowTraverse ? item : edge;
            MatrixCell cell = edgeMatrix.GetElem(edgeMatrixRow, edgeMatrixCol);

            var itemPart = TraverseItem(item, rowTraverse, cell);
            if (itemPart == null) return new();

            if(systemRow.ContainsKey(itemPart.Value.Item1))
            {
                systemRow[itemPart.Value.Item1] += itemPart.Value.Item2;
            } else
            {
                systemRow.Add(itemPart.Value.Item1, itemPart.Value.Item2);
            }
        }

        for (int colIndex = 0; colIndex < rowSize; colIndex++)
        {
            if (systemRow.ContainsKey(colIndex)) continue;
            systemRow.Add(colIndex, 0);
        }

        return systemRow;
    }
    private (int, double)? TraverseItem(IEdge item, bool rowTraverse, MatrixCell? cell = null)
    {
        var traverseMap = rowTraverse ? rowTraverseMap : colTraverseMap;

        if (rowTraverse && item.Component.IsExternal()) return null;
        if (!traverseMap.ContainsKey(item)) return null;

        (int colIndex, double value) = traverseMap[item];

        double multiplier = -1;
        if (cell != null)
        {
            multiplier = (double)cell;
        }

        double multiplied = value * multiplier;
        return (colIndex, multiplied);
    }
    private void TrimMatrix(INumberMatrix matrix)
    {
        HashSet<int> emptyRows = new();

        foreach (int row in matrix.GetRows())
        {
            bool empty = true;
            foreach (int col in matrix.GetCols())
            {
                if (matrix.GetElem(row, col) != 0)
                {
                    empty = false;
                    break;
                }
            }

            if(empty)
            {
                emptyRows.Add(row);
            }
        }

        foreach(int row in emptyRows)
        {
            matrix.DeleteRow(row);
        }
    }
    private void SaturateMatrix(INumberMatrix matrix)
    {
        Dictionary<int, IEdge> voltageStated = new();
        foreach((IEdge edge, var pair) in colTraverseMap)
        {
            if (edge.Component.GetStateType() != StateType.Voltage) continue;
            voltageStated.Add(pair.Item1, edge);
        }

        INumberMatrix saturation = new NumberMatrix();
        foreach(int rowIndex in matrix.GetRows())
        {
            foreach(int colIndex in voltageStated.Keys)
            {
                double value = matrix.GetElem(rowIndex, colIndex);
                if (value == 0) continue;

                CopyRow(matrix, saturation, rowIndex);

                ICurrent current = voltageStated[colIndex].Current;
                matrix.SetElem(rowIndex, colIndex, 0);
                matrix.SetElem(rowIndex, currentIndex[current], value / Math.Abs(value));
            }
        }

        for(int i = 0; i < saturation.GetRowsCount(); i++)
        {
            CopyRow(saturation, matrix, i);
        }
    }
    private void StackMatrix(INumberMatrix matrix)
    {
        List<int> rows = new List<int>(matrix.GetRows());
        for(int i = 0; i < rows.Count; i++)
        {
            if (i == rows[i]) continue;

            foreach(int col in matrix.GetCols())
            {
                matrix.SetElem(i, col, matrix.GetElem(rows[i], col));
            }

            matrix.DeleteRow(rows[i]);
        }
    }
    private void CopyRow(INumberMatrix from, INumberMatrix to, int rowIndex)
    {
        int newRowIndex = to.GetRowsCount();
        foreach(int colIndex in from.GetCols())
        {
            double value = from.GetElem(rowIndex, colIndex);
            to.SetElem(newRowIndex, colIndex, value);
        }
    }
}
