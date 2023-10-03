using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Lab4_RPVSREVORK
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataTable dt;
        
        public MainWindow()
        {
            InitializeComponent();

            tbRow.Text = "3";
            tbCol.Text = "4";

            FillTable(4,3);

        }

        private void btFintUniqueElem_Click(object sender, RoutedEventArgs e)
        {
            //showMass.Columns[1].CellStyle = (Style)showMass.Resources["CpecialKey"];
            ResultSpecialEl.Text = CountCpecial().ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(dt.Rows[0][0].ToString());
            try
            {
                int countRow = int.Parse(tbRow.Text);
                int countCol = int.Parse(tbCol.Text);

                FillTable(countCol, countRow);
            }
            catch (FormatException exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private int SumRow(int index)
        {
            int sum = 0;
            for(int i=1;i<dt.Columns.Count;i++)
            {
                sum += (int)dt.Rows[index][i];
            }

            return sum;
        }

        private int CountCpecial()
        {
            //List<int> collection = dt.Rows as List<int>;

            int count = 0;

            for(int i=1;i<dt.Rows.Count;i++)
            {
                for(int j=1;j<dt.Columns.Count;j++)
                {
                    if ((int)dt.Rows[i][j] > SumRow(i) - (int)dt.Rows[i][j])
                    {
                        count++;
                    }
                }
            }


            return count;
        }

        private void FillTable(int col,int row)
        {
            dt = new DataTable();

            dt.Columns.Add(new DataColumn("----", typeof(string)));
            dt.Columns[0].ReadOnly = true;
            for (int i = 1; i < col+1; i++)
            {
                dt.Columns.Add(new DataColumn($"Колонка {i + 1}", typeof(int)));
            }

            Random random = new Random();
            for (int j = 0; j < row; j++)
            {
                DataRow dataRow = dt.NewRow();
                dataRow[0] = $"Строка {j +1}";
                
                for (int i = 1; i < col+1; i++)
                {
                    dataRow[i] = random.Next(0,10);
                }
                dt.Rows.Add(dataRow);
            }
            
            showMass.ItemsSource = dt.DefaultView;
        }

        private void showMass_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int indexRow = showMass.SelectedIndex;
            int indexCol = showMass.CurrentColumn.DisplayIndex;

            MessageBox.Show($"Значение {dt.Rows[indexRow][indexCol]} строка {indexRow} столбец {indexCol}");
        }
    }
}
