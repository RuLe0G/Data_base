﻿using System;
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
            try
            {
                string sql = "INSERT INTO Tabl (Number,FIO,Phy,Math) VALUES (" + Numb.Text.ToString() + ",'" + FIO.Text.ToString() + "'," + Phy.Text.ToString() + "," + Math.Text.ToString() + ")";
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
            catch
            {
                Err.Content = "ОШИБКА: Такой номер (" + Numb.Text.ToString() + ") уже существует";
            }
        }

        private void datgrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datgrid.SelectedIndex > -1)
            {
                CTest DaT = (CTest)datgrid.SelectedItem;
                Err.Content = "";
                Math_Copy.Text = DaT.Math.ToString(); Phy_Copy.Text = DaT.Phy.ToString(); FIO_Copy.Text = DaT.FIO.ToString(); Numb_Copy.Text = DaT.Number.ToString();

                Del.IsEnabled = true;
                Numb_Copy.IsEnabled = true;
                FIO_Copy.IsEnabled = true;
                Math_Copy.IsEnabled = true;
                Phy_Copy.IsEnabled = true;
                upd.IsEnabled = true;
            }
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            CTest DabGr = (CTest)datgrid.SelectedItem;
            string sql = ("DELETE FROM Tabl WHERE(Number=" + DabGr.Number.ToString() + " AND FIO = '" + DabGr.FIO.ToString() + "'" + " AND Phy = " + DabGr.Phy.ToString() + " And Math = " + DabGr.Math.ToString() + ")");
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            datgrid.Items.RemoveAt(int.Parse (datgrid.SelectedIndex.ToString()));
            Del.IsEnabled = false;
            Numb_Copy.IsEnabled = false;
            FIO_Copy.IsEnabled = false;
            Math_Copy.IsEnabled = false;
            Phy_Copy.IsEnabled = false;
            upd.IsEnabled = false;
        }

        private void upd_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                int index = int.Parse(datgrid.SelectedIndex.ToString());
                CTest DabGr = (CTest)datgrid.SelectedItem;
                var data = new CTest { Number = int.Parse(Numb_Copy.Text.ToString()), FIO = FIO_Copy.Text.ToString(), Phy = int.Parse(Phy_Copy.Text.ToString()), Math = int.Parse(Math_Copy.Text.ToString()) };
                string sql = ("UPDATE Tabl SET Number=" + int.Parse(Numb_Copy.Text.ToString()) + ", FIO= '" + FIO_Copy.Text.ToString() + "', Phy=" + int.Parse(Phy_Copy.Text.ToString()) + ", Math=" + int.Parse(Math_Copy.Text.ToString()) + " WHERE Number = " + DabGr.Number.ToString());
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
                datgrid.Items.RemoveAt(index);
                datgrid.Items.Insert(index, data);
            }
            catch 
            {
                Err.Content = "ОШИБКА: Такой номер (" + Numb_Copy.Text.ToString() + ") уже существует";
                datgrid.SelectedIndex = -1;
            }
            Math_Copy.Text = "";  Phy_Copy.Text = ""; FIO_Copy.Text = ""; Numb_Copy.Text = "";
            Del.IsEnabled = false;
            Numb_Copy.IsEnabled = false;
            FIO_Copy.IsEnabled = false;
            Math_Copy.IsEnabled = false;
            Phy_Copy.IsEnabled = false;
            upd.IsEnabled = false;
        }
    }
}

