using System;

namespace circuit;

internal enum Direction
{
    Forward,
    Backward,
}

internal static class DirectionHelper
{
    public static Direction GetOpposite(this Direction value)
    {
        return value == Direction.Forward ? Direction.Backward : Direction.Forward;
    }
}