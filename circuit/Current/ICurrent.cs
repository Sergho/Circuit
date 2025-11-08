namespace circuit;

public interface ICurrent : IEquatable<ICurrent>
{
    int Id { get; }
    Direction Direction { get; }

    ICurrent GetReversed();
}
