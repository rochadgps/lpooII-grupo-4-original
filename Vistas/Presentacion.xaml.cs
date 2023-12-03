﻿/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.IO;

namespace Vistas
{
    /// <summary>
    /// Interaction logic for Presentacion.xaml
    /// </summary>
    public partial class Presentacion : Window
    {
        public Presentacion()
        {
            InitializeComponent();
            try
            {

                string carpetaBase = AppDomain.CurrentDomain.BaseDirectory;
                string audioFilePath = Path.Combine(carpetaBase, "..", "..", "media", "splash.wav");

                mediaPlayer.Source = new Uri(audioFilePath);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al iniciar la aplicación: " + ex.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mediaPlayer.LoadedBehavior = MediaState.Manual;
            mediaPlayer.Source = new Uri("/Vistas/media/splash.wav", UriKind.Relative);
            mediaPlayer.Play();
        }

        private void meAudio_MediaEnded(object sender, RoutedEventArgs e)
        {

            Login oLogin = new Login();
            oLogin.Show();
            this.Close();
        }
    }
}
