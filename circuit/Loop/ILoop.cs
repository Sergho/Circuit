namespace circuit;

public interface ILoop
{
    bool Includes(IEdge edge);
    Direction? CompareDirections(IEdge first, IEdge second);
}
