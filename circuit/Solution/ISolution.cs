namespace circuit;

internal interface ISolution
{
    IEnumerable<double> GetX();
    IEnumerable<double> GetY();
    double GetTime();

    void Next();
}
