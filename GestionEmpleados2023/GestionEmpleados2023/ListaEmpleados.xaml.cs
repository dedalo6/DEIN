using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Shapes;

namespace GestionEmpleados2023
{
    /// <summary>
    /// Lógica de interacción para ListaEmpleados.xaml
    /// </summary>
    public partial class ListaEmpleados : Window
    {
        private GestionEmpleados2023 gestionEmpleados;
        public ListaEmpleados()
        {
            InitializeComponent();
            gestionEmpleados = new GestionEmpleados2023();
            CargarEmpleadosEnDataGrid();

        }

        private void CargarEmpleadosEnDataGrid()
        {
            List<Empleado> empleados = gestionEmpleados.ObtenerEmpleados();
            miDataGrid.ItemsSource = empleados;
        }




        public partial class GestionEmpleados2023
        {
            private SqlConnection conexionConSql;

            public GestionEmpleados2023()
            {
                EstablecerConexion();
            }

            public void EstablecerConexion()
            {
                string CadenaDeConexion = ConfigurationManager.ConnectionStrings["GestionEmpleados2023.Properties.Settings.GestionEmpleadosConnectionString"].ConnectionString;
                /**
                 * Descompuesto en variables:
                 * 
                // Obtener la colección de cadenas de conexión desde la configuración de la aplicación
                ConnectionStringSettingsCollection connectionStrings = ConfigurationManager.ConnectionStrings;

                // Nombre de la cadena de conexión específica
                string nombreCadena = "GestionEmpleados2023.Properties.Settings.GestionEmpleadosConnectionString";

                // Acceder a la cadena de conexión específica mediante el nombre
                ConnectionStringSettings cadenaConfiguracion = connectionStrings[nombreCadena];

                // Verificar si se encontró la cadena de conexión
                if (cadenaConfiguracion != null)
                {
                    // Obtener la cadena de conexión real en formato de texto
                    string CadenaDeConexion = cadenaConfiguracion.ConnectionString;

                    // Ahora, CadenaDeConexion contiene la cadena de conexión que originalmente se asignó en una sola línea.
                }
                else
                {
                    // Manejar el caso en el que la cadena de conexión no se encuentra
                    // Puedes mostrar un mensaje de error, asignar un valor predeterminado, o tomar alguna otra acción apropiada.
                }

                */

                conexionConSql = new SqlConnection(CadenaDeConexion);
            }

            public List<Empleado> ObtenerEmpleados()
            {
                EstablecerConexion();

                string consulta = "SELECT * FROM EMPLEADOS";

                DataTable Empleados = new DataTable();

                List<Empleado> listaEmpleados = new List<Empleado>();

                SqlDataAdapter adaptadorSql = new SqlDataAdapter(consulta, conexionConSql);

                using (adaptadorSql)
                {
                    adaptadorSql.Fill(Empleados);
                }


                listaEmpleados = Empleados.AsEnumerable().Select(row => new Empleado
                {
                    Nombre = row.Field<string>("Nombre"),
                    Apellidos = row.Field<string>("Apellidos"),
                    EsUsuario = (row["EsUsuario"] != DBNull.Value) ? row.Field<bool>("EsUsuario") : false,
                    Edad = row.Field<int>("Edad"),
                }).ToList();

                /**
                 * EQUIVALENTE A:
                 * 
                listaEmpleados = new List<Empleado>();

                foreach (DataRow row in Empleados.Rows)
                {
                    Empleado empleado = new Empleado();

                    // Asignar valores a las propiedades del objeto Empleado
                    empleado.Nombre = row.Field<string>("Nombre");
                    empleado.Apellidos = row.Field<string>("Apellidos");

                    // Verificar si la columna "EsUsuario" no es nula
                    if (row["EsUsuario"] != DBNull.Value)
                    {
                        empleado.EsUsuario = row.Field<bool>("EsUsuario");
                    }
                    else
                    {
                        // Asignar false si la columna "EsUsuario" es nula
                        empleado.EsUsuario = false;
                    }

                    empleado.Edad = row.Field<int>("Edad");

                    // Agregar el objeto Empleado a la lista
                    listaEmpleados.Add(empleado);
                }
                */

                return listaEmpleados;
            }

        }

        public class Empleado
        {
            public string Nombre { get; set; }
            public string Apellidos { get; set; }
            public bool EsUsuario { get; set; }
            public int Edad { get; set; }

        }

        private void eliminarEmpleadoClick(object sender, RoutedEventArgs e)
        {
            if (miDataGrid.SelectedItem != null)
            {
                
                var filaSeleccionada = (Empleado)miDataGrid.SelectedItem;
              
                var valorColumnaNombre = filaSeleccionada.Nombre;
                var valorColumnaApellidos = filaSeleccionada.Apellidos;
                var valorColumnaEsUsuario = filaSeleccionada.EsUsuario;
                var valorColumnaEdad = filaSeleccionada.Edad;



                using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["GestionEmpleados2023.Properties.Settings.GestionEmpleadosConnectionString"].ConnectionString))
                {
                    //cmd.CommandText = "DELETE FROM empleados WHERE Nombre = @Nombre AND Apellidos = @Apellidos AND EsUsuario = @EsUsuario AND Edad = @Edad";
                    string consulta = "DELETE FROM empleados WHERE Nombre = @Nombre AND Apellidos = @Apellidos AND EsUsuario = @EsUsuario AND Edad = @Edad";

                    using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                    {
                        cmd.Parameters.AddWithValue("@Nombre", valorColumnaNombre);
                        cmd.Parameters.AddWithValue("@Apellidos", valorColumnaApellidos);
                        cmd.Parameters.AddWithValue("@EsUsuario", valorColumnaEsUsuario);
                        cmd.Parameters.AddWithValue("@Edad", valorColumnaEdad);


                        try
                        {
                            conexion.Open();
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error al agregar empleado: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }


                }
                // Para borrar el dataGrid

                //List<Empleado> listaEmpleados = (List<Empleado>)miDataGrid.ItemsSource;

                //listaEmpleados.Remove(filaSeleccionada);

                //miDataGrid.ItemsSource = null;
                //miDataGrid.ItemsSource = listaEmpleados;

                //Alternativa, recargar el data grid
                gestionEmpleados = new GestionEmpleados2023();
                CargarEmpleadosEnDataGrid();

            }
            else
            {
                MessageBox.Show("Selecciona una fila antes de hacer clic en el botón.");
            }
        }

        private void irAMainClick(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();

            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
            {
                parentWindow.Close();
            }

            main.Show();
        }
    }
}
