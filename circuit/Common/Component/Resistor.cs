namespace circuit;

public class Resistor : AComponent
{
    public Resistor(double value) : base(value) { }
    public override int GetPriority() { return 1; }
    public override bool IsExternal() { return false; }
}
