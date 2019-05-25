using System;
using System.Windows;

namespace InverseMatrix.SolutionInverseMatrix
{
    public class AlgComplementMethod
    {
        public static Matrix GetInverseMatrix(Matrix A)
        {
            Matrix inverseMatrix = new Matrix(A.N, A.M);
            double detA = Matrix.DetRec(A);

            if (Matrix.DetRec(A) == 0)
            {
                throw new Exception("Обратная матрица не может быть найдена.\nОпределитель равен нулю");
            }

            inverseMatrix = Matrix.GetAlgDopMatrix(A);

            inverseMatrix = Matrix.GetTransMatrix(inverseMatrix);

            inverseMatrix /= detA;

            return inverseMatrix;
        }
    }
}
