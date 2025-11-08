namespace circuit.Tests;

public class DirectionTests
{
    [Theory]
    [InlineData(Direction.Forward, Direction.Backward)]
    [InlineData(Direction.Backward, Direction.Forward)]
    public void GetOpposite_ReturnsCorrectDirection(Direction direction, Direction opposite)
    {
        Assert.Equal(direction.GetOpposite(), opposite);
    }
}
