namespace circuit;

public class PowerSource : AComponent
{
    public PowerSource(string name, double value) : base(name, value) { }
    public override int GetPriority() { return 3; }
    public override bool IsExternal() { return true; }
    public override IEnumerable<ILinearEquation> Accept(IComponentRuleSetVisitor visitor)
    {
        return visitor.GetRules(this);
    }
}
