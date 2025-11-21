using ScottPlot.TickGenerators.TimeUnits;
using System.Xml.Linq;

namespace circuit;

public class ComponentMatrixPair : IComponentMatrixPair
{
    public IComponent Component { get; private set; }
    public ICurrent Current { get; private set; }

    public ComponentMatrixPair(IComponent component, ICurrent current)
    {
        Component = component;
        Current = current;
    }

    public bool Equals(IComponentMatrixPair? other)
    {
        if (other == null) return false;

        return Component.Equals(other.Component) && Current.Equals(other.Current);
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(
            Component.GetHashCode(),
            Current.GetHashCode()
        );
    }
}
