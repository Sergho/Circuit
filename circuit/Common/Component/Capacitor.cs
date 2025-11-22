namespace circuit;

public class Capacitor : AComponent
{
    public Capacitor(string name, double value) : base(name, value, VariableType.Voltage) { }
    public override int GetPriority() { return 2; }
    public override bool IsExternal() { return false; }
}
