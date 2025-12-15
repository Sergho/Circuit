namespace circuit;

public class VoltagePowerSource : AComponent
{
    public VoltagePowerSource(string name, double value) : base(name, value, null, VariableType.Voltage) { }
    public override int GetPriority() { return 3; }
    public override IEnumerable<ILinearEquation> Accept(IComponentRuleSetVisitor visitor)
    {
        return visitor.GetRules(this);
    }
}
