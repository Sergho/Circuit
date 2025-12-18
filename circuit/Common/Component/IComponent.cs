namespace circuit;

public interface IComponent
{
    IVariable Current { get; }
    IVariable Voltage { get; }
    IState? State { get; }
    VariableType? StateType { get; }

    string Name { get; }
    double Value { get; }

    int GetPriority();
    bool IsDisplacing();
    bool IsExternal();

    IEnumerable<ILinearEquation> Accept(IComponentRuleSetVisitor visitor);
}
