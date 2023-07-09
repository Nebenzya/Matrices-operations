namespace MatrixLib;

public partial class Matrix
{
    public void FillRandomValue(int minValue, int maxValue)
    {
        var random = new Random();

        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _columns; j++)
            {
                _array[i, j] = random.Next(minValue, maxValue);
            }
        }
    }

    public Matrix Transpose()
    {
        var result = new Matrix(_columns, _rows);

        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _columns; j++)
            {
                result._array[j, i] = _array[i, j];
            }
        }

        return result;
    }

    private bool EqualSize(Matrix? otherMatrix) => _rows == otherMatrix?._rows && _columns == otherMatrix?._columns;
}
