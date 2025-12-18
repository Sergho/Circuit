namespace circuit;

public interface IState
{
    VariableType Type { get; }
    IVariable Variable { get; }
    IVariable DVariable { get; }
}
