﻿using System;
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
using System.Windows.Shapes;

namespace Vistas
{
    /// <summary>
    /// Interaction logic for Zonas.xaml
    /// </summary>
    public partial class Zonas : Window
    {
        public Zonas()
        {
            InitializeComponent();
        }

        private void btnZ1_Click(object sender, RoutedEventArgs e)
        {
            VehiculosEnPlaya sector = new VehiculosEnPlaya(1);
            sector.Show();
            this.Close();
        }

        private void btnZ2_Click(object sender, RoutedEventArgs e)
        {
            VehiculosEnPlaya sector = new VehiculosEnPlaya(2);
            sector.Show();
            this.Close();
        }

        private void btnZ3_Click(object sender, RoutedEventArgs e)
        {
            VehiculosEnPlaya sector = new VehiculosEnPlaya(3);
            sector.Show();
            this.Close();
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            this.Close();
            main.Show();
        }
    }
}