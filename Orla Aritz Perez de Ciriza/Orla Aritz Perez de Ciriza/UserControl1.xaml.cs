﻿using System;
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

namespace Orla_Aritz_Perez_de_Ciriza
{
    /// <summary>
    /// Lógica de interacción para UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {

        public static DependencyProperty NombreProperty =
            DependencyProperty.Register("Nombre", typeof(string), typeof(UserControl1), new
            PropertyMetadata(string.Empty));

        public static DependencyProperty ApellidosProperty =
    DependencyProperty.Register("Apellidos", typeof(string), typeof(UserControl1), new
    PropertyMetadata(string.Empty));

        public static DependencyProperty ImageProperty =
    DependencyProperty.Register("Image", typeof(string), typeof(UserControl1), new
     PropertyMetadata(string.Empty));

        public static DependencyProperty EmailProperty =
    DependencyProperty.Register("Email", typeof(string), typeof(UserControl1), new
    PropertyMetadata(string.Empty));

        public UserControl1()
        {
            InitializeComponent();
        }

        private void Persona_Click(object sender, RoutedEventArgs e)
        {
            LabelPuesto.Text = Email;
        }

        private void Persona_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Profile win2 = new Profile(Nombre, Apellidos, Email, Image);
            win2.Show();
        }

        private void Persona_MouseEnter(object sender, MouseEventArgs e)
        {
            LabelPuesto.Text = Nombre + " " + Apellidos;
        }

        private void Persona_MouseLeave(object sender, MouseEventArgs e)
        {
            LabelPuesto.Text = "";
        }




        public string Nombre
        {
            get { return (string)GetValue(NombreProperty); }
            set { SetValue(NombreProperty, value); }
        }

        public string Apellidos
        {
            get { return (string)GetValue(ApellidosProperty); }
            set { SetValue(ApellidosProperty, value); }
        }

        public string Email
        {
            get { return (string)GetValue(EmailProperty); }
            set { SetValue(EmailProperty, value); }
        }

        public string Image
        {
            get { return (string)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }
    }
}
