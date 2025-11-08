namespace circuit;

public class Inductance : AComponent
{
    public Inductance(double value) : base(value) { }
    public override int GetPriority() { return 2; }
    public override StateType GetStateType() { return StateType.Current; }
    public override bool IsExternal() { return false; }
}
