namespace circuit;

public class Current : ICurrent
{
    public Direction Direction { get; private set; }
    public IVariable Variable { get; private set; }

    public Current(Direction direction, IVariable? variable = null)
    {
        Direction = direction;

        Variable = variable?? new Variable("I");
    }

    public ICurrent GetReversed()
    {
        return new Current(Direction.GetOpposite(), Variable);
    }

    public bool Equals(ICurrent? other)
    {
        if (other == null) return false;

        return Variable.Name == other.Variable.Name;
    }
    public override int GetHashCode()
    {
        return Variable.Name.GetHashCode();
    }
}
