namespace circuit;

internal class Capacitor : AComponent
{
    public Capacitor(double value) : base(value) { }
    public override int getPriority() { return 2; }
}
