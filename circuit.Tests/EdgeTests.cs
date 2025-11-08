namespace circuit.Tests;

public class EdgeTests
{
    public static readonly List<INode> Nodes = new List<INode>() { new Node(1), new Node(2) };
    public static readonly List<IComponent> Components = new List<IComponent>() { new Capacitor(0), new Resistor(0) };
    public static readonly List<ICurrent> Currents = new List<ICurrent>() { new Current(0, Direction.Forward), new Current(1, Direction.Backward) };
    public static IEnumerable<object[]> EqualsTestData()
    {
        yield return new object[] {
            new Edge(1, Nodes[0], Nodes[1], Components[0], Currents[0]),
            new Edge(1, Nodes[0], Nodes[1], Components[0], Currents[0]),
            true
        };
        yield return new object[] {
            new Edge(1, Nodes[1], Nodes[0], Components[0], Currents[0]),
            new Edge(1, Nodes[0], Nodes[1], Components[0], Currents[0]),
            true
        };
        yield return new object[] {
            new Edge(1, Nodes[0], Nodes[1], Components[0], Currents[0]),
            new Edge(1, Nodes[0], Nodes[1], Components[1], Currents[0]),
            true
        };
        yield return new object[] {
            new Edge(1, Nodes[0], Nodes[1], Components[0], Currents[0]),
            new Edge(1, Nodes[0], Nodes[1], Components[1], Currents[1]),
            true
        };
        yield return new object[] {
            new Edge(1, Nodes[0], Nodes[1], Components[0], Currents[0]),
            new Edge(2, Nodes[0], Nodes[1], Components[1], Currents[1]),
            false
        };
    }
    public static IEnumerable<object[]> GetReversedTestData()
    {
        yield return new object[] {
            new Edge(1, Nodes[0], Nodes[1], Components[0], Currents[0]),
            new Edge(1, Nodes[1], Nodes[0], Components[0], Currents[0])
        };
        yield return new object[] {
            new Edge(2, Nodes[0], Nodes[1], Components[0], Currents[0]),
            new Edge(2, Nodes[1], Nodes[0], Components[0], Currents[0])
        };
    }

    [Theory]
    [MemberData(nameof(EqualsTestData))]
    public void Equals_ComparesCorrectly(IEdge first, IEdge second, bool areEqual)
    {
        Assert.Equal(first.Equals(second), areEqual);
        Assert.Equal(second.Equals(first), areEqual);
    }

    [Theory]
    [MemberData(nameof(EqualsTestData))]
    public void GetHashCode_ComparesCorrectly(IEdge first, IEdge second, bool areEqual)
    {
        Assert.Equal(first.GetHashCode() == second.GetHashCode(), areEqual);
    }

    [Theory]
    [MemberData(nameof(GetReversedTestData))]
    public void GetReversed_ReturnsCorrectResult(IEdge edge, IEdge reversed)
    {
        Assert.Equal(edge.GetReversed(), reversed);
    }
}
