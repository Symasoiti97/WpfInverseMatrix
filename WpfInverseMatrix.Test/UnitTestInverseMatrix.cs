using System;
using System.Windows;
using InverseMatrix;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WpfInverseMatrix.Test
{
    [TestClass]
    public class UnitTestInverseMatrix
    {
        [TestMethod]
        public void Test_MatrixAddition_NewMatrixReturned()
        {
            //arrange
            Matrix A = new Matrix(new double[,] { { 1, 1, 1 }, { 1, 1, 1 } });
            Matrix B = new Matrix(new double[,] { { 2, 2, 2 }, { 2, 2, 2 } });

            Matrix expected = new Matrix(new double[,] { { 3, 3, 3 }, { 3, 3, 3 } });

            //act
            Matrix actual = A + B;

            //assert 
            CollectionAssert.AreEqual(expected.GetMatrix(), actual.GetMatrix());
        }

        [TestMethod]
        public void Test_MatrixSubtraction_NewMatrixReturned()
        {
            //arrange
            Matrix A = new Matrix(new double[,] { { 1, 1, 1 }, { 1, 1, 1 } });
            Matrix B = new Matrix(new double[,] { { 2, 2, 2 }, { 2, 2, 2 } });

            Matrix expected = new Matrix(new double[,] { { -1, -1, -1 }, { -1, -1, -1 } });

            //act
            Matrix actual = A - B;

            //assert 
            CollectionAssert.AreEqual(expected.GetMatrix(), actual.GetMatrix());
        }

        [TestMethod]
        public void Test_MatrixMultiplication_NewMatrixReturned()
        {
            //arrange
            Matrix A = new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } });
            Matrix B = new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } });

            Matrix expected = new Matrix(new double[,] { { 30, 36, 42 }, { 66, 81, 96 }, { 102, 126, 150 } });

            //act
            Matrix actual = A * B;

            //assert 
            CollectionAssert.AreEqual(expected.GetMatrix(), actual.GetMatrix());
        }

        [TestMethod]
        public void Test_InverseMatrixSolution_NewMatrixReturned()
        {
            //arrange
            Matrix A = new Matrix(new double[,] { { 1, 2, 1 }, { 0, 1, 0 }, { 0, 2, 2 } });
            double eps = 0.01;
            int m = 1;

            //act
            Matrix actual = InverseMatrixCalculation.FindInverseMatrix(A, eps, m);

            string sol = "";

            for (int i = 0; i < actual.N; i++)
            {
                for (int j = 0; j < actual.M; j++)
                {
                    sol += actual[i, j] + " ";
                }
                sol += "\n";
            }

            //assert 
            MessageBox.Show(sol);
        }
    }
}
