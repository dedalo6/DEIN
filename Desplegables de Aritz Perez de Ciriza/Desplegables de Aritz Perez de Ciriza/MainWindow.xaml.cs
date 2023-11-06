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

namespace Desplegables_de_Aritz_Perez_de_Ciriza
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

        private void M_Nuevo_Click(Object sender, RoutedEventArgs e)
        {
            MainWindow NuevaVentana = new MainWindow();
            NuevaVentana.Show();
        }

        private void M_Abrir_Click(Object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog AbrirFichero = new Microsoft.Win32.OpenFileDialog();
            AbrirFichero.ShowDialog();
        }

        private void M_Guardar_Click(Object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Guardado");
        }
        private void M_Guardar_como_Click(Object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog GuardarFicheroComo = new Microsoft.Win32.SaveFileDialog();
            GuardarFicheroComo.ShowDialog();
            
        }
        private void M_Imprimir_Click(Object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.PrintDialog Imprimir = new System.Windows.Controls.PrintDialog();
            Imprimir.ShowDialog();
        }

        private void M_Salir_Click(Object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void M_Usuarios_Click(Object sender, RoutedEventArgs e)
        {
            Usuarios AbrirUsuarios = new Usuarios();
            AbrirUsuarios.Show();
        }

        private void M_Copiar_Click(Object sender, RoutedEventArgs e)
        {
            MessageBox.Show(" Copiado ");
        }

        private void M_Cortar_Click(Object sender, RoutedEventArgs e)
        {
            MessageBox.Show(" Cortado ");
        }

        private void M_Pegar_Click(Object sender, RoutedEventArgs e)
        {
            MessageBox.Show(" Pegado ");
        }

        private void M_Eliminar_Click(Object sender, RoutedEventArgs e)
        {
            MessageBox.Show(" Eliminado ");
        }
    }
}
