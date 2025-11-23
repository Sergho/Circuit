namespace circuit;

public interface IComponentRuleSetVisitor
{
    IEnumerable<ILinearEquation> GetRules(IComponent component);
    IEnumerable<ILinearEquation> GetRules(Capacitor capacitor);
    IEnumerable<ILinearEquation> GetRules(Inductance inductance);
    IEnumerable<ILinearEquation> GetRules(Resistor resistor);
}
