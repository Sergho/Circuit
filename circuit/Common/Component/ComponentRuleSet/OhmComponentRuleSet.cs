
namespace circuit;

public class OhmComponentRuleSet : IComponentRuleSetVisitor
{
    public OhmComponentRuleSet()
    {
    }

    public IEnumerable<ILinearEquation> GetRules(Capacitor capacitor)
    {
        if(capacitor.State == null)
        {
            throw new Exception("State is not defined for capacitor");
        }

        IState state = capacitor.State;
        ILinearEquation rule = new LinearEquation();
        rule.Add(capacitor.Current, 1);
        rule.Add(state.DVariable, -1 * capacitor.Value);

        return new List<ILinearEquation> { rule };
    }

    public IEnumerable<ILinearEquation> GetRules(Inductance inductance)
    {
        if (inductance.State == null)
        {
            throw new Exception("State is not defined for inductance");
        }

        IState state = inductance.State;
        ILinearEquation rule = new LinearEquation();
        rule.Add(inductance.Voltage, 1);
        rule.Add(state.DVariable, -1 * inductance.Value);

        return new List<ILinearEquation> { rule };
    }

    public IEnumerable<ILinearEquation> GetRules(Resistor resistor)
    {
        ILinearEquation rule = new LinearEquation();
        rule.Add(resistor.Voltage, 1);
        rule.Add(resistor.Current, -1 * resistor.Value);

        return new List<ILinearEquation> { rule };
    }

    public IEnumerable<ILinearEquation> GetRules(VoltagePowerSource powerSource)
    {
        return new List<ILinearEquation>();
    }

    public IEnumerable<ILinearEquation> GetRules(CurrentPowerSource powerSource)
    {
        return new List<ILinearEquation>();
    }

    public IEnumerable<ILinearEquation> GetRules(ActivePowerSource activePowerSource)
    {
        ILinearEquation rule = new LinearEquation();
        rule.Add(activePowerSource.LinkedVoltage, 1 * activePowerSource.Value);
        rule.Add(activePowerSource.Current, -1);

        return new List<ILinearEquation> { rule };
    }
}
