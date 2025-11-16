namespace circuit.Tests;

public class MatrixTests
{
    public static readonly int maxRow = 5;
    public static readonly int maxCol = 12;
    public static readonly IMatrix<int, int, double> Matrix = new NumberMatrix();

    public MatrixTests()
    {
        for (int row = 0; row <= maxRow; row++)
        {
            for (int col = 0; col <= maxCol; col += 2)
            {
                Matrix.SetElem(row, col, row + col);
            }
        }

        Matrix.DeleteElem(4, 4);
    }

    [Theory]
    [InlineData(-1, false)]
    [InlineData(0, true)]
    [InlineData(3, true)]
    [InlineData(5, true)]
    [InlineData(6, false)]
    public void HasRow_ReturnsCorrectResult(int row, bool hasRow)
    {
        Assert.Equal(Matrix.HasRow(row), hasRow);
    }

    [Theory]
    [InlineData(-1, false)]
    [InlineData(0, true)]
    [InlineData(2, true)]
    [InlineData(3, false)]
    [InlineData(11, false)]
    [InlineData(12, true)]
    [InlineData(13, false)]
    public void HasCol_ReturnsCorrectResult(int col, bool hasCol)
    {
        Assert.Equal(Matrix.HasCol(col), hasCol);
    }

    [Theory]
    [InlineData(-1, -1, false)]
    [InlineData(0, 0, true)]
    [InlineData(2, 2, true)]
    [InlineData(4, 7, false)]
    [InlineData(5, 12, true)]
    [InlineData(5, 13, false)]
    [InlineData(6, 12, false)]
    [InlineData(6, 13, false)]
    public void HasElem_ReturnsCorrectResult(int row, int col, bool hasElem)
    {
        Assert.Equal(Matrix.HasElem(row, col), hasElem);
    }

    [Theory]
    [InlineData(0, 0, 0)]
    [InlineData(2, 2, 4)]
    [InlineData(5, 12, 17)]
    public void GetElem_ReturnsExisting(int row, int col, double elem)
    {
        Assert.True(Matrix.HasElem(row, col));
        Assert.Equal(Matrix.GetElem(row, col), elem);
    }

    [Theory]
    [InlineData(-1, -1)]
    [InlineData(4, 7)]
    [InlineData(5, 13)]
    [InlineData(6, 12)]
    [InlineData(6, 13)]
    public void GetElem_ThrowsErrorForNotExisting(int row, int col)
    {
        Assert.False(Matrix.HasElem(row, col));
        Assert.Throws<Exception>(() => Matrix.GetElem(row, col));
    }

    [Theory]
    [InlineData(0, 0, 1)]
    [InlineData(2, 2, 5)]
    [InlineData(5, 12, 10)]
    public void SetElem_ChangesValue(int row, int col, double elem)
    {
        double lastValue = Matrix.GetElem(row, col);
        Assert.NotEqual(lastValue, elem);
        Matrix.SetElem(row, col, elem);
        Assert.Equal(Matrix.GetElem(row, col), elem);
        Matrix.SetElem(row, col, lastValue);
    }

    [Theory]
    [InlineData(-1, -1, -2)]
    [InlineData(4, 7, 11)]
    [InlineData(5, 13, 18)]
    [InlineData(6, 12, 18)]
    [InlineData(6, 13, 19)]
    public void SetElem_CreatesElement(int row, int col, double elem)
    {
        Assert.False(Matrix.HasElem(row, col));
        Matrix.SetElem(row, col, elem);
        Assert.Equal(Matrix.GetElem(row, col), elem);
        Matrix.DeleteElem(row, col);
    }

    [Theory]
    [InlineData(-1, -1, -2)]
    [InlineData(6, 12, 18)]
    [InlineData(6, 13, 19)]
    public void SetElem_CreatesRow(int row, int col, double elem)
    {
        Assert.False(Matrix.HasRow(row));
        Matrix.SetElem(row, col, elem);
        Assert.True(Matrix.HasRow(row));
        Matrix.DeleteElem(row, col);
    }

    [Theory]
    [InlineData(-1, -1, -2)]
    [InlineData(4, 7, 11)]
    [InlineData(5, 13, 18)]
    [InlineData(6, 13, 19)]
    public void SetElem_CreatesCol(int row, int col, double elem)
    {
        Assert.False(Matrix.HasCol(col));
        Matrix.SetElem(row, col, elem);
        Assert.True(Matrix.HasCol(col));
        Matrix.DeleteElem(row, col);
    }

    [Theory]
    [InlineData(0, new double[] { 0, 2, 4, 6, 8, 10, 12 })]
    [InlineData(4, new double[] { 4, 6, 10, 12, 14, 16 })]
    public void GetRow_ReturnsExisting(int row, double[] elems)
    {
        Assert.True(Matrix.HasRow(row));

        int index = 0;
        foreach (double elem in Matrix.GetRow(row))
        {
            Assert.Equal(elem, elems[index]);
            index++;
        }
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(6)]
    public void GetRow_ThrowsErrorForNotExisting(int row)
    {
        Assert.False(Matrix.HasRow(row));
        Assert.Throws<Exception>(() => Matrix.GetRow(row).First());
    }

