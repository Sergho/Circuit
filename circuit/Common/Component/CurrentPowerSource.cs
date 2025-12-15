namespace circuit;

public class CurrentPowerSource : AComponent
{
    public CurrentPowerSource(string name, double value) : base(name, value, null, VariableType.Current) { }
    public override int GetPriority() { return 3; }
    public override IEnumerable<ILinearEquation> Accept(IComponentRuleSetVisitor visitor)
    {
        return visitor.GetRules(this);
    }
}
