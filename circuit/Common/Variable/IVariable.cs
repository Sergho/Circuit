namespace circuit;

public interface IVariable : IEquatable<IVariable>
{
    VariableType Type { get; }
    bool IsDerivative { get; }
    bool IsStated { get; }
    bool IsExternal { get; }
    string Name { get; }
}
