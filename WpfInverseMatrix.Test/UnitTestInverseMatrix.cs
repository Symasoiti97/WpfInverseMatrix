using System;
using System.Windows;
using InverseMatrix;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InverseMatrix.SolutionInverseMatrix;

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
        public void Test_Matrix_DetMatrixReturned()
        {
            //arrange
            Matrix A = new Matrix(new double[,] { { 1, -2, 3 }, { 4, 0, 6 }, { -7, 8, 9 } });

            double expected = 204;

            //act
            double actual = Matrix.DetRec(A);

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
            Matrix actual = Matrix.GetTransMatrix(A);

            //assert 
            CollectionAssert.AreEqual(expected.GetMatrix(), actual.GetMatrix());
        }

        [TestMethod]
        public void Test_CrossOutColumnAndRowinMatrix_MatrixReturned()
        {
            //arrange
            Matrix A = new Matrix(new double[,] { { 1, 2, 1 }, { 0, 1, 0 }, { 0, 2, 2 } });

            Matrix expected = new Matrix(new double[,] { { 1, 1 }, { 0, 2 } });

            //act
            Matrix actual = A.GetMinor(1, 1);

            //assert 
            CollectionAssert.AreEqual(expected.GetMatrix(), actual.GetMatrix());
        }

        [TestMethod]
        public void Test_GaussMethod_InverseMatrixReturned()
        {
            //arrange
            Matrix A = new Matrix(new double[,] { { 1, 2, 1 }, { 0, 1, 0 }, { 0, 2, 2 } });

            Matrix expected = new Matrix(new double[,] { { 1, -1, -0.5 }, { 0, 1, 0 }, { 0, -1, 0.5 } });

            //act
            Matrix actual = GaussMethod.GetInverseMatrix(A);

            //assert 
            CollectionAssert.AreEqual(expected.GetMatrix(), actual.GetMatrix());
        }

        [TestMethod]
        public void Test_AlgComplementMethod_InverseMatrixReturned()
        {
            //arrange
            Matrix A = new Matrix(new double[,] { { 1, 2, 1 }, { 0, 1, 0 }, { 0, 2, 2 } });

            Matrix expected = new Matrix(new double[,] { { 1, -1, -0.5 }, { 0, 1, 0 }, { 0, -1, 0.5 } });

            //actr
            Matrix actual = AlgComplementMethod.GetInverseMatrix(A);

            //assert 
            CollectionAssert.AreEqual(expected.GetMatrix(), actual.GetMatrix());
        }
    }
}
