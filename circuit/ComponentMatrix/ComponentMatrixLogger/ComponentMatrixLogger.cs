namespace circuit;

public class ComponentMatrixLogger : IComponentMatrixLogger
{
    public ComponentMatrixLogger() { }

    public void Log(IComponentMatrix matrix)
    {
        Console.WriteLine("--- Component Matrix Logger ---");
        foreach (var row in matrix.GetRows())
        {
            foreach (var col in matrix.GetCols())
            {
                MatrixCell cell = matrix.GetElem(row, col);

                Console.WriteLine($"{StringifyPair(row)} - {StringifyPair(col)}: {cell}");
            }
        }
    }

    private string StringifyPair(IComponentMatrixPair pair)
    {
        return $"{pair.Component.GetType().Name} ({pair.Current.Variable.Name})";
    }
}
