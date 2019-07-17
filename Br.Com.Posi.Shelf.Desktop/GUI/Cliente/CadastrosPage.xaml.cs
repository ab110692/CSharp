using Br.Com.Posi.Shelf.DAO;
using Br.Com.Posi.Shelf.Desktop.GUI.Outro;
using Br.Com.Posi.Shelf.Model;
using Br.Com.Posi.Util;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Br.Com.Posi.Shelf.Desktop.GUI.Cliente
{
    /// <summary>
    /// Interaction logic for RedePage.xaml
    /// </summary>
    public partial class CadastrosPage : Page
    {
        private static CadastrosPage _instance;

        //Model
        Rede redeSelect;
        AntiVirus antivirusSelect;
        MSWindows msWindowsSelect;

        //DAO
        IRedeDAO daoRede;
        IAntiVirusDAO daoAntiVirus;
        IMSWindowsDAO daoMSWindows;

        //List
        List<Rede> redes;
        List<AntiVirus> antivirus;
        List<MSWindows> windows;

        
        private static CadastrosPage GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CadastrosPage();
            }
            return _instance;
        }

        public CadastrosPage()
        {
            InitializeComponent();

            redeSelect = new Rede();
            antivirusSelect = new AntiVirus();
            msWindowsSelect = new MSWindows();

            redes = new List<Rede>();
            antivirus = new List<AntiVirus>();
            windows = new List<MSWindows>();

            daoRede = DAOFactory.InitRedeDAO();
            daoAntiVirus = DAOFactory.InitAntivirusDAO();
            daoMSWindows = DAOFactory.InitMSWindowsDAO();

            CarregarRede();
            CarregarAntivirus();
            CarregarMSWindows();

            habilitarDesabilitarRede(false);
            habilitarDesabilitarAntivirus(false);
            habilitarDesabilitarSistemaOperacional(false);
            
        }

        #region Sistema Operacional
        private void CarregarMSWindows()
        {
            windows = daoMSWindows.GetList();
            gd_SistemaOperacional.ItemsSource = windows;
            gd_SistemaOperacional.Items.Refresh();
        }
        private int controleOS = 1;
        private void abreOS()
        {
            if (controleOS == 0)
            {
                (FindResource("fechaOS") as Storyboard).Begin();
                (FindResource("sobeAnti") as Storyboard).Begin();
                controleOS = 1;
            }
            else if (controleOS == 1)
            {
                (FindResource("abreOS") as Storyboard).Begin();
                (FindResource("desceAnti") as Storyboard).Begin();
                controleOS = 0;
            }
        }

        private void btn_OS_Click(object sender, RoutedEventArgs e)
        {
            abreOS();
        }

        private void gd_SistemaOperacional_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gd_SistemaOperacional.SelectedIndex != -1)
            {
                txt_SistemaOperacional.TextChanged -= new TextChangedEventHandler(txt_SistemaOperacional_TextChanged);
                this.btn_NovoSistemaOperacional.Visibility = Visibility.Visible;
                this.btn_AdicionarSistemaOperacional.Visibility = Visibility.Collapsed;
                this.btn_NovoSistemaOperacional.Content = "Alterar";
                this.btn_RemoverSistemaOperacional.Content = "Remover";
                msWindowsSelect = (MSWindows)this.gd_SistemaOperacional.SelectedItem;
                this.txt_SistemaOperacional.Text = msWindowsSelect.Nome;
            }
        }

        private void habilitarDesabilitarSistemaOperacional(bool b)
        {
            this.txt_SistemaOperacional.IsEnabled = b;
            if (b == false)
            {
                this.txt_SistemaOperacional.Background = new SolidColorBrush(Color.FromRgb(204, 204, 204));
            }
            else
            {
                this.txt_SistemaOperacional.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255)); ;
            }
        }

        private void ClearSistemaOperacional()
        {
            this.txt_SistemaOperacional.Text = string.Empty;
        }

        private void btn_NovoSistemaOperacional_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Content.Equals("Alterar"))
            {
                this.habilitarDesabilitarSistemaOperacional(true);
                this.btn_NovoSistemaOperacional.Visibility = Visibility.Collapsed;
                this.btn_AdicionarSistemaOperacional.Visibility = Visibility.Visible;
                this.btn_AdicionarSistemaOperacional.Content = "Alterar";
                this.btn_RemoverSistemaOperacional.Content = "Cancelar";
            }
            else
            {
                this.habilitarDesabilitarSistemaOperacional(true);
                this.btn_RemoverSistemaOperacional.Content = "Cancelar";
                this.btn_NovoSistemaOperacional.Visibility = Visibility.Collapsed;
                this.btn_AdicionarSistemaOperacional.Visibility = Visibility.Visible;
            }
        }

        private void btn_AdicionarSistemaOperacional_Click(object sender, RoutedEventArgs e)
        {
            this.btn_NovoSistemaOperacional.Visibility = Visibility.Visible;
            this.btn_AdicionarSistemaOperacional.Visibility = Visibility.Collapsed;
            if ((sender as Button).Content.Equals("Salvar"))
            {
                try
                {
                    if (string.IsNullOrEmpty(txt_SistemaOperacional.Text.Trim()))
                    {
                        BallonDialog.Show("O campo Sistema Operacional deve ser preenchido !", "Campo nulo ou inválido");
                        this.btn_NovoSistemaOperacional.Visibility = Visibility.Visible;
                        this.btn_AdicionarSistemaOperacional.Visibility = Visibility.Collapsed;
                        this.habilitarDesabilitarSistemaOperacional(false);
                        return;
                    }
                    else if (msWindowsSelect != null)
                    {
                        BallonDialog.Show("Sistema Operacional salvo com sucesso !", "Mensagem");
                        (sender as Button).Content = "Salvar";
                        habilitarDesabilitarSistemaOperacional(false);
                        windows.Add(msWindowsSelect);
                        salvarSistemaOperacional();
                        CarregarMSWindows();
                    }
                    else
                    {
                        this.btn_NovoSistemaOperacional.Visibility = Visibility.Visible;
                        this.btn_AdicionarSistemaOperacional.Visibility = Visibility.Collapsed;
                        this.gd_SistemaOperacional.Items.Refresh();
                        this.ClearSistemaOperacional();
                    }
                }
                catch (Exception ex)
                {
                    BallonDialog.Show(ex.Message, "Alerta");
                    this.btn_NovoSistemaOperacional.Visibility = Visibility.Visible;
                    this.btn_AdicionarSistemaOperacional.Visibility = Visibility.Collapsed;
                    this.habilitarDesabilitarSistemaOperacional(false);
                }
            }
            else if ((sender as Button).Content.Equals("Alterar"))
            {
                msWindowsSelect.Nome = txt_SistemaOperacional.Text;
                msWindowsSelect.IDWindows = msWindowsSelect.IDWindows;
                if (redeSelect != null)
                {
                    BallonDialog.Show("Sistema Operacional alterado com sucesso !", "Mensagem");
                    this.btn_NovoSistemaOperacional.Content = "Novo";

                    habilitarDesabilitarSistemaOperacional(false);
                    daoMSWindows.Update(msWindowsSelect);
                }
                this.gd_SistemaOperacional.Items.Refresh();
                this.ClearSistemaOperacional();
            }
        }

        private void txt_SistemaOperacional_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txt_SistemaOperacional.Text.Length >= 1)
            {
                this.btn_RemoverSistemaOperacional.Content = "Cancelar";
            }
            else
            {
                this.btn_RemoverSistemaOperacional.Content = "Remover";
            }
        }

        private void btn_RemoverSistemaOperacional_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((sender as Button).Content.Equals("Cancelar"))
                {
                    this.btn_NovoSistemaOperacional.Visibility = Visibility.Visible;
                    this.btn_AdicionarSistemaOperacional.Visibility = Visibility.Collapsed;
                    habilitarDesabilitarSistemaOperacional(false);
                    CarregarMSWindows();
                    this.ClearSistemaOperacional();
                }
                else if (gd_SistemaOperacional.SelectedIndex != -1)
                {
                    msWindowsSelect.Nome = txt_Antivirus.Text;
                    msWindowsSelect.IDWindows = msWindowsSelect.IDWindows;
                    if (msWindowsSelect != null)
                    {
                        daoMSWindows.Delete(msWindowsSelect);
                        BallonDialog.Show("Sistema Operacional removido com sucesso !", "Aviso");
                        windows.Remove(msWindowsSelect);
                        this.btn_NovoSistemaOperacional.Content = "Novo";
                        this.gd_SistemaOperacional.Items.Refresh();
                        this.ClearSistemaOperacional();
                    }
                    else
                    {
                        BallonDialog.Show("Ocorreu um erro ao remover o Sistema Operacional", "Alerta");
                    }
                    this.habilitarDesabilitarSistemaOperacional(false);
                    this.ClearSistemaOperacional();
                    this.gd_SistemaOperacional.Items.Refresh();
                }
            }
            catch (Exception)
            {
                BallonDialog.Show("Não é possível remover o sistema operacional, pois esta sendo utilizado no cadastro de algum cliente.", "Aviso");
                this.ClearSistemaOperacional();
            }


        }

        private void salvarSistemaOperacional()
        {
            msWindowsSelect = new MSWindows();
            msWindowsSelect.Nome = txt_SistemaOperacional.Text;
            msWindowsSelect = daoMSWindows.Save(msWindowsSelect);
        }
        #endregion

        #region Antivirus
        private void CarregarAntivirus()
        {
            antivirus = daoAntiVirus.GetList();
            gd_Antivirus.ItemsSource = antivirus;
            gd_Antivirus.Items.Refresh();
        }
        private int controleAntivirus = 1;
        private void abreAnt()
        {
            if (controleAntivirus == 0)
            {
                (FindResource("fechaAnt") as Storyboard).Begin();

                controleAntivirus = 1;
            }
            else if (controleAntivirus == 1)
            {
                (FindResource("abreAnt") as Storyboard).Begin();

                controleAntivirus = 0;
            }
        }

        private void btn_Antivirus_Click(object sender, RoutedEventArgs e)
        {
            abreAnt();
        }
        private void gd_Antivirus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gd_Antivirus.SelectedIndex != -1)
            {
                this.txt_Antivirus.TextChanged -= new TextChangedEventHandler(txt_Antivirus_TextChanged);
                this.btn_NovoAntivirus.Visibility = Visibility.Visible;
                this.btn_AdicionarAntivirus.Visibility = Visibility.Collapsed;
                this.btn_NovoAntivirus.Content = "Alterar";
                this.btn_RemoverAntivirus.Content = "Remover";
                this.antivirusSelect = (AntiVirus)this.gd_Antivirus.SelectedItem;
                this.txt_Antivirus.Text = antivirusSelect.Nome;
            }
        }

        private void ClearAntivirus()
        {
            this.txt_Antivirus.Text = string.Empty;
        }

        private void habilitarDesabilitarAntivirus(bool b)
        {
            this.txt_Antivirus.IsEnabled = b;
            if (b == false)
            {
                this.txt_Antivirus.Background = new SolidColorBrush(Color.FromRgb(204, 204, 204));
            }
            else
            {
                this.txt_Antivirus.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255)); ;
            }
        }

        private void btn_NovoAntivirus_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Content.Equals("Alterar"))
            {
                this.habilitarDesabilitarAntivirus(true);
                this.btn_NovoAntivirus.Visibility = Visibility.Collapsed;
                this.btn_AdicionarAntivirus.Visibility = Visibility.Visible;
                this.btn_AdicionarAntivirus.Content = "Alterar";
                this.btn_RemoverAntivirus.Content = "Cancelar";
            }
            else
            {
                this.habilitarDesabilitarAntivirus(true);
                this.btn_RemoverAntivirus.Content = "Cancelar";
                this.btn_NovoAntivirus.Visibility = Visibility.Collapsed;
                this.btn_AdicionarAntivirus.Visibility = Visibility.Visible;
            }
        }

        private void btn_AdicionarAntivirus_Click(object sender, RoutedEventArgs e)
        {
            this.btn_NovoAntivirus.Visibility = Visibility.Visible;
            this.btn_AdicionarAntivirus.Visibility = Visibility.Collapsed;
            if ((sender as Button).Content.Equals("Salvar"))
            {
                try
                {
                    if (string.IsNullOrEmpty(txt_Antivirus.Text.Trim()))
                    {
                        BallonDialog.Show("O campo Antivirus deve ser preenchido !", "Campo nulo ou inválido");
                        this.btn_NovoAntivirus.Visibility = Visibility.Visible;
                        this.btn_AdicionarAntivirus.Visibility = Visibility.Collapsed;
                        habilitarDesabilitarAntivirus(false);
                        return;
                    }
                    else if (antivirusSelect != null)
                    {
                        BallonDialog.Show("Antivirus salvo com sucesso !", "Mensagem");
                        (sender as Button).Content = "Salvar";

                        habilitarDesabilitarAntivirus(false);
                        antivirus.Add(antivirusSelect);
                        salvarAntivirus();
                        CarregarAntivirus();
                    }
                    this.btn_NovoAntivirus.Visibility = Visibility.Visible;
                    this.btn_AdicionarAntivirus.Visibility = Visibility.Collapsed;
                    this.gd_Antivirus.Items.Refresh();
                    this.ClearAntivirus();
                }
                catch (Exception ex)
                {
                    BallonDialog.Show(ex.Message, "Alerta");
                    this.btn_NovoAntivirus.Visibility = Visibility.Visible;
                    this.btn_AdicionarAntivirus.Visibility = Visibility.Collapsed;
                    this.habilitarDesabilitarAntivirus(false);
                }
            }
            else if ((sender as Button).Content.Equals("Alterar"))
            {
                antivirusSelect.Nome = txt_Antivirus.Text;
                antivirusSelect.IDAntiVirus = antivirusSelect.IDAntiVirus;
                if (redeSelect != null)
                {
                    BallonDialog.Show("Antivirus alterado com sucesso !", "Mensagem");
                    this.btn_NovoAntivirus.Content = "Novo";

                    habilitarDesabilitarAntivirus(false);
                    daoAntiVirus.Update(antivirusSelect);
                }
                this.gd_Antivirus.Items.Refresh();
                this.ClearAntivirus();
            }
        }

        private void txt_Antivirus_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txt_Antivirus.Text.Length >= 1)
            {
                this.btn_RemoverAntivirus.Content = "Cancelar";
            }
            else
            {
                this.btn_RemoverAntivirus.Content = "Remover";
            }
        }

        private void btn_RemoverAntivirus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((sender as Button).Content.Equals("Cancelar"))
                {
                    this.btn_NovoAntivirus.Visibility = Visibility.Visible;
                    this.btn_AdicionarAntivirus.Visibility = Visibility.Collapsed;
                    habilitarDesabilitarAntivirus(false);
                    CarregarAntivirus();
                    this.ClearAntivirus();
                }
                else if (gd_Antivirus.SelectedIndex != -1)
                {
                    antivirusSelect.Nome = txt_Antivirus.Text;
                    antivirusSelect.IDAntiVirus = antivirusSelect.IDAntiVirus;
                    if (antivirusSelect != null)
                    {
                        daoAntiVirus.Delete(antivirusSelect);
                        BallonDialog.Show("Antivirus removido com sucesso !", "Aviso");
                        antivirus.Remove(antivirusSelect);
                        this.btn_NovoAntivirus.Content = "Novo";
                        this.gd_Antivirus.Items.Refresh();
                        this.ClearAntivirus();
                    }
                    else
                    {
                        BallonDialog.Show("Ocorreu um erro ao remover o Antivirus", "Alerta");
                    }
                    habilitarDesabilitarAntivirus(false);
                    this.ClearAntivirus();
                    this.gd_Antivirus.Items.Refresh();
                }
            }
            catch (Exception)
            {
                BallonDialog.Show("Não é possível remover o antivirus, pois esta sendo utilizado no cadastro de algum cliente.", "Aviso");
                this.ClearAntivirus();
            }
        }

        private void salvarAntivirus()
        {
            antivirusSelect = new AntiVirus();
            antivirusSelect.Nome = txt_Antivirus.Text;
            antivirusSelect = daoAntiVirus.Save(antivirusSelect);
        }
        #endregion

        #region Rede



        private int controleRede = 1;
        private void abreRede()
        {
            
            if (controleRede == 0)
            {
                (FindResource("fechaRede") as Storyboard).Begin();
                (FindResource("sobeOS") as Storyboard).Begin();
                controleRede = 1;
            }
            else if (controleRede == 1)
            {
                (FindResource("abreRede") as Storyboard).Begin();
                (FindResource("desceOS") as Storyboard).Begin();
                controleRede = 0;
            }

        }
        
        private void btn_Rede_Click(object sender, RoutedEventArgs e)
        {
            abreRede();
        }
        
        
        
       
        private void CarregarRede()
        {
                  
            redes = daoRede.GetList();
            dg_Rede.ItemsSource = redes;
            dg_Rede.Items.Refresh();
            RedePage rede = new RedePage();
           // f_Rede.Content = rede;
        }

        private void dg_Rede_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg_Rede.SelectedIndex != -1)
            {
                txt_Codigo.TextChanged -= new TextChangedEventHandler(txt_Codigo_TextChanged);
                this.btn_NovaRede.Visibility = Visibility.Visible;
                this.btn_AdicionarRede.Visibility = Visibility.Collapsed;

                this.btn_NovaRede.Content = "Alterar";
                this.btn_RemoverRede.Content = "Remover";
                redeSelect = (Rede)this.dg_Rede.SelectedItem;
                this.txt_Nome.Text = redeSelect.Nome;
                this.txt_Codigo.Text = redeSelect.Codigo.ToString();
            }
        }

        private void habilitarDesabilitarRede(bool b)
        {
            this.txt_Codigo.IsEnabled = b;
            this.txt_Nome.IsEnabled = b;
            this.txt_SistemaOperacional.IsEnabled = b;
            if (b == false)
            {
                this.txt_Codigo.Background = new SolidColorBrush(Color.FromRgb(204, 204, 204));
                this.txt_Nome.Background = new SolidColorBrush(Color.FromRgb(204, 204, 204));
            }
            else
            {
                this.txt_Codigo.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255)); ;
                this.txt_Nome.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255)); ;
            }
        }

        private void ClearRede()
        {
            this.txt_Codigo.Text = string.Empty;
            this.txt_Nome.Text = string.Empty;
        }

        private void btn_NovaRede_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Content.Equals("Alterar"))
            {
                habilitarDesabilitarRede(true);
                this.btn_NovaRede.Visibility = Visibility.Collapsed;
                this.btn_AdicionarRede.Visibility = Visibility.Visible;
                this.btn_AdicionarRede.Content = "Alterar";
                this.btn_RemoverRede.Content = "Cancelar";
            }
            else
            {
                habilitarDesabilitarRede(true);
                this.btn_RemoverRede.Content = "Cancelar";
                this.btn_NovaRede.Visibility = Visibility.Collapsed;
                this.btn_AdicionarRede.Visibility = Visibility.Visible;
            }
        }

        private void btn_AdicionarRede_Click(object sender, RoutedEventArgs e)
        {
            this.btn_NovaRede.Visibility = Visibility.Visible;
            this.btn_AdicionarRede.Visibility = Visibility.Collapsed;
            if ((sender as Button).Content.Equals("Salvar"))
            {
                try
                {
                    redeSelect = new Rede();

                    if (string.IsNullOrEmpty(txt_Nome.Text.Trim()) || string.IsNullOrEmpty(txt_Codigo.Text.Trim()))
                    {
                        BallonDialog.Show("Os campos Nome e Codigo devem ser preenchidos !", "Campo nulo ou inválido");
                        this.btn_NovaRede.Visibility = Visibility.Visible;
                        this.btn_AdicionarRede.Visibility = Visibility.Collapsed;
                        habilitarDesabilitarRede(false);
                        return;
                    }
                    else if (redeSelect != null)
                    {
                        BallonDialog.Show("Rede salvo com sucesso !", "Mensagem");
                        (sender as Button).Content = "Salvar";

                        habilitarDesabilitarRede(false);
                        redes.Add(redeSelect);
                        salvarRede();
                        CarregarRede();
                    }

                    this.btn_NovaRede.Visibility = Visibility.Visible;
                    this.btn_AdicionarRede.Visibility = Visibility.Collapsed;
                    this.dg_Rede.Items.Refresh();
                    this.ClearRede();

                }
                catch (Exception ex)
                {
                    BallonDialog.Show(ex.Message, "Alerta");
                    this.btn_NovaRede.Visibility = Visibility.Visible;
                    this.btn_AdicionarRede.Visibility = Visibility.Collapsed;
                    habilitarDesabilitarRede(false);
                }

            }
            else if ((sender as Button).Content.Equals("Alterar"))
            {
                if (string.IsNullOrEmpty(txt_Nome.Text.Trim()) || string.IsNullOrEmpty(txt_Codigo.Text.Trim()))
                {
                    BallonDialog.Show("Não há Rede selecionada  !", "Impossível atualizar");
                    this.btn_NovaRede.Visibility = Visibility.Visible;
                    this.btn_AdicionarRede.Visibility = Visibility.Collapsed;
                    habilitarDesabilitarRede(false);
                    return;
                }
                redeSelect.Codigo = Convert.ToInt64(txt_Codigo.Text);
                redeSelect.Nome = txt_Nome.Text;
                redeSelect.IDRede = redeSelect.IDRede;
                if (redeSelect != null)
                {
                    BallonDialog.Show("Rede alterada com sucesso !", "Mensagem");
                    this.btn_NovaRede.Content = "Novo";

                    habilitarDesabilitarRede(false);
                    daoRede.Update(redeSelect);
                }
                this.dg_Rede.Items.Refresh();
                this.ClearRede();
            }

        }

        private void txt_Codigo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txt_Codigo.Text.Length >= 1)
            {
                this.btn_RemoverRede.Content = "Cancelar";
            }
            else
            {
                this.btn_RemoverRede.Content = "Remover";
            }
        }

        private void btn_RemoverRede_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((sender as Button).Content.Equals("Cancelar"))
                {
                    this.btn_NovaRede.Visibility = Visibility.Visible;
                    this.btn_AdicionarRede.Visibility = Visibility.Collapsed;
                    habilitarDesabilitarRede(false);
                    CarregarRede();
                    this.ClearRede();
                }
                else if (dg_Rede.SelectedIndex != -1)
                {
                    redeSelect.Codigo = Convert.ToInt64(txt_Codigo.Text);
                    redeSelect.Nome = txt_Nome.Text;
                    redeSelect.IDRede = redeSelect.IDRede;
                    if (redeSelect != null)
                    {
                        daoRede.Delete(redeSelect);
                        BallonDialog.Show("Rede removido com sucesso !", "Aviso");
                        redes.Remove(redeSelect);
                        this.btn_NovaRede.Content = "Novo";
                        this.dg_Rede.Items.Refresh();
                        this.ClearRede();
                    }
                    else
                    {
                        BallonDialog.Show("Ocorreu um erro ao remover a rede", "Alerta");
                    }
                    habilitarDesabilitarRede(false);
                    this.ClearRede();
                    this.dg_Rede.Items.Refresh();
                }
            }
            catch (Exception)
            {
                BallonDialog.Show("Não é possível remover a rede, pois esta sendo utilizado no cadastro de algum cliente.", "Aviso");
                this.ClearRede();
            }
        }

        private void salvarRede()
        {
            redeSelect = new Rede();
            redeSelect.Codigo = Convert.ToInt64(txt_Codigo.Text);
            redeSelect.Nome = txt_Nome.Text;
            redeSelect = daoRede.Save(redeSelect);
        }
        #endregion

        private void btn_Voltar_Click(object sender, RoutedEventArgs e)
        {
            if (btn_AdicionarAntivirus.Visibility == Visibility.Visible && !string.IsNullOrEmpty(txt_Antivirus.Text.Trim()))
            {
                if (MessageDialog.Show(Window.GetWindow(this), "Foi identificado que houveram alterações que ainda não foram salvas, deseja salva-las?", "Sair", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == true)
                {
                    salvarAntivirus();
                }
            }
            else if (btn_AdicionarRede.Visibility == Visibility.Visible && !string.IsNullOrEmpty(txt_Nome.Text.Trim()) && !string.IsNullOrEmpty(txt_Codigo.Text.Trim()))
            {
                if (MessageDialog.Show(Window.GetWindow(this), "Foi identificado que houveram alterações que ainda não foram salvas, deseja salva-las?", "Sair", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == true)
                {
                    salvarRede();
                }
            }
            else if (btn_AdicionarSistemaOperacional.Visibility == Visibility.Visible && !string.IsNullOrEmpty(txt_SistemaOperacional.Text.Trim()))
            {
                if (MessageDialog.Show(Window.GetWindow(this), "Foi identificado que houveram alterações que ainda não foram salvas, deseja salva-las?", "Sair", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == true)
                {
                    salvarSistemaOperacional();
                }
            }
            if ((sender as Button).Content.Equals("Voltar"))
            {
                PrincipalWindow.GetInstance().Main.Navegate(LocalizarClientePage.GetInstance());
            }
            else if ((sender as Button).Content.Equals("Cancelar"))
            {
                habilitarDesabilitarRede(false);
                this.ClearRede();
                this.btn_Voltar.Content = "Voltar";
            }
        }

        
    }
}
