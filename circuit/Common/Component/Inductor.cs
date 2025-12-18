namespace circuit;

public class Inductor : AComponent
{
    public Inductor(string name, double value) : base(name, value, VariableType.Current)
    {
    }
    public override int GetPriority() { return 2; }
    public override IEnumerable<ILinearEquation> Accept(IComponentRuleSetVisitor visitor)
    {
        return visitor.GetRules(this);
    }
}
