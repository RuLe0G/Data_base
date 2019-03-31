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

        
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            
            dlg.ShowDialog();

            string db_name = dlg.FileName;

            SQLiteConnection m_dbConnection; m_dbConnection = new SQLiteConnection("Data Source=" + db_name + ";Version=3;");
            m_dbConnection.Open();

            string sql = "SELECT * FROM Tabl ORDER BY Number";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            var data = new CTest { Number = int.Parse(reader["Number"].ToString()), FIO = reader["FIO"].ToString(), Phy = int.Parse(reader["Phy"].ToString()), Math = int.Parse(reader["Math"].ToString()) };
            datgrid.Items.Add(data);
            m_dbConnection.Close(); 
        }
    }
}
