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
            DataContext = this;

        }

        private void button_Imagen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();



            //openFileDialog.InitialDirectory = Environment.CurrentDirectory; //Con esto puedes saber donde esta ejecutandose el programa, desde que carpeta, que sera Debug o Realease
            //openFileDialog.InitialDirectory = System.IO.Directory.GetParent(System.IO.Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString() + "\\Images"; //Este monstruo funciona


            //IMPORTANTE: ESTA ES LA MANERA DE USAR PATHS System.IO.Path, Si pones solo Path. Estas usando una infernal clase que solo sirve PARA LIAR y para hacer lineas y curvas

            openFileDialog.InitialDirectory = System.IO.Path.GetFullPath("..\\..\\Images");  //Con esto salimos dos carpetas hacia fuera desde Debug o Release y llegamos a la carpeta que contiene Images
            // No podia ser tan facil como poner la ruta y ya esta, no, claro que no!

            openFileDialog.FilterIndex = 1;
            //Resumen:
            //     Obtiene o establece el índice del filtro que está seleccionado en un cuadro de
            //     diálogo de archivo.
            //
            // Devuelve:
            //     El System.Int32 que es el índice del filtro seleccionado. El valor predeterminado
            //     es 1.
            openFileDialog.Multiselect = false;
            // Resumen:
            //     Obtiene o establece un valor que indica si la casilla de verificación de sólo
            //     lectura se muestran por Microsoft.Win32.OpenFileDialog está seleccionada.
            //
            // Devuelve:
            //     true Si la casilla de verificación está seleccionada; de lo contrario, false.
            //     De manera predeterminada, es false.




            openFileDialog.Filter = "Archivos de Imagen|*.jpg;*.png;*.jpeg;|Todos los archivos|*.*";
            // Resumen: (public string Filter { get; set; })
            //     Obtiene o establece la cadena de filtro que determina qué tipos de archivo se
            //     muestran desde Microsoft.Win32.OpenFileDialog o Microsoft.Win32.SaveFileDialog.
            //
            // Devuelve:
            //     Un objeto System.String que contiene el filtro. El valor predeterminado es System.String.Empty,
            //     que indica que no se aplica ningún filtro y que se muestran todos los tipos de
            //     archivo.
            //
            // Excepciones:
            //   T:System.ArgumentException:
            //     La cadena de filtro no es válida.


            if ((bool)openFileDialog.ShowDialog())
            //
            // Resumen:
            //     Muestra un cuadro de diálogo común.
            //
            // Parámetros:
            //   owner:
            //     Identificador de la ventana que posee el cuadro de diálogo.
            //
            // Devuelve:
            //     Si el usuario hace clic en el botón Aceptar del cuadro de diálogo que aparece
            //     (por ejemplo, Microsoft.Win32.OpenFileDialog, Microsoft.Win32.SaveFileDialog),
            //     true devuelto; de lo contrario, false.
            {
                string rutaImagen = openFileDialog.FileName;
                //
                // Resumen:
                //     Obtiene una matriz que contiene un nombre de archivo seguro de cada archivo seleccionado.
                //
                // Devuelve:
                //     Una matriz de System.String que contiene un nombre de archivo seguro de cada
                //     archivo seleccionado. El valor predeterminado es una matriz con un elemento único
                //     cuyo valor es System.String.Empty.



                //BitmapImage imagenBitmap = new BitmapImage(new Uri(rutaImagen), new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.CacheOnly));
                
                BitmapImage imagenBitmap = new BitmapImage();
                imagenBitmap.BeginInit(); //Pedimos que pare el inicio para que nos deje establecer la uri
                imagenBitmap.UriSource = new Uri(rutaImagen); // Le indicamos la ruta hacia la imagen que hemos conseguido con openFileDialog.FileName
                imagenBitmap.DecodePixelWidth = 80;// Con esto la imagen no se deformara por minimizarla
                imagenBitmap.DecodePixelHeight = 100;
                imagenBitmap.EndInit(); //Le decimos que termine el inicio
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


            if (textBox_nombre.Text == "")
            {
                MessageBox.Show("El campo Nombre no puede estar vacío", "Error");
            }
            else if (textBox_apellidos.Text == "")
            {
                MessageBox.Show("El campo Apellidos no puede estar vacío", "Error");
            }
            else if (textBox_email.Text == "")
            {
                MessageBox.Show("El campo E-mail no puede estar vacío", "Error");
            }
            else if (textBox_telefono.Text == "")
            {
                MessageBox.Show("El campo Teléfono no puede estar vacío", "Error");
            }
            else
            {
                Empleado nuevoEmpleado = new Empleado(textBox_nombre.Text, textBox_apellidos.Text, textBox_email.Text, textBox_telefono.Text);
                dataGrid.Items.Add(nuevoEmpleado);


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

        private void lostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                if (String.IsNullOrWhiteSpace(textBox.Text))
                {
                    if (textBox.Name == "textBox_direccion")
                    {
                        textBox.Text = "Dirección";
                    }
                    else if (textBox.Name == "textBox_ciudad")
                    {
                        textBox.Text = "Ciudad";
                    }
                    else if (textBox.Name == "textBox_provincia")
                        textBox.Text = "Provincia";
                    else if (textBox.Name == "textBox_codigo")
                        textBox.Text = "Código Postal";
                    else if (textBox.Name == "textBox_pais")
                        textBox.Text = "País";
                }
            }
        }
    }
}
