using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (A.N != B.M) throw new Exception("A.N != B.N");

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
    }
}
