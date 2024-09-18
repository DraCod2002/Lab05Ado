using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Lab05
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string cadena = "Server=LAB1507-02\\SQLEXPRESS03;Initial Catalog=NeptunoB;User ID=user02;Password=123456;";
                using (SqlConnection connection = new SqlConnection(cadena))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("USP_ListarEmpleados", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridEmpleados.ItemsSource = dataTable.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            CrearEmpleadoWindow crearEmpleadoWindow = new CrearEmpleadoWindow();
            crearEmpleadoWindow.ShowDialog();
        }
      
        private void DataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            
        }
    }
}