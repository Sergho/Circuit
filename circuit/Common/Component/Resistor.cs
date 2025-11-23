namespace circuit;

public class Resistor : AComponent
{
    public Resistor(string name, double value) : base(name, value) { }
    public override int GetPriority() { return 1; }
    public override bool IsExternal() { return false; }
    public override IEnumerable<ILinearEquation> Accept(IComponentRuleSetVisitor visitor)
    {
        return visitor.GetRules(this);
    }
}
