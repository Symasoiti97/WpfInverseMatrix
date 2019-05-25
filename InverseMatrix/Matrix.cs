using System;

namespace InverseMatrix
{
    public class Matrix
    {
        private double[,] _matrix;

        public int N { get; set; }
        public int M { get; set; }
        
        public Matrix(int n, int m)
        {
            N = n;
            M = m;
            _matrix = new double[N, M];
        }

        public Matrix(double[,] matrix)
        {
            N = matrix.GetLength(0);
            M = matrix.GetLength(1);
            _matrix = matrix;
        }

        public double[,] GetMatrix()
        {
            return _matrix;
        }

        public double this[int n, int m]
        {
            get
            {
                return _matrix[n, m];
            }
            set
            {
                _matrix[n, m] = value;
            }
        }

        public static Matrix operator +(Matrix A, Matrix B)
        {
            Matrix C = new Matrix(A.N, A.M);

            for (int i = 0; i < C.N; i++)
            {
                for (int j = 0; j < C.M; j++)
                {
                    C[i, j] = A[i, j] + B[i, j];
                }
            }

            return C;
        }

        public static Matrix operator -(Matrix A, Matrix B)
        {
            Matrix C = new Matrix(A.N, A.M);

            for (int i = 0; i < C.N; i++)
            {
                for (int j = 0; j < C.M; j++)
                {
                    C[i, j] = A[i, j] - B[i, j];
                }
            }

            return C;
        }

        public static Matrix operator *(Matrix A, Matrix B)
        {
            if (A.M != B.N) throw new Exception("A.M != B.N");

            Matrix C = new Matrix(A.N, B.M);

            for (int i = 0; i < A.N; i++)
            {
                for (int j = 0; j < B.M; j++)
                {
                    C[i, j] = 0;
                    for (int k = 0; k < A.M; k++)
                    {
                        C[i, j] += A[i, k] * B[k, j];
                    }
                }
            }

            return C;
        }

        public static Matrix operator /(Matrix A, double b)
        {
            Matrix C = new Matrix(A.N, A.M);

            for (int i = 0; i < A.N; i++)
            {
                for (int j = 0; j < A.M; j++)
                {
                    C[i, j] = A[i, j] / b;
                }
            }

            return C;
        }

        public Matrix GetMinor(int row, int column)
        {
            Matrix newMatrix = new Matrix(_matrix.GetLength(0) - 1, _matrix.GetLength(1) - 1);
            int k = 0, l;

            for (int i = 0; i < _matrix.GetLength(0); i++)
            {
                l = 0;
                for (int j = 0; j < _matrix.GetLength(1); j++)
                {
                    if (!(i == row || j == column))
                    {
                        newMatrix[k, l] = _matrix[i, j];
                        l++;
                    }
                }

                if (l > 0)
                {
                    k++;
                }
            }

            return newMatrix;
        }

        public static Matrix GetTransMatrix(Matrix A)
        {
            Matrix B = new Matrix(A.M, A.N);

            for (int i = 0; i < B.N; i++)
            {
                for (int j = 0; j < B.M; j++)
                {
                    B[i, j] = A[j, i];
                }
            }

            return B;
        }

        public static double DetRec(Matrix matrix)
        {
            if (matrix.N == 2)
            {
                return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
            }

            double sign = 1, result = 0;

            for (int i = 0; i < matrix.M; i++)
            {
                Matrix minor = GetMinor(matrix, i);
                result += sign * matrix[0, i] * DetRec(minor);
                sign = -sign;
            }

            return result;
        }

        private static Matrix GetMinor(Matrix matrix, int n)
        {
            Matrix result = new Matrix(matrix.N - 1, matrix.N - 1);

            for (int i = 1; i < matrix.N; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    result[i - 1, j] = matrix[i, j];
                }

                for (int j = n + 1; j < matrix.N; j++)
                {
                    result[i - 1, j - 1] = matrix[i, j];
                }
            }

            return result;
        }

        public static Matrix MatrixE(int n, int m)
        {
            Matrix E = new Matrix(n, m);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (i == j)
                    {
                        E[i, j] = 1;
                    }
                    else
                    {
                        E[i, j] = 0;
                    }
                }
            }

            return E;
        }

        public static double NormaMatrix(Matrix matrix)
        {
            double norma = 0;

            for (int i = 0; i < matrix.N; i++)
            {
                for (int j = 0; j < matrix.M; j++)
                {
                    norma += Math.Pow(matrix[i, j], 2);
                }
            }

            return Math.Sqrt(norma);
        }

        public static Matrix GetAlgDopMatrix(Matrix A)
        {
            Matrix rezult = new Matrix(A.N, A.M);

            for (int i = 0; i < A.N; i++)
            {
                for (int j = 0; j < A.M; j++)
                {
                    rezult[i, j] = Math.Pow(-1, (i + 1) + (j + 1)) * DetRec(A.GetMinor(i, j));
                }
            }

            return rezult;
        }
    }
}
