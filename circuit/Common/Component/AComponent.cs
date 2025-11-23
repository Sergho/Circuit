namespace circuit;

public abstract class AComponent : IComponent
{
    public IVariable Current { get; protected set; }
    public IVariable Voltage { get; protected set; }
    public IState? State { get; protected set; }
    public VariableType? StateType => State?.Type;

    public string Name { get; private set; }
    public double Value { get; private set; }

    public AComponent(string name, double value, VariableType? stateType = null, bool isExternal = false)
    {
        Name = name;
        Value = value;
        
        double? externalValue = isExternal ? value : null;

        Current = new Variable(name, VariableType.Current, false, stateType == VariableType.Current, externalValue);
        Voltage = new Variable(name, VariableType.Voltage, false, stateType == VariableType.Voltage, externalValue);

        if (stateType == null) return;

        State = new State(name, (VariableType)stateType, externalValue);
    }
    public bool IsDisplacing()
    {
        return GetPriority() > 1;
    }
    public abstract int GetPriority();
    public abstract bool IsExternal();

    public abstract IEnumerable<ILinearEquation> Accept(IComponentRuleSetVisitor visitor);
}
