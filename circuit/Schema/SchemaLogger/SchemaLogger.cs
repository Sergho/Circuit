namespace circuit;

public class SchemaLogger : ISchemaLogger
{
    public SchemaLogger() { }

    public void Log(ISchema schema)
    {
        Console.WriteLine("--- Schema logging ---");
        foreach (IEdge edge in schema.GetEdges())
        {
            INode from = edge.From;
            INode to = edge.To;
            ICurrent current = edge.Current;
            IComponent component = edge.Component;

            if (current.Direction == Direction.Backward)
            {
                (from, to) = (to, from);
            }

            Console.WriteLine(StringifyEdge(edge));
            Console.WriteLine($"  From: {StringifyNode(from)}");
            Console.WriteLine($"  To: {StringifyNode(to)}");
            Console.WriteLine(StringifyCurrent(current));
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
    private string StringifyCurrent(ICurrent current)
    {
        return $"  Current: {current.Variable.Name}";
    }
    private string StringifyComponent(IComponent component)
    {
        string result = $"  {component.GetType().Name} ({component.Value})";
        
        if(component.State != null)
        {
            result += $": {component.State.Variable.Name}, {component.State.DVariable.Name}";
        }

        return result;
    }
}
