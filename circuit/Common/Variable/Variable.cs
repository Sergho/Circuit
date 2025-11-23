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
    public bool IsStated { get; private set; }
    public double? ExternalValue { get; private set; }
    public string Name { get; private set; }
    public string BaseName { get; private set; }

    public Variable(string baseName, VariableType type, bool isDerivative = false, bool isStated = false, double? externalValue = null)
    {
        string derivativePrefix = isDerivative ? "d" : "";

        BaseName = baseName;
        Name = $"{derivativePrefix}{typePrefixes[type]}{baseName}";
        Type = type;
        IsDerivative = isDerivative;
        IsStated = isStated;
        ExternalValue = externalValue;
    }

    public bool Equals(IVariable? other)
    {
        if (other == null) return false;

        return Name == other.Name;
    }
    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}
