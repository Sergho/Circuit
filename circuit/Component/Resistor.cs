namespace circuit;

internal class Resistor : AComponent
{
    public Resistor(double value) : base(value) { }
    public override int getPriority() { return 1; }
}
