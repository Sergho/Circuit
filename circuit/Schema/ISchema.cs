namespace circuit;

internal interface ISchema : ICloneable
{
    void AddNode(INode node);
    INode GetNode(int id);
    void RemoveNode(int id);
    void AddEdge(IEdge edge);
    bool HasEdge(IEdge edge);
    void RemoveEdge(IEdge edge);

    IEnumerable<INode> GetNodes(Func<INode, bool>? filter = null);
    IEnumerable<IEdge> GetEdges(Func<IEdge, bool>? filter = null);

    ISchema GetOnlyNodes();
    ISchema GetTree();
    ISchema GetDiff(ISchema other);

    ILoop? GetLoop();

    IEdgeMatrix GetEdgeMatrix();
}
