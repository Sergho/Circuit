namespace circuit;

public class Variable : IVariable
{
    private readonly Dictionary<VariableType, string> typePrefixes = new()
    {
        { VariableType.Current, "I" },
        { VariableType.Voltage, "U" },
    };

    public VariableType Type { get; private set; }
    public bool IsDerivative { get; private set; }
    public string Name { get; private set; }

    public Variable(string baseName, VariableType type, bool isDerivative = false)
    {
        string derivativePrefix = isDerivative ? "d" : "";

        Name = $"{derivativePrefix}{typePrefixes[type]}{baseName}";
        Type = type;
        IsDerivative = isDerivative;
    }

    public bool Equals(IVariable? other)
    {
        if (other == null) return false;

        bool nameEquals = Name == other.Name;
        bool isDerivativeEquals = IsDerivative == other.IsDerivative;
        bool typeEquals = Type == other.Type;

        return nameEquals && isDerivativeEquals && typeEquals;
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(
            Name.GetHashCode(),
            IsDerivative.GetHashCode(),
            Type.GetHashCode()
        );
    }
}
