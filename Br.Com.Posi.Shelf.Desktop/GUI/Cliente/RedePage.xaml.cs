using Br.Com.Posi.Shelf.DAO;
using Br.Com.Posi.Shelf.Model;
using Br.Com.Posi.Util;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Br.Com.Posi.Shelf.Desktop.GUI.Cliente
{
    /// <summary>
    /// Interaction logic for RedePage.xaml
    /// </summary>
    /// 
    
    public partial class RedePage : Page
    {
        Rede redeSelect;
        IRedeDAO daoRede;
        List<Rede> redes;
        public RedePage()
        {
            InitializeComponent();
            redeSelect = new Rede();
            redes = new List<Rede>();
            daoRede = DAOFactory.InitRedeDAO();
            CarregarRede();
        }



        #region Rede

        private void btn_Rede_Click(object sender, RoutedEventArgs e)
        {
            (FindResource("FechaRede") as Storyboard).Begin();
        }
        private void CarregarRede()
        {

            redes = daoRede.GetList();
            dg_Rede.ItemsSource = redes;
            dg_Rede.Items.Refresh();
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
           // this.txt_SistemaOperacional.IsEnabled = b;
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



    }
}
