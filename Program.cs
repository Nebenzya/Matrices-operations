using Matrices_operations;

var m = new Matrix(5,3);
m.FillRandomValue(0, 10);

var n = new Matrix(m.Transpose());

var k = new Matrix(n.Rows, n.Columns);
k.FillRandomValue(1, 5);

Console.WriteLine(2*m*(n-k));

Console.ReadLine();