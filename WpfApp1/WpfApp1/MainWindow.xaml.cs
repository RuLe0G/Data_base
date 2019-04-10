using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;


namespace Data_base
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>


    public class CTest { public int Number { get; set; } public string FIO { get; set; } public int Phy { get; set; } public int Math { get; set; } }


    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        string db_name;
        SQLiteConnection m_dbConnection;

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.ShowDialog();

            string db_name = dlg.FileName;

            m_dbConnection = new SQLiteConnection("Data Source=" + db_name + ";Version=3;");
            m_dbConnection.Open();

            string sql = "SELECT * FROM Tabl ORDER BY Number";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var data = new CTest { Number = int.Parse(reader["Number"].ToString()), FIO = reader["FIO"].ToString(), Phy = int.Parse(reader["Phy"].ToString()), Math = int.Parse(reader["Math"].ToString()) };
                datgrid.Items.Add(data);
            }
            AddBut.IsEnabled = true;
            Numb.IsEnabled = true;
            FIO.IsEnabled = true;
            Math.IsEnabled = true;
            Phy.IsEnabled = true;
            datgrid.IsEnabled = true;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            string sql = "INSERT INTO Tabl (Number,FIO,Phy,Math) VALUES (" + Numb.Text.ToString() + "," + FIO.Text.ToString() + "," + Phy.Text.ToString() + "," + Math.Text.ToString() + ")";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            var data = new CTest { Number = int.Parse(Numb.Text.ToString()), FIO = FIO.Text.ToString(), Phy = int.Parse(Phy.Text.ToString()), Math = int.Parse(Math.Text.ToString()) };
            datgrid.Items.Add(data);
            command.ExecuteNonQuery();
            Numb.Text = "";
            FIO.Text = "";
            Phy.Text = "";
            Math.Text = "";
            //string sql = "SELECT Number FROM Table ORDER BY int_field DESC LIMIT 1";


        }

        private void datgrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Del.IsEnabled = true;
            Numb_Copy.IsEnabled = true;
            FIO_Copy.IsEnabled = true;
            Math_Copy.IsEnabled = true;
            Phy_Copy.IsEnabled = true;
            upd.IsEnabled = true;

        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            CTest DabGr = (CTest)datgrid.SelectedItem;
            string sql = ("DELETE FROM Tabl WHERE(Number=" + DabGr.Number.ToString() + " AND FIO = '" + DabGr.FIO.ToString() + "'" + " AND Phy = " + DabGr.Phy.ToString() + " And Math = " + DabGr.Math.ToString() + ")");
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            datgrid.Items.RemoveAt(int.Parse (datgrid.SelectedIndex.ToString()));
        }

        private void upd_Click(object sender, RoutedEventArgs e)
        {
            CTest DabGr = (CTest)datgrid.SelectedItem;
            string sql =("UPDATE Tabl SET Number=" + int.Parse(Numb_Copy.Text.ToString()) + " WHERE Number=" + DabGr.Number.ToString());
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            string sql1 = ("UPDATE Tabl SET FIO=" + FIO_Copy.Text.ToString() + " WHERE FIO=" + DabGr.FIO.ToString());
            SQLiteCommand command1 = new SQLiteCommand(sql1, m_dbConnection);
            string sql2 = ("UPDATE Tabl SET Phy=" + int.Parse(Phy_Copy.Text.ToString()) + " WHERE Phy=" + DabGr.Phy.ToString());
            SQLiteCommand command2 = new SQLiteCommand(sql2, m_dbConnection);
            string sql3 = ("UPDATE Tabl SET Math=" + int.Parse(Math_Copy.Text.ToString()) + " WHERE Math=" + DabGr.Math.ToString());
            SQLiteCommand command3 = new SQLiteCommand(sql3, m_dbConnection);
            command.ExecuteNonQuery(); command1.ExecuteNonQuery(); command2.ExecuteNonQuery(); command3.ExecuteNonQuery();
        }
    }
    }

