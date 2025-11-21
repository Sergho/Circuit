namespace circuit;

public interface ICurrent : IEquatable<ICurrent>
{
    Direction Direction { get; }
    IVariable Variable { get; }

    ICurrent GetReversed();
}
