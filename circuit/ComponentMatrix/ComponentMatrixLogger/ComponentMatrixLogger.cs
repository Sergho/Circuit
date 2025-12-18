namespace circuit;

public class ComponentMatrixLogger : IComponentMatrixLogger
{
    public ComponentMatrixLogger() { }

    public void Log(IComponentMatrix matrix)
    {
        Console.WriteLine("--- Component Matrix Logging ---");
        foreach (IComponent row in matrix.GetRows())
        {
            foreach (IComponent col in matrix.GetCols())
            {
                MatrixCell cell = matrix.GetElem(row, col);

                Console.WriteLine($"{StringifyComponent(row)} - {StringifyComponent(col)}: {cell}");
            }
        }
    }

    private string StringifyComponent(IComponent component)
    {
        return $"{component.GetType().Name} ({component.Current.Name}, {component.Voltage.Name})";
    }
}
