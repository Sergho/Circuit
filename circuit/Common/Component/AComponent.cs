namespace circuit;

public abstract class AComponent : IComponent
{
    public IVariable Current { get; protected set; }
    public IVariable Voltage { get; protected set; }
    public IState? State { get; protected set; }
    public VariableType? StateType => State?.Type;
    public VariableType? ExternalType { get; private set; }

    public string Name { get; private set; }
    public double Value { get; private set; }

    public AComponent(string name, double value, VariableType? stateType = null, VariableType? externalType = null)
    {
        Name = name;
        Value = value;
        ExternalType = externalType;

        Current = new Variable(name, VariableType.Current, false, stateType == VariableType.Current, externalType == VariableType.Current ? value : null);
        Voltage = new Variable(name, VariableType.Voltage, false, stateType == VariableType.Voltage, externalType == VariableType.Voltage ? value : null);

        if (stateType == null) return;

        State = new State(name, (VariableType)stateType, externalType != null ? value : null);
    }

    public bool IsDisplacing()
    {
        return GetPriority() > 1;
    }
    public abstract int GetPriority();
    public bool IsExternal()
    {
        return ExternalType != null;
    }

    public abstract IEnumerable<ILinearEquation> Accept(IComponentRuleSetVisitor visitor);
}
