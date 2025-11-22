namespace circuit;

public class Inductance : AComponent
{
    public Inductance(string name, double value) : base(name, value, VariableType.Current) { }
    public override int GetPriority() { return 2; }
    public override bool IsExternal() { return false; }
}
