using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InverseMatrix
{
    public static class InverseMatrixCalculation
    {
        public static Matrix FindInverseMatrix(Matrix A, double eps, int m)
        {
            if (DetRec(A) == 0)
            {
                MessageBox.Show("Обратная матрица не может быть найдена.\nОпределитель равен нулю", "Упс", MessageBoxButton.OK, MessageBoxImage.Warning);
                return A;
            }

            while (true)
            {
                List<Matrix> ListU = new List<Matrix>();
                List<Matrix> ListFi = new List<Matrix>();
                ListU.Add(new Matrix(A.N, A.M).InitMatrixU1(A));
                Matrix E = MatrixE(A.N, A.M);
                int k = -1;
                bool flag = true;

                do
                {
                    k++;

                    ListFi.Add(E - A * ListU[k]);

                    if (NormaMatrix(ListFi[k]) < eps)
                    {
                        break;
                    }

                    if (double.IsNaN(ListU[k][0, 0]))
                    {
                        flag = false;
                        break;
                    }

                    ListU.Add(ListU[k] * (E + ListFi[k]));
                }
                while (true);

                if(flag == true) return ListU[k];
            }
            
        }

        private static Matrix InitMatrixU(this Matrix U, Matrix A)
        {
            Random rand = new Random();

            double maxOwn = MaxOwnValue(OwnValues(A));

            int ownValue = Convert.ToInt16(maxOwn);

            if (ownValue == 0) ownValue = 1;

            int[] signs = new int[2] { -1, 1 };

            for (int i = 0; i < U.N; i++)
            {
                for (int j = 0; j < U.M; j++)
                {
                    do
                    {
                        U[i, j] = Math.Round(rand.NextDouble() * signs[rand.Next(0, 2)], 2);
                    }
                    while (Math.Abs(U[i, j]) > maxOwn && U[i,j] != 0);
                }
            }

            return U; //new Matrix(new double[,] { { 0.6, -0.6, -0.4 }, { 0.1, 0.3, 0.2 }, { 0.1, -0.5, 0.5 } });
        }

        private static Matrix InitMatrixU1(this Matrix U, Matrix A)
        {
            U = new Matrix(GetTransMatrix(A).GetMatrix());

            double maxOwn = OwnMaxValue(A * U);

            maxOwn = 2 / maxOwn / 2;

            for (int i = 0; i < U.N; i++)
            {
                for (int j = 0; j < U.M; j++)
                {
                    U[i, j] *= maxOwn;
                }
            }

            return U;
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

        public static double MaxOwnValue(double[] vector)
        {
            for (int i = 0; i < vector.Length; i++)
            {
                vector[i] = Math.Abs(vector[i]);
            }

            return vector.Max();
        }

        public static double[] OwnValues(Matrix A)
        {
            int n = A.N;
            double[] w0 = new double[n];
            double[] w = new double[n];
            double[] w0norm = new double[n];
            double summ = 0, e, d, d0;

            int i, j;

            for (i = 0; i < n; i++)
            {
                w0[i] = 0;
            }

            w0[0] = 1;

            do
            {
                for (i = 0; i < n; i++)
                {
                    summ = summ + w0[i] * w0[i];
                }

                d0 = Math.Sqrt(summ);

                for (i = 0; i < n; i++)
                {
                    w0norm[i] = w0[i] / d0;
                }

                for (i = 0; i < n; i++)
                {
                    w[i] = 0;

                    for (j = 0; j < n; j++)
                    {
                        w[i] = w[i] + A[i, j] * w0norm[j];
                    }
                }

                summ = 0;

                for (i = 0; i < n; i++)
                {
                    summ = summ + w[i] * w[i];
                }

                d = Math.Sqrt(summ);
                e = Math.Abs(d - d0);

                for (i = 0; i < n; i++)
                {
                    w0[i] = w[i];
                }
                summ = 0;
            }
            while (e > 0.001);

            return w0norm;
        }

        public static double OwnMaxValue(Matrix A)
        {
            double Eps = 0.1;
            List<double> lambda = new List<double>();
            List<Matrix> X = new List<Matrix>();
            X.Add(new Matrix(A.N, 1));
            int k = 0;

            for (int i = 0; i < X[0].N; i++)
            {
                X[0][i, 0] = 1; 
            }

            X.Add(A * X[k]);
            k = 1;
            lambda.Add(X[k][0, 0] / X[k-1][0, 0]);


            do
            {
                X.Add(A * X[k]);
                k++;
                lambda.Add(X[k][0, 0] / X[k-1][0, 0]);
            }
            while (Math.Abs(lambda[k-1] - lambda[k - 2]) > Eps);

            return lambda[k-1];
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
    }
}
