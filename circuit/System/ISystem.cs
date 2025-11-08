namespace circuit;

public interface ISystem
{
    void AddXCol(int col);
    void AddDXCol(int col);
    void AddYCol(int col);
    void AddVCol(int col);

    double Get(int row, int col);
    void Set(int row, int col, double value);

    void Solve();
}
