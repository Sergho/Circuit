namespace circuit;

internal class GausSystemMatrixSolver : ISystemMatrixSolver
{
    public GausSystemMatrixSolver()
    {
    }

    public void Solve(ISystemMatrix matrix)
    {
        Triangulate(matrix);
        Diagonalize(matrix);
    }

    private void Add(ISystemMatrix matrix, int fromRow, int toRow)
    {
        foreach (IVariable col in matrix.GetCols())
        {
            double sum = matrix.GetElem(fromRow, col) + matrix.GetElem(toRow, col);
            matrix.SetElem(toRow, col, sum);
        }
    }
    private void Scale(ISystemMatrix matrix, int rowIndex, double factor)
    {
        foreach (IVariable col in matrix.GetCols())
        {
            double scaled = matrix.GetElem(rowIndex, col) * factor;
            matrix.SetElem(rowIndex, col, scaled);
        }
    }
    private void Swap(ISystemMatrix matrix, int firstRow, int secondRow)
    {
        foreach (IVariable col in matrix.GetCols())
        {
            double firstValue = matrix.GetElem(firstRow, col);
            double secondValue = matrix.GetElem(secondRow, col);

            matrix.SetElem(firstRow, col, secondValue);
            matrix.SetElem(secondRow, col, firstValue);
        }
    }

    private void Triangulate(ISystemMatrix matrix)
    {
        int solvedCount = 0;
        foreach (IVariable col in matrix.GetCols())
        {
            bool swapped = false;
            foreach (int row in matrix.GetRows())
            {
                if (row < solvedCount) continue;
                double value = matrix.GetElem(row, col);

                if (value == 0) continue;

                Scale(matrix, row, 1 / value);

                if (!swapped)
                {
                    Swap(matrix, row, solvedCount);
                    swapped = true;
                }
                else
                {
                    Scale(matrix, row, -1);
                    Add(matrix, solvedCount, row);
                }
            }

            solvedCount++;
        }
    }
    private void Diagonalize(ISystemMatrix matrix)
    {
        int rowsCount = matrix.GetRowsCount();
        List<IVariable> reversedCols = matrix.GetCols().Take(rowsCount).ToList();
        reversedCols.Reverse();
        int solvedCount = 0;

        foreach (IVariable col in reversedCols)
        {
            foreach (int row in matrix.GetRows())
            {
                if (row >= reversedCols.Count - solvedCount - 1) continue;
                double value = matrix.GetElem(row, col);

                if (value == 0) continue;

                Scale(matrix, row, -1 / value);
                Add(matrix, reversedCols.Count - solvedCount - 1, row);
                Scale(matrix, row, -value);
            }

            solvedCount++;
        }
    }
}
