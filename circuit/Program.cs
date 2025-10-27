namespace circuit;

internal class Program
{
    public static void Main()
    {
        List<INode> nodes = new List<INode>
        {
            new Node(1),
            new Node(2),
            new Node(3),
        };
        Schema schema = new Schema(nodes);

        schema.LinkNode(1, 2, new Capacitor(0));
        schema.LinkNode(1, 2, new Resistor(0));
        schema.LinkNode(2, 1, new PowerSource(0));
        schema.LinkNode(1, 3, new Inductance(0));
        schema.LinkNode(3, 2, new Resistor(0));

        var matrix = schema.getMatrix();

        foreach(IEdge addition in matrix.Keys)
        {
            foreach(IEdge edge in matrix[addition].Keys)
            {
                Console.WriteLine($"Addition: {addition.Id}, Edge: {edge.Id}, Value: {matrix[addition][edge]}");
            }
        }
    }
}