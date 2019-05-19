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
            //Matrix B = new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } });
            Matrix B = new Matrix(new double[,] { { 1 }, { 4 }, { 1 } });

            Matrix expected = new Matrix(new double[,] { { 30, 36, 42 }, { 66, 81, 96 }, { 102, 126, 150 } });

            //act
            Matrix actual = A * B;
            //assert 
            CollectionAssert.AreEqual(expected.GetMatrix(), actual.GetMatrix());
        }

        [TestMethod]
        public void Test_Matrix_DetMatrixReturned()
        {
            //arrange
            Matrix A = new Matrix(new double[,] { { 1, -2, 3 }, { 4, 0, 6 }, { -7, 8, 9 } });

            double expected = 204;

            //act
            double actual = InverseMatrixCalculation.DetRec(A);

            //assert 
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_MatrixAndSizeMatrix_MaxOwnValueReturned()
        {
            //arrange
            //Matrix A = new Matrix(new double[,] { { 1, 2, 1 }, { 0, 2, 2 }, { 0, 1, 2 } });
            Matrix A = new Matrix(new double[,] { { 1, 2, 1 }, { 0, 1, 0 }, { 0, 2, 2 } });

            var expected = new double[] {18,6,3};

            //act
            var actual = InverseMatrixCalculation.OwnValues(A);
            var actualOwn = InverseMatrixCalculation.MaxOwnValue(actual);

            //assert 
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_FindOwnMaxValue_OwnMaxValueReturned()
        {
            //arrange
            Matrix A = new Matrix(new double[,] { { 5, 1, 2 }, { 1, 4, 1 }, { 2, 1, 3 } });

            double expected = 6.9559;

            //act
            double actual = Math.Round(InverseMatrixCalculation.OwnMaxValue(A),4);

            //assert 
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_GetTransMatrix_MatrixReturned()
        {
            //arrange
            Matrix A = new Matrix(new double[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } });

            Matrix expected = new Matrix(new double[,] { { 1, 3, 5 }, { 2, 4, 6 } });

            //act
            Matrix actual = InverseMatrixCalculation.GetTransMatrix(A);

            //assert 
            CollectionAssert.AreEqual(expected.GetMatrix(), actual.GetMatrix());
        }

        [TestMethod]
        public void Test_InverseMatrixSolution_NewMatrixReturned()
        {
            //arrange
            Matrix A = new Matrix(new double[,] { { 1, 2, 1 }, { 0, 1, 0 }, { 0, 2, 2 } });
            //Matrix A = new Matrix(new double[,] { { 1, 2, 1 }, { 0, 2, 2 }, { 0, 1, 2 } });
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
