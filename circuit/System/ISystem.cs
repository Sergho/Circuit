namespace circuit;

public interface ISystem
{
    void AddXCol(int col, int order);
    void AddDXCol(int col, int order);
    void AddYCol(int col, int order);
    void AddVCol(int col, int order);

    void AddVValue(double value, int order);

    IEnumerable<int> GetXCols();
    IEnumerable<int> GetDXCols();
    IEnumerable<int> GetYCols();
    IEnumerable<int> GetVCols();

    IEnumerable<double> GetVValues();

    double Get(int row, int col);
    void Set(int row, int col, double value);

    IEnumerable<int> GetRows();

    void Solve();
}
