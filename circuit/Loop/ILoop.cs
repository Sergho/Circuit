namespace circuit;

internal interface ILoop
{
    void SetPath(List<IEdge> path);
    bool Includes(IEdge edge);
    Direction? CompareDirections(IEdge first, IEdge second);
}
