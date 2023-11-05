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
using System.Windows.Shapes;

namespace Estilos_de_Aritz_Perez_de_Ciriza
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void ButtonMesas_Click(object sender, RoutedEventArgs e)
        {
            Window1 AbrirVentana1_Mesas = new Window1();

            this.Close();

            AbrirVentana1_Mesas.Show();
        }

        private void ButtonTimetable_Click(object sender, RoutedEventArgs e)
        {
            Window2 AbrirVentana2_Timetable = new Window2();

            this.Close();

            AbrirVentana2_Timetable.Show();
        }

        private void ButtonRecetas_Click(object sender, RoutedEventArgs e)
        {
            Window3 AbrirVentana3_Recetas = new Window3();

            this.Close();

            AbrirVentana3_Recetas.Show();
        }

        private void ButtonMain_Click(object sender, RoutedEventArgs e)
        {
            MainWindow AbrirVentana0_Main = new MainWindow();

            this.Close();

            AbrirVentana0_Main.Show();
        }
    }
}
