using System;

namespace InverseMatrix.SolutionInverseMatrix
{
    public class GaussMethod
    {
        public static Matrix GetInverseMatrix(Matrix A)
        {
            if (Matrix.DetRec(A) == 0)
            {
                throw new Exception("Обратная матрица не может быть найдена.\nОпределитель равен нулю");
            }

            Matrix B = new Matrix(A.GetMatrix());
            Matrix E = Matrix.MatrixE(B.N, B.M);
            ChangeRow(B, E); // проверка [0,0] на ноль(смена строк)

            //прямой ход
            for (int i = 0; i < B.N; i++)
            {
                double c = B[i, i]; // разрещающий элемент

                for (int j = 0; j < B.N; j++)
                {
                    if (c != 0)
                    {
                        B[i, j] /= c;
                        E[i, j] /= c;
                    }
                    else
                    {
                        //throw new Exception("Деление на ноль");
                    }
                }

                for (int k = i+1; k < B.N; k++)
                {
                    double d = B[k, i];

                    for (int h = 0; h < B.N; h++)
                    {
                        B[k, h] -= B[i, h] * d;
                        E[k, h] -= E[i, h] * d;
                    }
                }
            }

            //обратный ход
            for (int i = B.N - 1; i >= 0; i--)
            {
                for (int k = i - 1; k >= 0; k--)
                {
                    double d = B[k, i];

                    for (int h = B.N - 1; h >= 0; h--)
                    {
                        B[k, h] -= B[i, h] * d;
                        E[k, h] -= E[i, h] * d;
                    }
                }
            }

            return E;
        }

        private static void ChangeRow(Matrix A, Matrix B)
        {
            int i = 0;
            double c;

            while (A[0, 0] == 0)
            {
                i++;
                for (int j = 0; j < A.N; j++)
                {
                    c = A[0, j];
                    A[0, j] = A[i, j];
                    A[i, j] = c;

                    c = B[0, j];
                    B[0, j] = B[i, j];
                    B[i, j] = c;
                }
            }
        }
    }
}
