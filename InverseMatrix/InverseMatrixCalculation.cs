using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InverseMatrix
{
    public static class InverseMatrixCalculation
    {
        public static Matrix FindInverseMatrix(Matrix A, double eps, int m)
        {
            List<Matrix> ListU = new List<Matrix>();
            List<Matrix> ListFi = new List<Matrix>();
            ListU.Add(new Matrix(A.N, A.M).InitMatrixU());
            Matrix E = MatrixE(A.N, A.M);
            int k = -1;

            do
            {
                k++;

                ListFi.Add(E - A * ListU[k]);

                if (NormaMatrix(ListFi[k]) < eps)
                {
                    break;
                }

                ListU.Add(ListU[k] * (E + ListFi[k]));
            }
            while (true);

            return ListU[k];
        }

        private static Matrix InitMatrixU(this Matrix U)
        {
            Random rand = new Random();

            for (int i = 0; i < U.N; i++)
            {
                for (int j = 0; j < U.M; j++)
                {
                    U[i, j] = rand.NextDouble() * 10 * rand.Next(-1, 2);
                }
            }

            return new Matrix(new double[,] { { 0.6, -0.6, -0.4 }, { 0.1, 0.6, 0.2 }, { 0.1, -0.5, 0.5 } });
        }

        private static Matrix MatrixE(int n, int m)
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

        private static double NormaMatrix(Matrix matrix)
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
    }
}
