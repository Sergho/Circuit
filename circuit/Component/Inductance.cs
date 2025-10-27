namespace circuit;

internal class Inductance : AComponent
{
    public Inductance(double value) : base(value) { }
    public override int getPriority() { return 2; }
}
