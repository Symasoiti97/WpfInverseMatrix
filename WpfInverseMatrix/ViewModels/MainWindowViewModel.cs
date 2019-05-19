using DevExpress.Mvvm;
using InverseMatrix;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfInverseMatrix.Models;

namespace WpfInverseMatrix.ViewModels
{
    class MainWindowViewModel : BaseViewModel
    {
        private string _n;

        public MainWindowViewModel()
        {
            N = "3";
            Eps = "0,01";
            M = "1";

            using (DataTable dt = new DataTable())
            {
                dt.Columns.Add($"1");
                dt.Columns.Add($"2");
                dt.Columns.Add($"3");
                dt.Rows.Add(1, 2, 1);
                dt.Rows.Add(0, 1, 0);
                dt.Rows.Add(0, 2, 2);

                DataGridMain = dt.DefaultView;
            }
        }

        public string N
        {
            get
            {
                return _n;
            }
            set
            {
                _n = value;
                DataGridMain = SetDataGridMain(int.Parse(N), int.Parse(N)).DefaultView;
            }
        }

        public string Eps { get; set; }

        public string M { get; set; }

        public DataView DataGridMain { get; set; }

        public DataView DataGridSolution { get; set; }

        public ICommand Solution_Click
        {
            get
            {
                return new DelegateCommand(()=>
                {
                    Matrix A = new Matrix(GetDataGridMain());
                    Matrix U = InverseMatrixCalculation.FindInverseMatrix(A, double.Parse(Eps), int.Parse(M));
                    DataGridSolution = SetDataGridSolution(U.GetMatrix()).DefaultView;
                });
            }
        }

        private DataTable SetDataGridMain(int n, int m)
        {
            using (DataTable dt = new DataTable())
            {

                for (int i = 0; i < n; i++)
                {
                    dt.Columns.Add($"{i + 1}");
                }

                for (int j = 0; j < m; j++)
                {
                    dt.Rows.Add();
                }

                return dt;
            }
        }

        private double[,] GetDataGridMain()
        {
            using (DataTable dt = DataGridMain.Table)
            {
                double[,] matrix = new double[int.Parse(N), int.Parse(N)];

                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        matrix[i, j] = Convert.ToDouble(dt.Rows[i][j]);
                    }
                }

                return matrix;
            }
        }

        private DataTable SetDataGridSolution(double[,] matrix)
        {
            using (DataTable dt = new DataTable())
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    dt.Columns.Add($"{i+1}");
                }

                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    string[] mat = new string[matrix.GetLength(1)];
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        mat[j] = "" + Math.Round(matrix[i, j], 3);
                    }
                    dt.Rows.Add(mat);
                }

                return dt;
            }
        }
    }
}
