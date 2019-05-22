using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InverseMatrix.SolutionInverseMatrix
{
    public class AlgComplementMethod
    {
        public static Matrix GetInverseMatrix(Matrix A)
        {
            Matrix inverseMatrix = new Matrix(A.N, A.M);
            double detA = Matrix.DetRec(A);

            if (detA == 0)
            {
                MessageBox.Show("Обратная матрица не может быть найдена.\nОпределитель равен нулю", "Упс", MessageBoxButton.OK, MessageBoxImage.Warning);
                return A;
            }

            inverseMatrix = Matrix.GetAlgDopMatrix(A);

            inverseMatrix = Matrix.GetTransMatrix(inverseMatrix);

            inverseMatrix /= detA;

            return inverseMatrix;
        }
    }
}
