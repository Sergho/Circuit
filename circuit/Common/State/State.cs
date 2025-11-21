namespace circuit;

public class State : IState
{
    public StateType Type { get; private set; }
    public IVariable Variable { get; private set; }
    public IVariable DVariable { get; private set; }

    public State(string baseName, StateType type)
    {
        Type = type;
        Variable = new Variable(baseName);
        DVariable = new Variable($"d{Variable.Name}", false);
    }
}
