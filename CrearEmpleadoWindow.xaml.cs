using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Lab05
{
    public partial class CrearEmpleadoWindow : Window
    {
        public CrearEmpleadoWindow()
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

                    // Verificar si el ID ya existe
                    SqlCommand checkIdCommand = new SqlCommand("SELECT COUNT(*) FROM Empleados WHERE IdEmpleado = @IdEmpleado", connection);
                    checkIdCommand.Parameters.AddWithValue("@IdEmpleado", Convert.ToInt32(txtIdEmpleado.Text));

                    int count = (int)checkIdCommand.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("ID existente. Por favor, ingrese otro ID.");
                        return;
                    }

                    // Insertar nuevo empleado
                    SqlCommand insertCommand = new SqlCommand("USP_InsertarEmpleado", connection);
                    insertCommand.CommandType = CommandType.StoredProcedure;

                    insertCommand.Parameters.AddWithValue("@IdEmpleado", Convert.ToInt32(txtIdEmpleado.Text));
                    insertCommand.Parameters.AddWithValue("@Apellidos", txtApellidos.Text);
                    insertCommand.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                    insertCommand.Parameters.AddWithValue("@Cargo", txtCargo.Text);
                    insertCommand.Parameters.AddWithValue("@Tratamiento", txtTratamiento.Text);
                    insertCommand.Parameters.AddWithValue("@FechaNacimiento", dpFechaNacimiento.SelectedDate);
                    insertCommand.Parameters.AddWithValue("@FechaContratacion", dpFechaContratacion.SelectedDate);
                    insertCommand.Parameters.AddWithValue("@Direccion", txtDireccion.Text);
                    insertCommand.Parameters.AddWithValue("@Ciudad", txtCiudad.Text);
                    insertCommand.Parameters.AddWithValue("@Region", txtRegion.Text);
                    insertCommand.Parameters.AddWithValue("@CodPostal", txtCodigoPostal.Text);
                    insertCommand.Parameters.AddWithValue("@Pais", txtPais.Text);
                    insertCommand.Parameters.AddWithValue("@TelDomicilio", txtTelDomicilio.Text);
                    insertCommand.Parameters.AddWithValue("@Extension", txtExtension.Text);
                    insertCommand.Parameters.AddWithValue("@Notas", txtNotas.Text);

                    insertCommand.ExecuteNonQuery();

                    MessageBox.Show("Empleado creado exitosamente.");
                    this.Close(); // Cierra la ventana después de crear el empleado
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
