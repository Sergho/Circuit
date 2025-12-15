namespace circuit;

public class Inductance : AComponent
{
    public Inductance(string name, double value) : base(name, value, VariableType.Current)
    {
    }
    public override int GetPriority() { return 2; }
    public override IEnumerable<ILinearEquation> Accept(IComponentRuleSetVisitor visitor)
    {
        return visitor.GetRules(this);
    }
}
