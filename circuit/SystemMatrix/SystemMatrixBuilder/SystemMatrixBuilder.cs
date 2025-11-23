namespace circuit;

public class SystemMatrixBuilder : ISystemMatrixBuilder
{

    public SystemMatrixBuilder()
    {
    }

    public ISystemMatrix Build(IComponentMatrix componentMatrix)
    {
        ISystemMatrix matrix = new SystemMatrix();

        foreach(IComponent row in componentMatrix.GetRows())
        {
            Traverse(componentMatrix, matrix, row, true);
        }

        foreach (IComponent col in componentMatrix.GetCols())
        {
            Traverse(componentMatrix, matrix, col, false);
        }

        AddRuleSets(componentMatrix, matrix);

        return matrix;
    }

    private int CreateRow(IComponentMatrix componentMatrix, ISystemMatrix systemMatrix)
    {
        int rowIndex = systemMatrix.GetRowsCount();
        List<IComponent> components = new List<IComponent>(componentMatrix.GetRows());
        components.AddRange(componentMatrix.GetCols());

        foreach(IComponent component in components)
        {
            systemMatrix.SetElem(rowIndex, component.Current, 0);
            systemMatrix.SetElem(rowIndex, component.Voltage, 0);

            if (component.State == null) continue;

            systemMatrix.SetElem(rowIndex, component.State.Variable, 0);
            systemMatrix.SetElem(rowIndex, component.State.DVariable, 0);
        }

        return rowIndex;
    }

    private void Traverse(IComponentMatrix componentMatrix, ISystemMatrix systemMatrix, IComponent component, bool rowTraverse)
    {
        int rowIndex = CreateRow(componentMatrix, systemMatrix);
        systemMatrix.SetElem(rowIndex, rowTraverse ? component.Voltage : component.Current, -1);

        IEnumerable<IComponent> traverse = rowTraverse ? componentMatrix.GetCols() : componentMatrix.GetRows();

        foreach(IComponent currentComponent in traverse)
        {
            IComponent row = rowTraverse ? component : currentComponent;
            IComponent col = rowTraverse ? currentComponent : component;
            IVariable systemCol = rowTraverse ? currentComponent.Voltage : currentComponent.Current;
            double value = systemMatrix.GetElem(rowIndex, systemCol) + (int)componentMatrix.GetElem(row, col);

            systemMatrix.SetElem(rowIndex, systemCol, value);
        }
    }

    private void AddRuleSets(IComponentMatrix componentMatrix, ISystemMatrix systemMatrix)
    {
        List<IComponent> components = new List<IComponent>(componentMatrix.GetRows());
        components.AddRange(componentMatrix.GetCols());
        IComponentRuleSetVisitor ruleSetVisitor = new OhmComponentRuleSet();

        foreach(IComponent component in components)
        {
            foreach(ILinearEquation rule in component.Accept(ruleSetVisitor))
            {
                int rowIndex = CreateRow(componentMatrix, systemMatrix);
                
                foreach(IVariable variable in rule.GetVariables())
                {
                    systemMatrix.SetElem(rowIndex, variable, rule.GetCoefficient(variable));
                }
            }
        }
    }

    //private void TrimMatrix(INumberMatrix matrix)
    //{
    //    HashSet<int> emptyRows = new();

    //    foreach (int row in matrix.GetRows())
    //    {
    //        bool empty = true;
    //        foreach (int col in matrix.GetCols())
    //        {
    //            if (matrix.GetElem(row, col) != 0)
    //            {
    //                empty = false;
    //                break;
    //            }
    //        }

    //        if (empty)
    //        {
    //            emptyRows.Add(row);
    //        }
    //    }

    //    foreach (int row in emptyRows)
    //    {
    //        matrix.DeleteRow(row);
    //    }
    //}
    //private void SaturateMatrix(INumberMatrix matrix)
    //{
    //    Dictionary<int, IEdge> voltageStated = new();
    //    foreach ((IEdge edge, var pair) in colTraverseMap)
    //    {
    //        if (edge.Component.State?.Type != StateType.Voltage) continue;
    //        voltageStated.Add(pair.Item1, edge);
    //    }

    //    INumberMatrix saturation = new NumberMatrix();
    //    foreach (int rowIndex in matrix.GetRows())
    //    {
    //        foreach (int colIndex in voltageStated.Keys)
    //        {
    //            double value = matrix.GetElem(rowIndex, colIndex);
    //            if (value == 0) continue;

    //            CopyRow(matrix, saturation, rowIndex);

    //            ICurrent current = voltageStated[colIndex].Current;
    //            matrix.SetElem(rowIndex, colIndex, 0);
    //            matrix.SetElem(rowIndex, currentIndex[current], value / Math.Abs(value));
    //        }
    //    }

    //    for (int i = 0; i < saturation.GetRowsCount(); i++)
    //    {
    //        CopyRow(saturation, matrix, i);
    //    }
    //}
    //private void StackMatrix(INumberMatrix matrix)
    //{
    //    List<int> rows = new List<int>(matrix.GetRows());
    //    for (int i = 0; i < rows.Count; i++)
    //    {
    //        if (i == rows[i]) continue;

    //        foreach (int col in matrix.GetCols())
    //        {
    //            matrix.SetElem(i, col, matrix.GetElem(rows[i], col));
    //        }

    //        matrix.DeleteRow(rows[i]);
    //    }
    //}
    //private void CopyRow(INumberMatrix from, INumberMatrix to, int rowIndex)
    //{
    //    int newRowIndex = to.GetRowsCount();
    //    foreach (int colIndex in from.GetCols())
    //    {
    //        double value = from.GetElem(rowIndex, colIndex);
    //        to.SetElem(newRowIndex, colIndex, value);
    //    }
    //}
}
