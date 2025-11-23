namespace circuit;

public abstract class AComponent : IComponent
{
    public IVariable Current { get; private set; }
    public IVariable Voltage { get; private set; }
    public IState? State { get; private set; }
    public VariableType? StateType => State?.Type;

    public string Name { get; private set; }
    public double Value { get; private set; }

    public AComponent(string name, double value, VariableType? stateType = null)
    {
        Name = name;
        Value = value;

        Current = new Variable(name, VariableType.Current);
        Voltage = new Variable(name, VariableType.Voltage);

        if (stateType == null) return;

        State = new State(name, (VariableType)stateType);
    }
    public bool IsDisplacing()
    {
        return GetPriority() > 1;
    }
    public abstract int GetPriority();
    public abstract bool IsExternal();

    public abstract IEnumerable<ILinearEquation> Accept(IComponentRuleSetVisitor visitor);
}
