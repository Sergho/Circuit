namespace circuit;

public class Capacitor : AComponent
{
    public Capacitor(double value) : base(value) { }
    public override int GetPriority() { return 2; }
    public override StateType GetStateType() { return StateType.Voltage; }
    public override bool IsExternal() { return false; }
}
