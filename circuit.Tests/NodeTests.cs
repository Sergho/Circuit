namespace circuit.Tests;

public class NodeTests
{
    public static IEnumerable<object[]> EqualsTestData()
    {
        yield return new object[] { new Node(1), new Node(1), true };
        yield return new object[] { new Node(1), new Node(2), false };
    }

    [Theory]
    [MemberData(nameof(EqualsTestData))]
    public void Equals_ComparesCorrectly(INode first, INode second, bool areEqual)
    {
        Assert.Equal(first.Equals(second), areEqual);
        Assert.Equal(second.Equals(first), areEqual);
    }

    [Theory]
    [MemberData(nameof(EqualsTestData))]
    public void GetHashCode_ComparesCorrectly(INode first, INode second, bool areEqual)
    {
        Assert.Equal(first.GetHashCode() == second.GetHashCode(), areEqual);
    }
}
