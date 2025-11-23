namespace circuit;

public class State : IState
{
    public VariableType Type => Variable.Type;
    public IVariable Variable { get; private set; }
    public IVariable DVariable { get; private set; }

    public State(string baseName, VariableType type, bool isExternal = false)
    {
        Variable = new Variable(baseName, type, false, true, isExternal);
        DVariable = new Variable(baseName, type, true, true, isExternal);
    }
}
