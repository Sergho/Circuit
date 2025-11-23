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
        double multiplier = rowTraverse ? 1 : -1;
        systemMatrix.SetElem(rowIndex, rowTraverse ? component.Voltage : component.Current, multiplier);

        IEnumerable<IComponent> traverse = rowTraverse ? componentMatrix.GetCols() : componentMatrix.GetRows();

        foreach(IComponent currentComponent in traverse)
        {
            IComponent row = rowTraverse ? component : currentComponent;
            IComponent col = rowTraverse ? currentComponent : component;
            IVariable systemCol = rowTraverse ? currentComponent.Voltage : currentComponent.Current;
            double value = systemMatrix.GetElem(rowIndex, systemCol) + (double)componentMatrix.GetElem(row, col);

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
}
