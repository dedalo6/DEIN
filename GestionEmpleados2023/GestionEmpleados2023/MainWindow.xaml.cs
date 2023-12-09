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

namespace GestionEmpleados2023
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void listaEmpleadosClick(object sender, RoutedEventArgs e)
        {
            ListaEmpleados listaEmpleados = new ListaEmpleados();

            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
            {
                parentWindow.Close();
            }

            //Falta llamar a cargar empleados
            //NO PORQUE SE LLAMA AL INICIAR LA VENTANA

            listaEmpleados.Show();

        }

        public void agregarEmpleadosClick(object sender, RoutedEventArgs e)
        {
            AgregarEmpleado agregarEmpleados = new AgregarEmpleado();

            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
            {
                parentWindow.Close();
            }

            agregarEmpleados.Show();

        }

        private void buscarEmpleadosClick(object sender, RoutedEventArgs e)
        {
            BuscarEmpleado buscarEmpleados = new BuscarEmpleado();

            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
            {
                parentWindow.Close();
            }

            buscarEmpleados.Show();
        }
    }
}
