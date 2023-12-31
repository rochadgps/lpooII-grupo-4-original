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
using ClasesBase;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

namespace Vistas
{
    /// <summary>
    /// Interaction logic for RegistrarEntrada.xaml
    /// </summary>
    public partial class RegistrarEntrada : Window
    {
        //CollectionView Vista;
        ObservableCollection<Cliente> listaCliente;
        DataTable listaTipoVehiculo = new DataTable();
        private CollectionViewSource vistaColeccionFiltrada;
        public static ObservableCollection<Ticket> prueba;

        //para la duracion

        public int codigoSector;
        public int zona;
        decimal sTarifa = 0;

        public RegistrarEntrada(int sectorId)
        {
            InitializeComponent();
            codigoSector = sectorId;
            vistaColeccionFiltrada = Resources["VISTA_CLI"] as CollectionViewSource;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ObjectDataProvider odp = (ObjectDataProvider)this.Resources["LIST_CLI"];
            listaCliente = odp.Data as ObservableCollection<Cliente>;
            ObjectDataProvider odp1 = (ObjectDataProvider)this.Resources["LIST_TIPO"];
            listaTipoVehiculo = odp1.Data as DataTable;

            seleccionarEntrada();
        }

        public void seleccionarEntrada()
        {
            DateTime fechaentra = DateTime.Now;
            dateTimeEntraTicket.SelectedDate = fechaentra;
            //Desactivar los controles innecesarios como tarifa y total
            txtTarifa.IsEnabled = false;
            //txtTotal.IsEnabled = false;
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            this.Close();
            main.Show();
        }

        private void txtDniCliente_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (vistaColeccionFiltrada != null)
            {
                //Se indica el metodo eventVistaUsuario Filter a medida que escriba en el textBox
                vistaColeccionFiltrada.Filter += eventVistaCliente_Filter;
            }
        }

        private void eventVistaCliente_Filter(object sender, FilterEventArgs e)
        {
            Cliente usuario = e.Item as Cliente;

            if (usuario.Cli_Dni.StartsWith(txtDniCliente.Text, StringComparison.CurrentCultureIgnoreCase))
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
            }
        }

        Ticket tickultimo;
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            Ticket ticket = new Ticket();
            TipoVehiculo v = new TipoVehiculo();
            DataRowView drv = (DataRowView)cboTipoVehiculo.SelectedItem;

            //v = (TipoVehiculo)cboTipoVehiculo.SelectedItem;

            ticket.Cli_Dni = int.Parse(txtDniCliente.Text);
            ticket.Tick_FechaHoraEntra = DateTime.Now;
            ticket.Tick_FechaHoraSale = DateTime.Now.AddHours(1);
            ticket.Cli_Dni = int.Parse(txtDniCliente.Text.ToString());
            ticket.TipoV_Codigo = Convert.ToInt32(drv["tipov_codigo"]);
            ticket.Tick_Patente = txtPatente.Text.ToString();
            ticket.Tick_Tarifa = decimal.Parse(txtTarifa.Text);
            ticket.Tick_Duracion = 0;
            ticket.Tick_Total = 0;
            ticket.Sec_Codigo = codigoSector;

            TrabajarTicket.nuevoTicket(ticket);

            //TrabajarSector.liberarSector(false, ticket.Sec_Codigo);
            tickultimo = TrabajarTicket.traerultimo();
            MessageBox.Show("se agrego correctamente");
            FixedDocs fix = new FixedDocs(tickultimo);
            fix.Show();
            this.Hide();
            MainWindow main = new MainWindow();
            this.Close();
            main.Show();

        }

        private void txtTarifa_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void cboTipoVehiculo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedValue = cboTipoVehiculo.SelectedIndex;
            sTarifa = decimal.Parse(listaTipoVehiculo.Rows[selectedValue][2].ToString());
            txtTarifa.Text = sTarifa.ToString();
            //txtTotal.Text = (calcularTotal(sTarifa, sDuracion)).ToString();
        }

        private decimal calcularTotal(decimal tar, decimal dur)
        {
            dur = dur / 60;
            decimal total = tar * decimal.Parse(dur.ToString());
            return Math.Round(total, 2);
        }

        private void cboDuracion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //sDuracion = decimal.Parse(cboDuracion.SelectedValue.ToString());
           // txtTotal.Text = (calcularTotal(sTarifa, sDuracion)).ToString();
        }

        private void convertirFecha()
        {
            //int HoraE = int.Parse(cboHoraEntrada.ToString());
            //int MinutosE = int.Parse(cboHoraEntrada.ToString());
            //TimeSpan entrada = new TimeSpan(HoraE, MinutosE, 0);


            //dateTimeEntraTicket = DateTime.Parse(dateTimeEntraTicket.ToString()) + entrada;
        }
        private void consulta()
        {
            TrabajarTicket.traerTickets();

        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            limpiarCampos();
        }

        private void limpiarCampos()
        {
            txtDniCliente.Clear();
            txtPatente.Clear();
            txtTarifa.Clear();
            //txtTotal.Clear();
            //cboHoraEntrada.SelectedItem = null;
            //cboMinutosEntrada.SelectedItem = null;
        }
    }
}