    [Theory]
    [InlineData(0, new double[] { 0, 1, 2, 3, 4, 5 })]
    [InlineData(4, new double[] { 4, 5, 6, 7, 9 })]
    public void GetCol_ReturnsExisting(int col, double[] elems)
    {
        Assert.True(Matrix.HasCol(col));

        int index = 0;
        foreach (double elem in Matrix.GetCol(col))
        {
            Assert.Equal(elem, elems[index]);
            index++;
        }
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(13)]
    public void GetCol_ThrowsErrorForNotExisting(int col)
    {
        Assert.False(Matrix.HasCol(col));
        Assert.Throws<Exception>(() => Matrix.GetCol(col).First());
    }

    [Fact]
    public void GetRows_ReturnsCorrectResult()
    {
        int[] targetRows = { 0, 1, 2, 3, 4, 5 };

        int index = 0;
        foreach(int row in Matrix.GetRows())
        {
            Assert.Equal(row, targetRows[index]);
            index++;
        }
    }

    [Fact]
    public void GetCols_ReturnsCorrectResult()
    {
        int[] targetCols = { 0, 2, 4, 6, 8, 10, 12 };

        int index = 0;
        foreach (int col in Matrix.GetCols())
        {
            Assert.Equal(col, targetCols[index]);
            index++;
        }
    }

    [Fact]
    public void GetRowsCount_ReturnsCorrectResult()
    {
        Assert.Equal(Matrix.GetRowsCount(), Matrix.GetRows().Count());
    }

    [Fact]
    public void GetColsCount_ReturnsCorrectResult()
    {
        Assert.Equal(Matrix.GetColsCount(), Matrix.GetCols().Count());
    }

    [Theory]
    [InlineData(6)]
    [InlineData(7)]
    public void DeleteRow_RemovesRow(int row)
    {
        Assert.False(Matrix.HasRow(row));

        Matrix.SetElem(row, 0, row);
        Matrix.SetElem(row, 1, row + 1);

        Assert.True(Matrix.HasRow(row));
        Matrix.DeleteRow(row);
        Assert.False(Matrix.HasRow(row));
    }

    [Theory]
    [InlineData(6)]
    [InlineData(7)]
    public void DeleteRow_ThrowsErrorForNotExisting(int row)
    {
        Assert.False(Matrix.HasRow(row));
        Assert.Throws<Exception>(() => Matrix.DeleteRow(row));
    }

    [Theory]
    [InlineData(13)]
    [InlineData(14)]
    public void DeleteCol_RemovesCol(int col)
    {
        Assert.False(Matrix.HasCol(col));

        Matrix.SetElem(0, col, col);
        Matrix.SetElem(1, col, col + 1);

        Assert.True(Matrix.HasCol(col));
        Matrix.DeleteCol(col);
        Assert.False(Matrix.HasCol(col));
    }

    [Theory]
    [InlineData(13)]
    [InlineData(14)]
    public void DeleteCol_ThrowsErrorForNotExisting(int col)
    {
        Assert.False(Matrix.HasCol(col));
        Assert.Throws<Exception>(() => Matrix.DeleteCol(col));
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(2, 2)]
    [InlineData(5, 12)]
    public void DeleteElem_RemovesElement(int row, int col)
    {
        Assert.True(Matrix.HasElem(row, col));
        double lastValue = Matrix.GetElem(row, col);
        Matrix.DeleteElem(row, col);
        Assert.False(Matrix.HasElem(row, col));
        Matrix.SetElem(row, col, lastValue);
    }

    [Theory]
    [InlineData(-1, -1)]
    [InlineData(4, 7)]
    [InlineData(5, 13)]
    [InlineData(6, 12)]
    public void DeleteElem_ThrowsErrorForNotExisting(int row, int col)
    {
        Assert.False(Matrix.HasElem(row, col));
        Assert.Throws<Exception>(() => Matrix.DeleteElem(row, col));
    }

    [Theory]
    [InlineData(-1, -1)]
    [InlineData(6, 12)]
    [InlineData(6, 13)]
    public void DeleteElem_RemovesRow(int row, int col)
    {
        Assert.False(Matrix.HasRow(row));
        Matrix.SetElem(row, col, row + col);
        Assert.True(Matrix.HasRow(row));
        Matrix.DeleteElem(row, col);
        Assert.False(Matrix.HasRow(row));
    }

    [Theory]
    [InlineData(-1, -1)]
    [InlineData(4, 7)]
    [InlineData(5, 13)]
    [InlineData(6, 13)]
    public void DeleteElem_RemovesCol(int row, int col)
    {
        Assert.False(Matrix.HasCol(col));
        Matrix.SetElem(row, col, row + col);
        Assert.True(Matrix.HasCol(col));
        Matrix.DeleteElem(row, col);
        Assert.False(Matrix.HasCol(col));
    }
}
