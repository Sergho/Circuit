namespace circuit;

internal class PowerSource : AComponent
{
    public PowerSource(double value) : base(value) { }
    public override int getPriority() { return 3; }
}
