namespace circuit;

public class ActivePowerSource : AComponent
{
    public IVariable LinkedVoltage { get; private set; }

    public ActivePowerSource(string name, double value, IVariable linkedVoltage) : base(name, value, null)
    {
        LinkedVoltage = linkedVoltage;
    }
    public override int GetPriority() { return 4; }
    public override IEnumerable<ILinearEquation> Accept(IComponentRuleSetVisitor visitor)
    {
        return visitor.GetRules(this);
    }
}
