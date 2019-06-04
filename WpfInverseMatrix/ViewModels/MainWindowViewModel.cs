using DevExpress.Mvvm;
using InverseMatrix;
using System;
using System.Data;
using System.Windows.Input;
using InverseMatrix.SolutionInverseMatrix;
using System.Diagnostics;
using System.Windows;
using System.Threading.Tasks;

namespace WpfInverseMatrix.ViewModels
{
    class MainWindowViewModel : BaseViewModel
    {
        private string _n;

        public MainWindowViewModel()
        {
            N = "3";
            Eps = "0,01";
            Digits = 3;

            Method1 = "Метод Шульца";
            Method2 = "Метод Гаусса";
            Method3 = "Метод алгебр. дополнения";

            using (DataTable dt = new DataTable())
            {
                dt.Columns.Add($"0");
                dt.Columns.Add($"1");
                dt.Columns.Add($"2");
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
                if (int.Parse(value) <= 30 && int.Parse(value) >= 2)
                {
                    _n = value;
                    Task.Run(() => DataGridMain = SetDataGridMain(int.Parse(N), int.Parse(N)).DefaultView);
                }
                else
                {
                    throw new Exception("Порядок матрицы не должен быть меньше 2 и превышать 30");
                }
            }
        }

        public string Eps { get; set; }

        public int Digits { get; set; }

        public DataView DataGridMain { get; set; }

        public DataView DataGridSolution1 { get; set; }
        public DataView DataGridSolution2 { get; set; }
        public DataView DataGridSolution3 { get; set; }

        public string Method1 { get; set; }
        public string Method2 { get; set; }
        public string Method3 { get; set; }

        public string Time1 { get; set; }
        public string Time2 { get; set; }
        public string Time3 { get; set; }

        public ICommand Solution_Click
        {
            get
            {
                return new DelegateCommand(()=>
                {
                    try
                    {
                        Stopwatch watch;

                        Matrix A = new Matrix(GetDataGridMain());
                        watch = Stopwatch.StartNew();
                        Matrix U1 = SchulzIterationMethod.GetInverseMatrix(new Matrix(A.GetMatrix()), double.Parse(Eps));
                        Time1 = $"Время {watch.ElapsedMilliseconds} мс";

                        A = new Matrix(GetDataGridMain());
                        watch = Stopwatch.StartNew();
                        Matrix U2 = GaussMethod.GetInverseMatrix(new Matrix(A.GetMatrix()));
                        Time2 = $"Время {watch.ElapsedMilliseconds} мс";

                        A = new Matrix(GetDataGridMain());
                        watch = Stopwatch.StartNew();
                        Matrix U3 = AlgComplementMethod.GetInverseMatrix(new Matrix(A.GetMatrix()));
                        Time3 = $"Время {watch.ElapsedMilliseconds} мс";

                        watch.Stop();

                        DataGridSolution1 = SetDataGridSolution(U1.GetMatrix()).DefaultView;
                        DataGridSolution2 = SetDataGridSolution(U2.GetMatrix()).DefaultView;
                        DataGridSolution3 = SetDataGridSolution(U3.GetMatrix()).DefaultView;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{ex.Message}", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                });
            }
        }

        private DataTable SetDataGridMain(int n, int m)
        {
            using (DataTable dt = new DataTable())
            {

                for (int i = 0; i < n; i++)
                {
                    dt.Columns.Add($"{i}");
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
                        mat[j] = "" + Math.Round(matrix[i, j], Digits);
                    }
                    dt.Rows.Add(mat);
                }

                return dt;
            }
        }
    }
}
