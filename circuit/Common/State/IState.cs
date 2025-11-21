namespace circuit;

public interface IState
{
    StateType Type { get; }
    IVariable Variable { get; }
    IVariable DVariable { get; }
}
