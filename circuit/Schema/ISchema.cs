namespace circuit;

internal interface ISchema
{
    INode getNode(int id);
    void LinkNode(int fromId, int toId, IComponent component, Direction direction = Direction.Forward);
    Dictionary<IEdge, Dictionary<IEdge, int>> getMatrix();
}
