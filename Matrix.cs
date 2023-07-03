using System.Text;

namespace Matrices_operations;

public class Matrix
{
    private int[,] _array;
    private int _rows, _columns;

    public int Rows => _rows;
    public int Columns => _columns;

    public int this[int i, int j]
    {
        get
        {
            if (i < 0 || i >= _rows || j < 0 || j >= _columns)
                throw new IndexOutOfRangeException();
            return _array[i, j];
        }
        set
        {
            if (i < 0 || i >= _rows || j < 0 || j >= _columns)
                throw new IndexOutOfRangeException();
            _array[i, j] = value;
        }
    }

    #region Constructors
    public Matrix(int size) : this(size, size)
    {
    }

    public Matrix(int rows, int columns)
    {
        _rows = rows;
        _columns = columns;
        _array = new int[rows, columns];
    }

    /// <summary>
    /// Deep copy constructor
    /// </summary>
    public Matrix(Matrix previousMatrix)
    {
        _rows = previousMatrix._rows;
        _columns = previousMatrix._columns;
        _array = previousMatrix._array.Clone() as int[,];
    } 
    #endregion

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

    public override string ToString()
    {
        var result = new StringBuilder(capacity: _rows*_columns);

        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _columns; j++)
            {
                result.Append(_array[i, j] + " ");
            }

            if (i < _rows - 1) 
                result.Append("\n");
        }

        return result.ToString();
    }

    public bool EqualSize(Matrix otherMatrix) => _rows == otherMatrix._rows && _columns == otherMatrix._columns;

    #region Operators

    public static Matrix operator +(Matrix matrixA, Matrix matrixB)
    {
        if (!matrixA.EqualSize(matrixB))
            throw new Exception("Invalid operation! The matrices must be of equal size");

        var newMatrix = new Matrix(matrixA._rows, matrixA._columns);

        for (int i = 0; i < matrixA._rows; i++)
        {
            for (int j = 0; j < matrixA._columns; j++)
            {
                newMatrix[i, j] = matrixA[i, j] + matrixB[i, j];
            }
        }

        return newMatrix;
    }

    public static Matrix operator -(Matrix matrixA, Matrix matrixB)
    {
        if (!matrixA.EqualSize(matrixB))
            throw new Exception("Invalid operation! The matrices must be of equal size");

        var result = new Matrix(matrixA._rows, matrixA._columns);

        for (int i = 0; i < matrixA._rows; i++)
        {
            for (int j = 0; j < matrixA._columns; j++)
            {
                result[i, j] = matrixA[i, j] - matrixB[i, j];
            }
        }

        return result;
    }

    public static Matrix operator *(int value, Matrix matrix)
    {
        var result = new Matrix(matrix);

        for (int i = 0; i < result._rows; i++)
        {
            for (int j = 0; j < result._columns; j++)
            {
                result[i, j] *= value;
            }
        }

        return result;
    }

    public static Matrix operator *(Matrix matrix, int value)
    {
        return value * matrix;
    }

    public static Matrix operator *(Matrix matrixA, Matrix matrixB)
    {
        if (matrixA._columns != matrixB._rows)
            throw new Exception("Invalid operation!");

        var result = new Matrix(matrixA._rows, matrixB._columns);

        for (int i = 0; i < matrixA._rows; i++)
        {
            for (int j = 0; j < matrixB._columns; j++)
            {
                for (int k = 0; k < matrixA._columns; k++)
                {
                    result[i, j] += matrixA[i, k] * matrixB[k, j];
                }
            }
        }

        return result;
    } 
    #endregion
}
