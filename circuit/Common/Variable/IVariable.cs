namespace circuit;

public interface IVariable : IEquatable<IVariable>
{
    VariableType Type { get; }
    bool IsDerivative { get; }
    bool IsStated { get; }
    double? ExternalValue { get; }
    string Name { get; }
}
