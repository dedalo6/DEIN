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
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ProyectoBaseDeDatos
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection miConexionSql;

        public MainWindow()
        {
            
            InitializeComponent();

            string miConexion = ConfigurationManager.ConnectionStrings["ProyectoBaseDeDatos.Properties.Settings.BaseDeDatosConnectionString"].ConnectionString;

            miConexionSql = new SqlConnection(miConexion);

            miConexionSql.Open();

            MuestraUsuarios();


        }

        public void MuestraUsuarios()
        {
            string consulta = "SELECT * FROM USUARIOS";

            SqlDataAdapter miAdaptadorSql = new SqlDataAdapter(consulta, miConexionSql);

            using (miAdaptadorSql)
            {
                DataTable usuariosTabla = new DataTable();

                miAdaptadorSql.Fill(usuariosTabla);

                //listaUsuarios.DisplayMemberPath = "nombre";
                //listaUsuarios.SelectedValuePath = "Id";
                listaUsuarios.ItemsSource = usuariosTabla.DefaultView;


            }
        }

        //public void MuestraExistencias()
        //{
        //    string consulta = "SELECT Cantidad FROM EXISTENCIAS WHERE Id = 2";

        //    SqlDataAdapter miAdaptadorSql = new SqlDataAdapter(consulta, miConexionSql);

        //    using (miAdaptadorSql)
        //    {
        //        DataTable existenciasTabla = new DataTable();

        //        miAdaptadorSql.Fill(existenciasTabla);

        //        //listaUsuarios.DisplayMemberPath = "nombre";
        //        //listaUsuarios.SelectedValuePath = "Id";
        //        textBlock.Text = existenciasTabla.???;


        //    }
        //}




    }

   

}
