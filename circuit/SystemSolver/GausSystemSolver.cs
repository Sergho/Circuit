namespace circuit;

internal class GausSystemSolver : ISystemSolver
{
    private INumberMatrix matrix;

    public GausSystemSolver(INumberMatrix matrix)
    {
        this.matrix = matrix;
    }

    public void Solve(IEnumerable<int> cols)
    {
        Triangulate(cols);
        Diagonalize(cols);
    }

    private void Add(int fromRow, int toRow)
    {
        foreach(int col in matrix.GetCols())
        {
            double sum = matrix.Get(fromRow, col) + matrix.Get(toRow, col);
            matrix.Set(toRow, col, sum);
        }
    }
    private void Scale(int row, double factor)
    {
        foreach(int col in matrix.GetCols())
        {
            double scaled = matrix.Get(row, col) * factor;
            matrix.Set(row, col, scaled);
        }
    }
    private void Swap(int firstRow, int secondRow)
    {
        foreach(int col in matrix.GetCols())
        {
            double firstValue = matrix.Get(firstRow, col);
            double secondValue = matrix.Get(secondRow, col);

            matrix.Set(firstRow, col, secondValue);
            matrix.Set(secondRow, col, firstValue);
        }
    }

    private void Triangulate(IEnumerable<int> cols)
    {
        int solvedRowsCount = 0;
        foreach (int col in cols)
        {
            bool swapped = false;
            foreach (int row in matrix.GetRows())
            {
                if (row < solvedRowsCount) continue;
                double value = matrix.Get(row, col);

                if (value == 0) continue;

                Scale(row, 1 / value);

                if (!swapped)
                {
                    Swap(row, solvedRowsCount);
                    swapped = true;
                }
                else
                {
                    Scale(row, -1);
                    Add(solvedRowsCount, row);
                }
            }

            solvedRowsCount++;
        }
    }
    private void Diagonalize(IEnumerable<int> cols)
    {
        List<int> reversedCols = new List<int>(cols);
        reversedCols.Reverse();
        int solvedColsCount = 0;

        foreach(int col in reversedCols)
        {
            foreach(int row in matrix.GetRows())
            {
                if (row >= reversedCols.Count - solvedColsCount - 1) continue;
                double value = matrix.Get(row, col);

                if (value == 0) continue;

                Scale(row, -1 / value);
                Add(reversedCols.Count - solvedColsCount - 1, row);
                Scale(row, -value);
            }

            solvedColsCount++;
        }
    }
}
