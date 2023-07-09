using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace MatrixLib;

public partial class Matrix
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

    public Matrix(int[,] array)
    {
        if (array == null)
            throw new NullReferenceException();

        _array = array.Clone() as int[,];
        _rows = array.GetLength(0);
        _columns = array.GetLength(1);
    }



    /// <summary>
    /// Deep copy constructor
    /// </summary>
    public Matrix(Matrix previousMatrix)
    {
        _rows = previousMatrix._rows;
        _columns = previousMatrix._columns;
        _array = previousMatrix?._array?.Clone() as int[,];
    }
    #endregion

    #region Operators

    public static Matrix operator +(Matrix matrixA, Matrix matrixB)
    {
        if (!matrixA.EqualSize(matrixB))
            throw new InvalidOperationException("Matrix shapes do not match!");

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
            throw new InvalidOperationException("Matrix shapes do not match!");

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

    public static Matrix operator *(Matrix matrix, int[] vector)
    {
        if (matrix._columns != vector.Length)
            throw new InvalidOperationException();

        var result = new Matrix(vector.Length, 1);

        for (int i = 0; i < result._rows; i++)
        {
            for (int j = 0; j < vector.Length; j++)
            {
                result[i, 0] += matrix[i, j] * vector[j];
            }
        }

        return result;
    }

    public static Matrix operator *(Matrix matrixA, Matrix matrixB)
    {
        if (matrixA._columns != matrixB._rows)
            throw new InvalidOperationException();

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

    public static bool operator ==(Matrix matrixA, Matrix matrixB) => Equals(matrixA, matrixB);

    public static bool operator !=(Matrix matrixA, Matrix matrixB) => !Equals(matrixA, matrixB);
    #endregion
    #region Override

    public override bool Equals(object? obj)
    {
        var otherMatrix = obj as Matrix;

        if (!EqualSize(otherMatrix))
            return false;

        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _columns; j++)
            {
                if (this[i, j] != otherMatrix[i, j])
                    return false;
            }
        }

        return true;
    }

    public override int GetHashCode()
    {
        return _array.GetHashCode();
    }

    public override string ToString()
    {
        var result = new StringBuilder(capacity: _rows * _columns);

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
    #endregion
}
