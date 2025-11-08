namespace circuit;

public class PowerSource : AComponent
{
    public PowerSource(double value) : base(value) { }
    public override int GetPriority() { return 3; }
    public override StateType GetStateType() { return StateType.None; }
    public override bool IsExternal() { return true; }
}
