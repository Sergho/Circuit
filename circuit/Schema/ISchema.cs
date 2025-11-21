namespace circuit;

public interface ISchema : ICloneable
{
    void AddNode(INode node);
    void AddEdge(IEdge edge);
    bool HasEdge(IEdge edge);
    void RemoveEdge(IEdge edge);

    IEnumerable<INode> GetNodes(Func<INode, bool>? filter = null);
    IEnumerable<IEdge> GetEdges(Func<IEdge, bool>? filter = null);

    ISchema GetTree();
    ISchema GetDiff(ISchema other);
    ILoop? GetLoop();
}
