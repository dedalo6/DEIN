using System;
using System.Collections.Generic;
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
using static GestionEmpleados2023.ListaEmpleados;

namespace GestionEmpleados2023
{
    /// <summary>
    /// Lógica de interacción para BuscarEmpleado.xaml
    /// </summary>
    public partial class BuscarEmpleado : Window
    {
        public BuscarEmpleado()
        {
            InitializeComponent();

            esUsuarioTextBlock.IsEnabled = false;
            esUsuarioCheckBox.IsEnabled = false;
        }

        private void buscarEmpleadoClick(object sender, RoutedEventArgs e)
        {

            List<Empleado> empleados = ObtenerEmpleados();
            miDataGrid.ItemsSource = empleados;
        }
       
        public List<Empleado> ObtenerEmpleados()
        {
            
            //Primero construiremos la consulta

            string consulta = "SELECT * FROM empleados WHERE";
            string nombre = "";
            string apellidos = "";

            //Si Nombre no esta vacio lo asigno a una variable
            if (txtNombre.Text != null || txtNombre.Text != "")
            {
                nombre = txtNombre.Text;
            }
            
            bool nombreAnd = (bool) rbNombreAnd.IsChecked;

            if (txtApellidos.Text != null || txtApellidos.Text != "")
            {
                apellidos = txtApellidos.Text;
            }
            
            bool apellidosAnd = (bool)rbApellidosAnd.IsChecked;


            bool esUsuario = (bool) esUsuarioCheckBox.IsChecked;

            bool esUsuarioActivated = (bool)rbActivateEsUsuario.IsChecked;

            //Separamos los casos de que nombre sea un solo nombre, o que haya varios separados por comas

            if(nombre != "")
            {
                if (nombreAnd || !nombre.Contains(","))
                {
                    consulta += " Nombre = @Nombre ";
                }
                else
                {
                    string[] arrayNombres = nombre.Split(',');

                    //Quito espacios
                    for (int i = 0; i < arrayNombres.Length; i++)
                    {
                        arrayNombres[i] = arrayNombres[i].Trim();
                    }

                    consulta += " (Nombre = @Nombre1 ";

                    for (int i = 0; i < arrayNombres.Length - 1; i++)
                    {
                        consulta += " OR Nombre = @Nombre"+ (i+2) + " ";
                    }

                    consulta += ")";

                }
            }

            //Igual con apellidos
            if (apellidos != "")
            {
                if (apellidosAnd || !apellidos.Contains(","))
                {
                    consulta += (nombre == "") ? " Apellidos = @Apellidos " : " AND Apellidos = @Apellidos ";


                }
                else
                {
                    string[] arrayApellidos = apellidos.Split(',');

                    //Quito espacios
                    for (int i = 0; i < arrayApellidos.Length; i++)
                    {
                        arrayApellidos[i] = arrayApellidos[i].Trim();
                    }

                    consulta += (nombre == "") ? " (Apellidos = @Apellidos1 " : " AND (Apellidos = @Apellidos1 ";  //Pregunto si ahi un nombre por delante, si lo hay pongo

                    for (int i = 0; i < arrayApellidos.Length - 1; i++)
                    {
                        consulta += " OR Apellidos = @Apellidos" + (i + 2) + " ";
                    }
                    consulta += ")";


                }
            }

            //Miramos si usuario esta activo, si lo esta se tomara en consideracion en la consulta, si no, no.

            if (esUsuarioActivated)
            {              
                consulta += (nombre == "" && apellidos == "") ? " EsUsuario = @EsUsuario " : " AND EsUsuario = @EsUsuario ";   
            }

            laConsulta.Text = consulta;

            if (consulta.Substring(consulta.LastIndexOf(" ") + 1) == "WHERE") // Miramos si la consulta esta vacia
            {
                MessageBox.Show("Escribe algo en los campos, o activa EsUsuario ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<Empleado>();
            }
            else
            {
                //Creamos la conexion
                using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["GestionEmpleados2023.Properties.Settings.GestionEmpleadosConnectionString"].ConnectionString))
                {
                    DataTable Empleados = new DataTable(); // Aqui meteremos los resultados de la consulta

                    List<Empleado> listaEmpleados = new List<Empleado>();

                    // Aqui meteremos los parametros de la consulta de forma segura para evitar inyecciones de codigo

                    using (SqlDataAdapter adaptadorSql = new SqlDataAdapter(consulta, conexion))
                    {
                        if (!nombre.Contains(","))
                        {
                            adaptadorSql.SelectCommand.Parameters.AddWithValue("@Nombre", nombre);
                        }
                        else
                        {
                            string[] arrayNombres = nombre.Split(',');

                            //Quito espacios
                            for (int i = 0; i < arrayNombres.Length; i++)
                            {
                                arrayNombres[i] = arrayNombres[i].Trim();
                            }
                            for (int i = 0; i < arrayNombres.Length; i++)
                            {
                                adaptadorSql.SelectCommand.Parameters.AddWithValue("@Nombre" + (i + 1), arrayNombres[i]);
                            }
                        }

                        if (!apellidos.Contains(","))
                        {
                            adaptadorSql.SelectCommand.Parameters.AddWithValue("@Apellidos", apellidos);
                        }
                        else
                        {
                            string[] arrayApellidos = apellidos.Split(',');

                            //Quito espacios
                            for (int i = 0; i < arrayApellidos.Length; i++)
                            {
                                arrayApellidos[i] = arrayApellidos[i].Trim();
                            }
                            for (int i = 0; i < arrayApellidos.Length; i++)
                            {
                                adaptadorSql.SelectCommand.Parameters.AddWithValue("@Apellidos" + (i + 1), arrayApellidos[i]);
                            }
                        }

                        if (esUsuarioActivated)
                        {
                            adaptadorSql.SelectCommand.Parameters.AddWithValue("@EsUsuario", esUsuario);
                        }




                        try
                        {
                            adaptadorSql.Fill(Empleados); //El adaptador vuelca la informacion de la dataTable
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error al meter los resultados en el dataTable Empleados (Si pones varios nombres, debes seleccionar OR): {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }

                    // Aqui creamos un nuevo empleado con cada row de la dataTable, usandola como enumerable, y luego lo transformamos en una lista y lo guardamos en listaEmpleados

                    listaEmpleados = Empleados.AsEnumerable().Select(row => new Empleado
                    {
                        Nombre = row.Field<string>("Nombre"),
                        Apellidos = row.Field<string>("Apellidos"),
                        EsUsuario = (row["EsUsuario"] != DBNull.Value) ? row.Field<bool>("EsUsuario") : false,
                        Edad = row.Field<int>("Edad"),
                    }).ToList();

                    return listaEmpleados;
                }
            }

            

            
        }



        private void editarEmpleadoClick(object sender, RoutedEventArgs e)
        {
            if (miDataGrid.SelectedItem != null)
            {
                var filaSeleccionada = (Empleado)miDataGrid.SelectedItem;

                var nuevoNombre = txtNombre.Text;
                var nuevoApellidos = txtApellidos.Text;
                var nuevoEsUsuario = (bool)esUsuarioCheckBox.IsChecked;
                

                using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["GestionEmpleados2023.Properties.Settings.GestionEmpleadosConnectionString"].ConnectionString))
                {
                    string consulta = "UPDATE empleados SET Nombre = @NuevoNombre, Apellidos = @NuevoApellidos, EsUsuario = @NuevoEsUsuario WHERE Nombre = @Nombre AND Apellidos = @Apellidos AND EsUsuario = @EsUsuario AND Edad = @Edad";

                    using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                    {
                        cmd.Parameters.AddWithValue("@NuevoNombre", nuevoNombre);
                        cmd.Parameters.AddWithValue("@NuevoApellidos", nuevoApellidos);
                        cmd.Parameters.AddWithValue("@NuevoEsUsuario", nuevoEsUsuario);
                        cmd.Parameters.AddWithValue("@Nombre", filaSeleccionada.Nombre);
                        cmd.Parameters.AddWithValue("@Apellidos", filaSeleccionada.Apellidos);
                        cmd.Parameters.AddWithValue("@EsUsuario", filaSeleccionada.EsUsuario);
                        cmd.Parameters.AddWithValue("@Edad", filaSeleccionada.Edad);

                        try
                        {
                            conexion.Open();
                            int numeroFilasAfectadas = cmd.ExecuteNonQuery();
                            //laConsulta.Text = numeroFilasAfectadas + ""; // Lo quito porque recargo el datagrid, pero si no saldria siempre 1, el numero de filas afectadas
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error al editar empleado: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }

                // Actualizar el DataGrid después de la edición

                List<Empleado> empleados = ObtenerEmpleados();
                miDataGrid.ItemsSource = empleados;
            }
            else
            {
                MessageBox.Show("Selecciona una fila antes de hacer clic en el botón Editar.");
            }
        }


        private void RadioButtonActivate_Checked(object sender, RoutedEventArgs e)
        {
            // Habilitar el TextBlock y el CheckBox cuando el RadioButton "Activate" se selecciona
            esUsuarioTextBlock.IsEnabled = true;
            esUsuarioCheckBox.IsEnabled = true;
        }

        private void RadioButtonDeactivate_Checked(object sender, RoutedEventArgs e)
        {
            // Deshabilitar el TextBlock y el CheckBox cuando el RadioButton "Deactivate" se selecciona
            esUsuarioTextBlock.IsEnabled = false;
            esUsuarioCheckBox.IsEnabled = false;
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
