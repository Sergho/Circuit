namespace circuit;

public interface ISystem
{
    void SetX(IEnumerable<int> cols);
    void SetY(IEnumerable<int> cols);
    void SetV(IEnumerable<int> cols);
}
