using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace InverseMatrix.SolutionInverseMatrix
{
    public class SchulzIterationMethod
    {
        public static Matrix GetInverseMatrix(Matrix A, double eps)
        {
            if (Matrix.DetRec(A) == 0)
            {
                throw new Exception("Обратная матрица не может быть найдена.\nОпределитель равен нулю");
            }

            while (true)
            {
                List<Matrix> ListU = new List<Matrix>();
                List<Matrix> ListFi = new List<Matrix>();
                ListU.Add(InitMatrixU(A));
                Matrix E = Matrix.MatrixE(A.N, A.M);
                int k = -1;
                bool flag = true;

                do
                {
                    k++;

                    ListFi.Add(E - A * ListU[k]);

                    if (Matrix.NormaMatrix(ListFi[k]) < eps)
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

                if (flag == true) return ListU[k];
            }

        }

        private static Matrix InitMatrixU(Matrix A)
        {
            Matrix U = new Matrix(Matrix.GetTransMatrix(A).GetMatrix());

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

        private static double MaxOwnValue(double[] vector)
        {
            for (int i = 0; i < vector.Length; i++)
            {
                vector[i] = Math.Abs(vector[i]);
            }

            return vector.Max();
        }

        private static double OwnMaxValue(Matrix A)
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
            lambda.Add(X[k][0, 0] / X[k - 1][0, 0]);

            do
            {
                X.Add(A * X[k]);
                k++;
                lambda.Add(X[k][0, 0] / X[k - 1][0, 0]);
            }
            while (Math.Abs(lambda[k - 1] - lambda[k - 2]) > Eps);

            return lambda[k - 1];
        }
    }
}
