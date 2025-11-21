namespace circuit;

public class Inductance : AComponent
{
    public Inductance(double value) : base(value, new State("I", StateType.Current)) { }
    public override int GetPriority() { return 2; }
    public override bool IsExternal() { return false; }
}
