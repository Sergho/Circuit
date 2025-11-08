namespace circuit;

public class Current : ICurrent
{
    public int Id {  get; private set; }
    public Direction Direction { get; private set; }

    public Current(int id, Direction direction)
    {
        Id = id;
        Direction = direction;
    }

    public ICurrent GetReversed()
    {
        return new Current(Id, Direction.GetOpposite());
    }

    public bool Equals(ICurrent? other)
    {
        if (other == null) return false;

        return Id == other.Id;
    }
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
