namespace circuit;

public interface IVariable : IEquatable<IVariable>
{
    VariableType Type { get; }
    bool IsDerivative { get; }
    string Name { get; }
}
