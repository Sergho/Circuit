namespace circuit;

public class Capacitor : AComponent
{
    public Capacitor(double value) : base(value, new State("U", StateType.Voltage)) { }
    public override int GetPriority() { return 2; }
    public override bool IsExternal() { return false; }
}
