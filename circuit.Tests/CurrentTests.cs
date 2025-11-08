namespace circuit.Tests;

public class CurrentTests
{
    public static IEnumerable<object[]> EqualsTestData()
    {
        yield return new object[] { new Current(1, Direction.Forward), new Current(1, Direction.Forward), true };
        yield return new object[] { new Current(1, Direction.Forward), new Current(1, Direction.Backward), true };
        yield return new object[] { new Current(1, Direction.Forward), new Current(2, Direction.Forward), false };
        yield return new object[] { new Current(1, Direction.Forward), new Current(3, Direction.Backward), false };
    }
    public static IEnumerable<object[]> GetReversedTestData()
    {
        yield return new object[] { new Current(1, Direction.Forward), new Current(1, Direction.Backward) };
        yield return new object[] { new Current(1, Direction.Backward), new Current(1, Direction.Forward) };
        yield return new object[] { new Current(2, Direction.Backward), new Current(2, Direction.Forward) };
    }

    [Theory]
    [MemberData(nameof(EqualsTestData))]
    public void Equals_ComparesCorrectly(ICurrent first, ICurrent second, bool areEqual)
    {
        Assert.Equal(first.Equals(second), areEqual);
        Assert.Equal(second.Equals(first), areEqual);
    }

    [Theory]
    [MemberData(nameof(EqualsTestData))]
    public void GetHashCode_ComparesCorrectly(ICurrent first, ICurrent second, bool areEqual)
    {
        Assert.Equal(first.GetHashCode() == second.GetHashCode(), areEqual);
    }

    [Theory]
    [MemberData(nameof(GetReversedTestData))]
    public void GetReversed_ReturnsCorrectResult(ICurrent current, ICurrent reversed)
    {
        Assert.Equal(current.GetReversed(), reversed);
    }
}
