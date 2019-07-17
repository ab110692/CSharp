using Br.Com.Posi.Shelf.DAO;
using Br.Com.Posi.Shelf.Desktop.GUI.Outro;
using Br.Com.Posi.Shelf.Model;
using Br.Com.Posi.Util;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Br.Com.Posi.Shelf.Desktop.GUI.Cliente
{
    /// <summary>
    /// Interaction logic for ComputadorPage.xaml
    /// </summary>
    public partial class ComputadorPage : Page
    {
        //Select
        Model.Cliente clienteSelect;
        Computador computadorSelect;
        Contrato contratoSelect;
        //DAO
        IComputadorDAO daoComputador;
        IContratoDAO daoContrato;
        IAntiVirusDAO daoAntiVirus;
        IMSWindowsDAO daoMSWindows;
        //List
        private List<AntiVirus> antivirus;
        private List<MSWindows> mswindows;
        private List<Computador> computadores;
        private List<Contrato> contratos;


        public ComputadorPage(Model.Cliente cliente)
        {
            InitializeComponent();

            daoComputador = DAOFactory.InitComputadorDAO();
            daoContrato = DAOFactory.InitContratoDAO();
            daoAntiVirus = DAOFactory.InitAntivirusDAO();
            daoMSWindows = DAOFactory.InitMSWindowsDAO();

            CarregarCliente(cliente);
            CarregarComputador(cliente);
            CarregarContrato();

            habilitarDesabilitarCampos(false);
            habilitarContrato(false);
        }

        private void CarregarCliente(Model.Cliente cliente)
        {
            contratos = new List<Contrato>();
            contratoSelect = new Contrato();

            clienteSelect = cliente;
            //contratos = daoContrato.GetByFK(clienteSelect.IDCliente);
        }

        private void CarregarComputador(Br.Com.Posi.Shelf.Model.Cliente cliente)
        {
            antivirus = new List<AntiVirus>();
            mswindows = new List<MSWindows>();
            computadores = new List<Computador>();
            computadorSelect = new Computador();

            antivirus = daoAntiVirus.GetList();
            mswindows = daoMSWindows.GetList();

            //computadores = daoComputador.GetByFK(cliente.IDCliente);
            cb_Antivirus.ItemsSource = antivirus;
            cb_SistemaOperacional.ItemsSource = mswindows;

            dg_Computadores.ItemsSource = computadores;
            dg_Computadores.Items.Refresh();
        }

        private void CarregarContrato()
        {
            dg_Contrato.ItemsSource = contratos;
            dg_Contrato.Items.Refresh();
        }
        private void btn_Voltar_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Content.Equals("Voltar"))
            {
                if (!string.IsNullOrEmpty(txt_Computador.Text.Trim()) || !string.IsNullOrEmpty(cb_SistemaOperacional.Text.Trim()) || !string.IsNullOrEmpty(txt_LicencaOS.Text.Trim()) || !string.IsNullOrEmpty(txt_MacAddress.Text.Trim()) || !string.IsNullOrEmpty(cb_Antivirus.Text.Trim()) || !string.IsNullOrEmpty(txt_LicencaAntivirus.Text.Trim()))
                {
                    if (MessageDialog.Show(Window.GetWindow(this), "Foi identificado que houveram alterações que ainda não foram salvas, deseja salva-las?", "Sair", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == true)
                    {

                        if (string.IsNullOrEmpty(txt_Computador.Text.Trim()))
                        {
                            BallonDialog.Show("O campo computador deve ser preenchido !", "Campo Nulo ou Inválido");
                            return;
                        }
                        if (cb_SistemaOperacional.SelectedIndex == -1)
                        {
                            BallonDialog.Show("O campo sistema operacional deve ser preenchido !", "Campo Nulo ou Inválido");
                            return;
                        }
                        if (string.IsNullOrEmpty(txt_LicencaOS.Text.Trim()))
                        {
                            BallonDialog.Show("O campo licença sistema operacional deve ser preenchido !", "Campo Nulo ou Inválido");
                            return;
                        }
                        if (string.IsNullOrEmpty(txt_MacAddress.Text.Trim()))
                        {
                            BallonDialog.Show("O campo mac address deve ser preenchido !", "Campo Nulo ou Inválido");
                            return;
                        }
                        if (cb_Antivirus.SelectedIndex == -1)
                        {
                            BallonDialog.Show("O campo antivirus deve ser preenchido !", "Campo Nulo ou Inválido");
                            return;
                        }
                        if (string.IsNullOrEmpty(txt_MacAddress.Text.Trim()))
                        {
                            BallonDialog.Show("O campo licença antivirus deve ser preenchido !", "Campo Nulo ou Inválido");
                            return;
                        }
                        if (string.IsNullOrEmpty(dt_InicioAntiVirus.Text.Trim()))
                        {
                            BallonDialog.Show("O campo data do antivirus deve ser preenchido !", "Campo Nulo ou Inválido");
                            return;
                        }
                        if (string.IsNullOrEmpty(dt_FinalAntiVirus.Text.Trim()))
                        {
                            BallonDialog.Show("O campo data do antivirus deve ser preenchido !", "Campo Nulo ou Inválido");
                            return;
                        }

                        salvarComputador();
                        habilitarDesabilitarCampos(false);                        
                    }
                }
                PrincipalWindow.GetInstance().Main.Navegate(new ClientePage(clienteSelect));
            }
            LimparComputador();
        }

        private void btn_SalvarAlterar_Click(object sender, RoutedEventArgs e)
        {
            if (this.btn_SalvarAlterar.Content.Equals("Salvar"))
            {

                if (string.IsNullOrEmpty(txt_Computador.Text.Trim()))
                {
                    BallonDialog.Show("O campo computador deve ser preenchido !", "Campo Nulo ou Inválido");
                    return;
                }
                if (cb_SistemaOperacional.SelectedIndex == -1)
                {
                    BallonDialog.Show("O campo sistema operacional deve ser preenchido !", "Campo Nulo ou Inválido");
                    return;
                }
                if (string.IsNullOrEmpty(txt_LicencaOS.Text.Trim()))
                {
                    BallonDialog.Show("O campo licença sistema operacional deve ser preenchido !", "Campo Nulo ou Inválido");
                    return;
                }
                if (string.IsNullOrEmpty(txt_MacAddress.Text.Trim()))
                {
                    BallonDialog.Show("O campo mac address deve ser preenchido !", "Campo Nulo ou Inválido");
                    return;
                }
                if (cb_Antivirus.SelectedIndex == -1)
                {
                    BallonDialog.Show("O campo antivirus deve ser preenchido !", "Campo Nulo ou Inválido");
                    return;
                }
                if (string.IsNullOrEmpty(txt_MacAddress.Text.Trim()))
                {
                    BallonDialog.Show("O campo licença antivirus deve ser preenchido !", "Campo Nulo ou Inválido");
                    return;
                }
                if (string.IsNullOrEmpty(dt_InicioAntiVirus.Text.Trim()))
                {
                    BallonDialog.Show("O campo data do antivirus deve ser preenchido !", "Campo Nulo ou Inválido");
                    return;
                }
                if (string.IsNullOrEmpty(dt_FinalAntiVirus.Text.Trim()))
                {
                    BallonDialog.Show("O campo data do antivirus deve ser preenchido !", "Campo Nulo ou Inválido");
                    return;
                }
                salvarComputador();
                habilitarDesabilitarCampos(false);
            }
            else if (btn_SalvarAlterar.Content.Equals("Alterar"))
            {
                alterarComputador();
                habilitarDesabilitarCampos(false);
            }
        }

        private void LimparComputador()
        {
            cb_Antivirus.Text = string.Empty;
            cb_SistemaOperacional.Text = string.Empty;
            txt_Computador.Text = string.Empty;
            txt_LicencaOS.Text = string.Empty;
            txt_MacAddress.Text = string.Empty;
            txt_LicencaAntivirus.Text = string.Empty;
        }

        private void habilitarDesabilitarCampos(bool a)
        {

            txt_Computador.IsEnabled = a;
            txt_LicencaAntivirus.IsEnabled = a;
            txt_MacAddress.IsEnabled = a;
            txt_LicencaOS.IsEnabled = a;
            txt_DescContrat.IsEnabled = a;
            dt_InicioAntiVirus.IsEnabled = a;
            dt_FinalAntiVirus.IsEnabled = a;
            cb_Antivirus.IsEnabled = a;
            cb_SistemaOperacional.IsEnabled = a;
            cb_Ativo.IsEnabled = a;

            if (a == false)
            {
                txt_Computador.Background = new SolidColorBrush(Color.FromRgb(204, 204, 204));
                txt_LicencaAntivirus.Background = new SolidColorBrush(Color.FromRgb(204, 204, 204));
                txt_MacAddress.Background = new SolidColorBrush(Color.FromRgb(204, 204, 204));
                txt_LicencaOS.Background = new SolidColorBrush(Color.FromRgb(204, 204, 204));

                dt_InicioAntiVirus.Background = new SolidColorBrush(Color.FromRgb(204, 204, 204));
                dt_FinalAntiVirus.Background = new SolidColorBrush(Color.FromRgb(204, 204, 204));
                cb_Antivirus.Background = new SolidColorBrush(Color.FromRgb(204, 204, 204));
                cb_SistemaOperacional.Background = new SolidColorBrush(Color.FromRgb(204, 204, 204));
            }
            else if (a == true)
            {
                txt_Computador.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                txt_LicencaAntivirus.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                txt_MacAddress.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                txt_LicencaOS.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));

                dt_InicioAntiVirus.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                dt_FinalAntiVirus.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                cb_Antivirus.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                cb_SistemaOperacional.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            }

        }

        private void btn_NovoAlterar_Click(object sender, RoutedEventArgs e)
        {
            if (btn_NovoAlterar.Content.Equals("Novo"))
            {
                habilitarDesabilitarCampos(true);
                btn_SalvarAlterar.Visibility = Visibility.Visible;
                btn_NovoAlterar.Visibility = Visibility.Collapsed;
                btn_CancelarRemover.Content = "Cancelar";
            }
            else if (btn_NovoAlterar.Content.Equals("Salvar"))
            {
                btn_SalvarAlterar.Visibility = Visibility.Visible;
                btn_NovoAlterar.Visibility = Visibility.Collapsed;
            }
            else if (btn_NovoAlterar.Content.Equals("Alterar"))
            {
                habilitarDesabilitarCampos(true);
                btn_SalvarAlterar.Visibility = Visibility.Visible;
                btn_NovoAlterar.Visibility = Visibility.Collapsed;
                btn_SalvarAlterar.Content = "Alterar";
                btn_CancelarRemover.Content = "Cancelar";
            }
        }

        private void dt_InicioAntiVirus_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            dt_InicioAntiVirus.SelectedDate.Value.ToString();
        }
        private void habilitarContrato(bool b)
        {
            txt_DescContrat.IsEnabled = b;
            cb_Ativo.IsEnabled = b;
            if (b == false)
            {
                txt_DescContrat.Background = new SolidColorBrush(Color.FromRgb(204, 204, 204));
                cb_Ativo.Background = new SolidColorBrush(Color.FromRgb(204, 204, 204));
            }
            else
            {
                txt_DescContrat.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255)); ;
                cb_Ativo.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            }
        }

        private void dg_Computadores_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Computador c = (Computador)dg_Computadores.SelectedItem;

            txt_Computador.Text = c.Nome;
            txt_LicencaOS.Text = c.LicencaWindows;
            txt_MacAddress.Text = c.MacAddress;
            txt_LicencaAntivirus.Text = c.LicencaAntiVirus;
            dt_InicioAntiVirus.Text = c.DataAquisicaoAntiVirus.ToString();
            dt_FinalAntiVirus.Text = c.DataTerminoAntiVirus.ToString();
            cb_SistemaOperacional.Text = c.MSWindows.ToString();
            cb_Antivirus.Text = c.AntiVirus.ToString();

            btn_NovoAlterar.Content = "Alterar";
            btn_CancelarRemover.Content = "Remover";
        }

        private void txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txt_Computador.Text.Length > 0 || txt_LicencaAntivirus.Text.Length > 0 || txt_LicencaOS.Text.Length > 0 || txt_MacAddress.Text.Length > 0)
            {
                btn_CancelarRemover.Content = "Cancelar";
            }
        }

        private void btn_CancelarRemover_Click(object sender, RoutedEventArgs e)
        {
            if (btn_CancelarRemover.Content.Equals("Cancelar"))
            {
                btn_NovoAlterar.Visibility = Visibility.Visible;
                btn_NovoAlterar.Content = "Novo";
                btn_SalvarAlterar.Visibility = Visibility.Collapsed;
                LimparComputador();
                habilitarDesabilitarCampos(false);
            }
            else if (btn_CancelarRemover.Content.Equals("Remover"))
            {
                if (MessageDialog.Show(Window.GetWindow(this), "Deseja realmente excluir este computador?", "Exclusão de contato", MessageBoxButton.YesNo, MessageBoxImage.Question) == true)
                {
                    Computador c = (Computador)dg_Computadores.SelectedItem;
                    IComputadorDAO Cdao = DAOFactory.InitComputadorDAO();
                    computadorSelect.IDComputador = c.IDComputador;
                    Cdao.Delete(computadorSelect);
                    LimparComputador();
                    //computadores = Cdao.GetByFK(clienteSelect.IDCliente);
                    dg_Computadores.ItemsSource = computadores;
                    dg_Computadores.Items.Refresh();
                    btn_NovoAlterar.Content = "Novo";
                }
            }
        }

        private void btn_NovoContrato_Click(object sender, RoutedEventArgs e)
        {
            habilitarContrato(true);
            if (btn_NovoContrato.Content.Equals("Novo"))
            {
                this.btn_NovoContrato.Visibility = Visibility.Collapsed;
                this.btn_SalvarContrato.Visibility = Visibility.Visible;
                this.btn_SalvarContrato.Content = "Salvar";
                this.btn_RemoverContrato.Content = "Cancelar";
            }
            else if (btn_NovoContrato.Content.Equals("Alterar"))
            {
                this.btn_NovoContrato.Visibility = Visibility.Collapsed;
                this.btn_SalvarContrato.Visibility = Visibility.Visible;
                this.btn_SalvarContrato.Content = "Alterar";
                this.btn_RemoverContrato.Content = "Cancelar";
            }
        }

        private void btn_SalvarContrato_Click(object sender, RoutedEventArgs e)
        {
            if (this.btn_SalvarContrato.Content.Equals("Salvar"))
            {
                salvarContrato();
            }
            else if (this.btn_SalvarContrato.Content.Equals("Alterar"))
            {
                alterarContrato();
            }

            txt_DescContrat.Text = String.Empty;
            cb_Ativo.SelectedIndex = -1;
            habilitarContrato(false);

            this.btn_NovoContrato.Visibility = Visibility.Visible;
            this.btn_SalvarContrato.Visibility = Visibility.Collapsed;

            IContratoDAO Cdao = DAOFactory.InitContratoDAO();
            //contratos = Cdao.GetByFK(clienteSelect.IDCliente);
            dg_Contrato.ItemsSource = contratos;
            dg_Contrato.Items.Refresh();
        }

        private void btn_RemoverContrato_Click(object sender, RoutedEventArgs e)
        {
            if (btn_RemoverContrato.Content.Equals("Remover"))
            {
                if (MessageDialog.Show(Window.GetWindow(this), "Deseja realmente excluir este contrato?", "Exclusão de contrato", MessageBoxButton.YesNo, MessageBoxImage.Question) == true)
                {
                    Contrato c = (Contrato)dg_Contrato.SelectedItem;
                    IContratoDAO Cdao = DAOFactory.InitContratoDAO();
                    contratoSelect = c;
                    Cdao.Delete(contratoSelect);
                    contratos.Remove(contratoSelect);
                    this.btn_NovoContrato.Visibility = Visibility.Visible;
                    txt_DescContrat.Text = String.Empty;
                    cb_Ativo.SelectedIndex = -1;
                    //contratos = Cdao.GetByFK(clienteSelect.IDCliente);
                    dg_Contrato.ItemsSource = contratos;
                    dg_Contrato.Items.Refresh();
                }
            }
            else if (btn_RemoverContrato.Content.Equals("Cancelar"))
            {
                btn_NovoContrato.Visibility = Visibility.Visible;
                btn_SalvarContrato.Visibility = Visibility.Collapsed;
                habilitarContrato(false);
                txt_DescContrat.Text = String.Empty;
                cb_Ativo.SelectedIndex = -1;
            }
        }

        private void dg_Contrato_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            txt_DescContrat.TextChanged -= new TextChangedEventHandler(txt_DescContrat_TextChanged);
            this.btn_NovoContrato.Visibility = Visibility.Visible;
            this.btn_NovoContrato.Content = "Alterar";
            this.btn_RemoverContrato.Content = "Remover";
            habilitarContrato(false);
            Contrato c = (Contrato)dg_Contrato.SelectedItem;
            txt_DescContrat.Text = c.Nome;
            if (c.Ativo == true)
            {
                cb_Ativo.SelectedIndex = 0;
            }
            else
            {
                cb_Ativo.SelectedIndex = 1;
            }
        }

        private void txt_DescContrat_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txt_DescContrat.Text.Length >= 1)
            {
                btn_RemoverContrato.Content = "Cancelar";
            }
            else
            {
                btn_RemoverContrato.Content = "Remover";
            }
        }

        private void salvarComputador()
        {
            computadorSelect.Cliente = clienteSelect;
            computadorSelect.Nome = txt_Computador.Text;
            computadorSelect.MSWindows = (MSWindows)cb_SistemaOperacional.SelectedItem;
            computadorSelect.LicencaWindows = txt_LicencaOS.Text;
            computadorSelect.MacAddress = txt_MacAddress.Text;
            computadorSelect.AntiVirus = (AntiVirus)cb_Antivirus.SelectedItem;
            computadorSelect.LicencaAntiVirus = txt_LicencaAntivirus.Text;
            computadorSelect.DataAquisicaoAntiVirus = dt_InicioAntiVirus.SelectedDate.Value;
            computadorSelect.DataTerminoAntiVirus = dt_FinalAntiVirus.SelectedDate.Value;
            IComputadorDAO Cdao = DAOFactory.InitComputadorDAO();

            if (string.IsNullOrEmpty(txt_Computador.Text.Trim()))
            {
                BallonDialog.Show("O campo Computador devem ser preenchido!", "Campo nulo ou inválido");
                return;
            }
            else if (cb_SistemaOperacional.SelectedIndex == -1)
            {
                BallonDialog.Show("Selecione um Sistema Opercional", "Seleção inválida");
                return;
            }
            else if (string.IsNullOrEmpty(txt_LicencaOS.Text.Trim()))
            {
                BallonDialog.Show("O campo Licença Sistema Operacional devem ser preenchido!", "Campo nulo ou inválido");
                return;
            }
            else if (string.IsNullOrEmpty(txt_MacAddress.Text.Trim()))
            {
                BallonDialog.Show("O campo MacAddress devem ser preenchido!", "Campo nulo ou inválido");
                return;
            }
            else if (cb_Antivirus.SelectedIndex == -1)
            {
                BallonDialog.Show("Selecione um Antivirus", "Seleção inválida");
                return;
            }
            else if (string.IsNullOrEmpty(txt_LicencaAntivirus.Text.Trim()))
            {
                BallonDialog.Show("O campo Licença Antivirus devem ser preenchido!", "Campo nulo ou inválido");
                return;
            }
            else if (computadorSelect != null)
            {
                BallonDialog.Show("Computador: " + computadorSelect.Nome + " salvo com sucesso.", "Salvo com sucesso");
                Cdao.Save(computadorSelect);
                btn_SalvarAlterar.Visibility = Visibility.Collapsed;
                btn_NovoAlterar.Visibility = Visibility.Visible;
                //computadores = Cdao.GetByFK(clienteSelect.IDCliente);
                dg_Computadores.ItemsSource = computadores;
                dg_Computadores.Items.Refresh();
                LimparComputador();
            }
        }

        private void alterarComputador()
        {
            Computador c = (Computador)dg_Computadores.SelectedItem;
            computadorSelect.IDComputador = c.IDComputador;
            computadorSelect.Cliente = clienteSelect;
            computadorSelect.Nome = txt_Computador.Text;
            computadorSelect.MSWindows = (MSWindows)cb_SistemaOperacional.SelectedItem;
            computadorSelect.LicencaWindows = txt_LicencaOS.Text;
            computadorSelect.MacAddress = txt_MacAddress.Text;
            computadorSelect.AntiVirus = (AntiVirus)cb_Antivirus.SelectedItem;
            computadorSelect.LicencaAntiVirus = txt_LicencaAntivirus.Text;
            computadorSelect.DataAquisicaoAntiVirus = dt_InicioAntiVirus.SelectedDate.Value;
            computadorSelect.DataTerminoAntiVirus = dt_FinalAntiVirus.SelectedDate.Value;
            IComputadorDAO Cdao = DAOFactory.InitComputadorDAO();

            if (string.IsNullOrEmpty(txt_Computador.Text.Trim()))
            {
                BallonDialog.Show("O campo Computador devem ser preenchido!", "Campo nulo ou inválido");
                return;
            }
            else if (cb_SistemaOperacional.SelectedIndex == -1)
            {
                BallonDialog.Show("Selecione um Sistema Opercional", "Seleção inválida");
                return;
            }
            else if (string.IsNullOrEmpty(txt_LicencaOS.Text.Trim()))
            {
                BallonDialog.Show("O campo Licença Sistema Operacional devem ser preenchido!", "Campo nulo ou inválido");
                return;
            }
            else if (string.IsNullOrEmpty(txt_MacAddress.Text.Trim()))
            {
                BallonDialog.Show("O campo MacAddress devem ser preenchido!", "Campo nulo ou inválido");
                return;
            }
            else if (cb_Antivirus.SelectedIndex == -1)
            {
                BallonDialog.Show("Selecione um Antivirus", "Seleção inválida");
                return;
            }
            else if (string.IsNullOrEmpty(txt_LicencaAntivirus.Text.Trim()))
            {
                BallonDialog.Show("O campo Licença Antivirus devem ser preenchido!", "Campo nulo ou inválido");
                return;
            }
            else if (computadorSelect != null)
            {
                BallonDialog.Show("Computador: " + computadorSelect.Nome + " alterado com sucesso.", "Alterado com sucesso");
                Cdao.Update(computadorSelect);
                habilitarDesabilitarCampos(false);
                //computadores = Cdao.GetByFK(clienteSelect.IDCliente);
                dg_Computadores.ItemsSource = computadores;
                dg_Computadores.Items.Refresh();
                btn_SalvarAlterar.Visibility = Visibility.Collapsed;
                btn_NovoAlterar.Visibility = Visibility.Visible;
                btn_NovoAlterar.Content = "Novo";
                LimparComputador();
            }
        }

        private void salvarContrato()
        {
            IContratoDAO Cdao = DAOFactory.InitContratoDAO();
            contratoSelect.Cliente = clienteSelect;
            contratoSelect.Nome = txt_DescContrat.Text;

            if (cb_Ativo.SelectionBoxItem.ToString().Equals("Sim"))
            {
                contratoSelect.Ativo = true;
            }
            else
            {
                contratoSelect.Ativo = false;
            }

            if (string.IsNullOrEmpty(txt_DescContrat.Text.Trim()))
            {
                BallonDialog.Show("O campo descrição deve ser preenchido !", "Campo Nulo ou Inválido");
                return;
            }
            else if (cb_Ativo.SelectedIndex == -1)
            {
                BallonDialog.Show("Selecione uma opção para o campo Ativo !", "Seleção inválida");
                return;
            }
            else if (contratoSelect != null)
            {
                BallonDialog.Show("Contrato: " + contratoSelect.Nome + " salvo com sucesso.", "Salvo com sucesso");
                Cdao.Save(contratoSelect);
            }
        }

        private void alterarContrato()
        {
            IContratoDAO Cdao = DAOFactory.InitContratoDAO();
            Contrato c = (Contrato)dg_Contrato.SelectedItem;
            contratoSelect.Cliente = clienteSelect;
            contratoSelect.IDContrato = c.IDContrato;
            contratoSelect.Nome = txt_DescContrat.Text;

            if (cb_Ativo.SelectionBoxItem.ToString().Equals("Sim"))
            {
                contratoSelect.Ativo = true;
            }
            else
            {
                contratoSelect.Ativo = false;
            }

            if (string.IsNullOrEmpty(txt_DescContrat.Text.Trim()))
            {
                BallonDialog.Show("O campo descrição deve ser preenchido !", "Campo Nulo ou Inválido");
                return;
            }
            else if (cb_Ativo.SelectedIndex == -1)
            {
                BallonDialog.Show("Selecione uma opção para o campo Ativo !", "Seleção inválida");
                return;
            }
            else if (contratoSelect != null)
            {
                BallonDialog.Show("Contrato: " + contratoSelect.Nome + " alterado com sucesso.", "Alterado com sucesso");
                Cdao.Update(contratoSelect);
            }
        }
    }

}
