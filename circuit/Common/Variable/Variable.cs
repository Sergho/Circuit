namespace circuit;

public class Variable : IVariable
{
    private static Dictionary<string, int> currentIds = new();

    public string Name { get; private set; }

    public Variable(string baseName, bool generateName = true)
    {
        Name = generateName ? GenerateName(baseName) : baseName;
    }

    private string GenerateName(string baseName)
    {
        if (!currentIds.ContainsKey(baseName))
        {
            currentIds.Add(baseName, 1);
        }

        string name = $"{baseName}{currentIds[baseName]}";
        currentIds[baseName]++;

        return name;
    }
}
