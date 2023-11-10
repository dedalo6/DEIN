using Microsoft.Win32;
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

namespace DataBindingFormularios
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

        private void button_Imagen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos de Imagen|*.jpg;*.jpeg;*.png;*.bmp|Todos los archivos|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string rutaImagen = openFileDialog.FileName;

                // Cargar la imagen seleccionada en el control Image
                BitmapImage imagenBitmap = new BitmapImage(new Uri(rutaImagen));
                Image.Source = imagenBitmap;
            }
        }

        private void button_cancelar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow nuevaVentana = new MainWindow();
            this.Close();
            nuevaVentana.Show();
            
        }

        private void button_guardar_Click(object sender, RoutedEventArgs e)
        {


            if (box_nombre.Text == "")
            {
                MessageBox.Show("El campo Nombre no puede estar vacío", "Error");
            }
            else if (box_apellidos.Text == "")
            {
                MessageBox.Show("El campo Apellidos no puede estar vacío", "Error");
            }
            else if (box_email.Text == "")
            {
                MessageBox.Show("El campo E-mail no puede estar vacío", "Error");
            }
            else if (box_telefono.Text == "")
            {
                MessageBox.Show("El campo Teléfono no puede estar vacío", "Error");
            }
            else
            {
                Empleado nuevoEmpleado = new Empleado(box_nombre.Text, box_apellidos.Text, box_email.Text, box_telefono.Text);


                datagrid.Items.Add(nuevoEmpleado);
            }


        }







        public class Empleado
        {
            public string nombre { get; set; }
            public string apellidos { get; set; }
            public string email { get; set; }
            public string telefono { get; set; }

            public Empleado(string nombre, string apellidos, string email, string telefono)
            {
                this.nombre = nombre;
                this.apellidos = apellidos;
                this.email = email;
                this.telefono = telefono;
            }
        }

        private void gotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textbox)
            {
                if (!String.IsNullOrWhiteSpace(textbox.Text))
                {
                    textbox.Text = "";
                }

            }
        }

        private void Txt_lostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textbox)
            {
                if (String.IsNullOrWhiteSpace(textbox.Text))
                {
                    if (textbox.Name == "box_direccion")
                    {
                        textbox.Text = "Dirección";
                    }
                    else if (textbox.Name == "box_ciudad")
                    {
                        textbox.Text = "Ciudad";
                    }
                    else if (textbox.Name == "box_provincia")
                        textbox.Text = "Provincia";
                    else if (textbox.Name == "box_codigo")
                        textbox.Text = "Código Postal";
                    else if (textbox.Name == "box_pais")
                        textbox.Text = "País";
                }
            }
        }
    }
}
