namespace circuit;

public interface IComponentRuleSetVisitor
{
    IEnumerable<ILinearEquation> GetRules(Capacitor capacitor);
    IEnumerable<ILinearEquation> GetRules(Inductor inductance);
    IEnumerable<ILinearEquation> GetRules(Resistor resistor);
    IEnumerable<ILinearEquation> GetRules(VoltagePowerSource powerSource);
    IEnumerable<ILinearEquation> GetRules(CurrentPowerSource powerSource);
    IEnumerable<ILinearEquation> GetRules(ActivePowerSource activePowerSource);
}
