namespace circuit.Tests;

public class LoopTests
{
    public static readonly List<INode> Nodes = new List<INode>() { new Node(1), new Node(2), new Node(3), new Node(4), new Node(5) };
    public static readonly List<IComponent> Components = new List<IComponent>() { new Resistor(0) };
    public static readonly List<ICurrent> Currents = new List<ICurrent>() { new Current(1, Direction.Forward) };
    public static readonly List<IEdge> Edges = new List<IEdge>() {
        new Edge(1, Nodes[0], Nodes[1], Components[0], Currents[0]),
        new Edge(2, Nodes[1], Nodes[2], Components[0], Currents[0]),
        new Edge(3, Nodes[2], Nodes[3], Components[0], Currents[0]),
        new Edge(4, Nodes[3], Nodes[1], Components[0], Currents[0]),
        new Edge(5, Nodes[1], Nodes[4], Components[0], Currents[0]),
    };
    public static readonly ILoop Loop = new Loop(Edges);
    public static IEnumerable<object[]> IncludesTestData()
    {
        yield return new object[] { new Edge(6, new Node(3), new Node(4), Components[0], Currents[0]), false };
        yield return new object[] { Edges[0], false };
        yield return new object[] { Edges[1], true };
        yield return new object[] { Edges[2], true };
        yield return new object[] { Edges[3], true };
        yield return new object[] { Edges[4], false };
    }
    public static IEnumerable<object[]> CompareDirectionsTestData()
    {
        yield return new object[] { Edges[0], Edges[1], null };
        yield return new object[] { Edges[1], Edges[2], Direction.Forward };
        yield return new object[] { Edges[2], Edges[1].GetReversed(), Direction.Backward };
        yield return new object[] { Edges[1], Edges[1], Direction.Forward };
        yield return new object[] { Edges[1], Edges[1].GetReversed(), Direction.Backward };
    }

    [Theory]
    [MemberData(nameof(IncludesTestData))]
    public void Includes_IdentifiesCorrectly(IEdge edge, bool includes)
    {
        Assert.Equal(Loop.Includes(edge), includes);
    }

    [Theory]
    [MemberData(nameof(CompareDirectionsTestData))]
    public void CompareDirections_CompareCorrectly(IEdge first, IEdge second, Direction? direction)
    {
        Assert.Equal(Loop.CompareDirections(first, second), direction);
    }
}
