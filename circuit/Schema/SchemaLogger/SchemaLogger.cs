namespace circuit;

public class SchemaLogger : ISchemaLogger
{
    public SchemaLogger() { }

    public void Log(ISchema schema)
    {
        Console.WriteLine("--- Schema Logging ---");
        foreach (IEdge edge in schema.GetEdges())
        {
            INode from = edge.From;
            INode to = edge.To;
            IComponent component = edge.Component;

            Console.WriteLine(StringifyEdge(edge));
            Console.WriteLine($"  From: {StringifyNode(from)}");
            Console.WriteLine($"  To: {StringifyNode(to)}");
            Console.WriteLine(StringifyComponent(component));
        }
    }

    private string StringifyEdge(IEdge edge)
    {
        return $"* Edge - {edge.Id}";
    }
    private string StringifyNode(INode node)
    {
        return $"Node - {node.Id}";
    }
    private string StringifyComponent(IComponent component)
    {
        string result = $"  Current: {component.Current.Name}\n";
        result += $"  Voltage: {component.Voltage.Name}\n";
        result += $"  {component.GetType().Name} ({component.Value})";
        
        if(component?.State != null)
        {
            result += $": {component.State.Variable.Name}, {component.State.DVariable.Name}";
        }

        return result;
    }
}
