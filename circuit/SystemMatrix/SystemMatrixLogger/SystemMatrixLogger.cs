namespace circuit;

public class SystemMatrixLogger : ISystemMatrixLogger
{
    public SystemMatrixLogger()
    {
    }

    public void Log(ISystemMatrix matrix)
    {
        Console.WriteLine("--- System Matrix Logging ---");
        Console.Write("Row\t");
        foreach (IVariable col in matrix.GetCols())
        {
            Console.Write($"{StringifyVariable(col)}\t");
        }
        Console.WriteLine();

        foreach (int row in matrix.GetRows())
        {
            Console.Write($"{row}\t");

            foreach (IVariable col in matrix.GetCols())
            {
                Console.Write($"{matrix.GetElem(row, col)}\t");
            }
            Console.WriteLine();
        }
    }

    private string StringifyVariable(IVariable variable)
    {
        return variable.Name;
    }
}
