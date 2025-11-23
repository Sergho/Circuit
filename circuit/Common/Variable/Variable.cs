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
    public bool IsExternal { get; private set; }
    public string Name { get; private set; }

    public Variable(string baseName, VariableType type, bool isDerivative = false, bool isStated = false, bool isExternal = false)
    {
        string derivativePrefix = isDerivative ? "d" : "";

        Name = $"{derivativePrefix}{typePrefixes[type]}{baseName}";
        Type = type;
        IsDerivative = isDerivative;
        IsStated = isStated;
        IsExternal = isExternal;
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
