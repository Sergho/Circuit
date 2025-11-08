namespace circuit;

public enum Direction
{
    Forward,
    Backward,
}

public static class DirectionHelper
{
    public static Direction GetOpposite(this Direction value)
    {
        return value == Direction.Forward ? Direction.Backward : Direction.Forward;
    }
}