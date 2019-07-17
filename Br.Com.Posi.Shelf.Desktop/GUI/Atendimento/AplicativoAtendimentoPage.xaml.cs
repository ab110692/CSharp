using Br.Com.Posi.Shelf.DAO;
using Br.Com.Posi.Shelf.Desktop.GUI.Outro;
using Br.Com.Posi.Shelf.Model;
using Br.Com.Posi.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Br.Com.Posi.Shelf.Desktop.GUI.Atendimento
{
    /// <summary>
    /// Interaction logic for AplicativoAtendimentoPage.xaml
    /// </summary>
    public partial class AplicativoAtendimentoPage : Page
    {
        //select
        private Aplicativo aplicativoSelect;
        private Versao versaoSelect;
        private Aplicativo aplicativoComboBoxSelect;

        //DAO
        private IAplicativoDAO daoAplicativo;
        private IVersaoDAO daoVersao;

        //list
        private List<Aplicativo> aplicativos;

        public AplicativoAtendimentoPage()
        {
            InitializeComponent();

            daoAplicativo = DAOFactory.InitAplicativoDAO();
            daoVersao = DAOFactory.InitVersaoDAO();

            aplicativos = new List<Aplicativo>();

            aplicativos.AddRange(daoAplicativo.GetList());

            dgAplicativo.ItemsSource = aplicativos;
            cbAplicativo.ItemsSource = aplicativos;

            Load();
        }

        private void Load()
        {
            dgAplicativo.Items.Refresh();
            cbAplicativo.Items.Refresh();
            dgVersao.Items.Refresh();
        }

        #region Aplicativo

        private void dgAplicativo_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if ((sender as DataGrid).SelectedIndex >= 0)
            {
                aplicativoSelect = dgAplicativo.SelectedItem as Aplicativo;
                txtAplicativo.Text = aplicativoSelect.Descricao;
                this.btnRemoverAplicativo.Content = "Remover";
                MyComponentsUtil.IsEnableComponents(false, btnCadastroAplicativo);
                MyComponentsUtil.IsEnableComponents(true, btnNovoAplicativo, txtAplicativo, btnAlterarAplicativo, btnRemoverAplicativo);
            }
        }

        private void btnNovoAplicativo_Click(object sender, RoutedEventArgs e)
        {
            aplicativoSelect = new Aplicativo();
            txtAplicativo.Text = string.Empty;
            this.btnRemoverAplicativo.Content = "Cancelar";
            MyComponentsUtil.IsEnableComponents(true, txtAplicativo, btnCadastroAplicativo, btnRemoverAplicativo);
            MyComponentsUtil.IsEnableComponents(false, btnNovoAplicativo, btnAlterarAplicativo);
        }

        private void btnCadastroAplicativo_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtAplicativo.Text.Trim()))
            {
                BallonDialog.Show("Necessario preencher o campo descrição !", "Alerta !");
                return;
            }
            if (aplicativos.Where(apli => apli.Descricao.Equals(txtAplicativo.Text.Trim(), StringComparison.OrdinalIgnoreCase)).Any())
            {
                BallonDialog.Show("Aplicativo já cadastrado !", "Alerta !");
                return;
            }

            try
            {
                aplicativoSelect = new Aplicativo();
                aplicativoSelect.Descricao = txtAplicativo.Text.Trim();
                aplicativoSelect = daoAplicativo.SaveOrUpdate(aplicativoSelect);
                aplicativos.Add(aplicativoSelect);
                txtAplicativo.Text = string.Empty;
                MyComponentsUtil.IsEnableComponents(false, btnCadastroAplicativo, txtAplicativo, btnAlterarAplicativo, btnRemoverAplicativo);
                MyComponentsUtil.IsEnableComponents(true, btnNovoAplicativo);
                this.btnRemoverAplicativo.Content = "Remover";
                BallonDialog.Show($"{aplicativoSelect.Descricao} cadastrado com sucesso !", "Cadastro");
                this.Load();
            }
            catch (Exception ex)
            {
                BallonDialog.Show(ex.Message.ToString(), "Erro no cadastro");
            }
        }

        private void btnAlterarAplicativo_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtAplicativo.Text.Trim()))
            {
                BallonDialog.Show("Necessario preencher o campo descrição !", "Alerta !");
                return;
            }
            if (dgAplicativo.SelectedIndex == -1)
            {
                BallonDialog.Show("Necessario selecionar um item da tabela !", "Alerta !");
                return;
            }

            try
            {
                aplicativoSelect.Descricao = txtAplicativo.Text.Trim();
                aplicativoSelect = daoAplicativo.SaveOrUpdate(aplicativoSelect);
                txtAplicativo.Text = string.Empty;
                MyComponentsUtil.IsEnableComponents(false, btnCadastroAplicativo, txtAplicativo, btnAlterarAplicativo, btnRemoverAplicativo);
                MyComponentsUtil.IsEnableComponents(true, btnNovoAplicativo);
                BallonDialog.Show($"{aplicativoSelect.Descricao} modificado com sucesso !", "Modificação");
                this.Load();
            }
            catch (Exception ex)
            {
                BallonDialog.Show(ex.Message.ToString(), "Erro na modificação");
            }
        }

        private void btnRemoverAplicativo_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Content.Equals("Cancelar"))
            {
                MyComponentsUtil.IsEnableComponents(false, btnCadastroAplicativo, txtAplicativo, btnAlterarAplicativo, btnRemoverAplicativo);
                MyComponentsUtil.IsEnableComponents(true, btnNovoAplicativo);
                (sender as Button).Content = "Remover";
                txtAplicativo.Text = string.Empty;
                return;
            }
            if (string.IsNullOrEmpty(txtAplicativo.Text.Trim()))
            {
                BallonDialog.Show("Necessario preencher o campo descrição !", "Alerta !");
                return;
            }
            if (dgAplicativo.SelectedIndex == -1)
            {
                BallonDialog.Show("Necessario selecionar um item da tabela !", "Alerta !");
                return;
            }

            try
            {
                if (MessageDialog.Show(Window.GetWindow(this), $"Deseja excluir {aplicativoSelect.Descricao} ?", "Excluir", MessageBoxButton.YesNo, MessageBoxImage.Question))
                {
                    aplicativoSelect.Descricao = txtAplicativo.Text.Trim();
                    if (daoAplicativo.Delete(aplicativoSelect))
                    {
                        txtAplicativo.Text = string.Empty;
                        MyComponentsUtil.IsEnableComponents(false, btnAlterarAplicativo, btnRemoverAplicativo, txtAplicativo);
                        BallonDialog.Show($"{aplicativoSelect.Descricao} removido com sucesso !", "Remoção");
                        aplicativos.Remove(aplicativoSelect);
                        Load();
                    }
                    else
                    {
                        BallonDialog.Show($"Ocorreu um erro na remoção do seguinte aplicativo: {aplicativoSelect.Descricao}", "Erro na modificação");
                    }
                }
            }
            catch (Exception ex)
            {
                BallonDialog.Show(ex.Message.ToString(), "Erro na remoção");
            }
        }
        #endregion

        #region Versão
        private void dgVersao_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if ((sender as DataGrid).SelectedIndex >= 0)
            {
                versaoSelect = ((sender as DataGrid).SelectedItem as Versao);
                this.txtVersao.Text = versaoSelect.VersaoSistema;
                MyComponentsUtil.IsEnableComponents(true, this, txtVersao, this.btnNovaVersao, this.btnAlterarVersao, this.btnRemoverVersao);
            }
        }

        private void cbAplicativo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            aplicativoComboBoxSelect = cbAplicativo.SelectedItem as Aplicativo;
            dgVersao.ItemsSource = aplicativoComboBoxSelect.Versoes;
            this.txtVersao.Text = string.Empty;
            MyComponentsUtil.IsEnableComponents(true, btnNovaVersao);
            MyComponentsUtil.IsEnableComponents(false, txtVersao, btnCadastroVersao, btnAlterarVersao, btnRemoverVersao);
        }

        private void btnNovaVersao_Click(object sender, RoutedEventArgs e)
        {
            MyComponentsUtil.IsEnableComponents(true, txtVersao, btnCadastroVersao, btnRemoverVersao);
            MyComponentsUtil.IsEnableComponents(false, btnNovaVersao, btnAlterarVersao);
            this.btnRemoverVersao.Content = "Cancelar";
        }

        private void btnCadastroVersao_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtVersao.Text.Trim()))
            {
                BallonDialog.Show("Necessario preencher o campo número da versão !", "Alerta !");
                return;
            }
            if (aplicativoComboBoxSelect.Versoes.Where(ver => ver.VersaoSistema.Equals(txtVersao.Text.Trim(), StringComparison.OrdinalIgnoreCase)).Any())
            {
                BallonDialog.Show("Versão já cadastrado !", "Alerta !");
                return;
            }

            try
            {
                versaoSelect = new Versao();
                versaoSelect.Aplicativo = aplicativoComboBoxSelect;
                versaoSelect.VersaoSistema = txtVersao.Text;
                versaoSelect = daoVersao.Save(versaoSelect);
                aplicativos.Where(apli => apli.IDAplicativo == versaoSelect.Aplicativo.IDAplicativo).Single().Versoes.Add(versaoSelect);
                this.btnRemoverVersao.Content = "Remover";
                this.txtVersao.Text = string.Empty;
                MyComponentsUtil.IsEnableComponents(true, btnNovaVersao);
                MyComponentsUtil.IsEnableComponents(false, txtVersao, btnCadastroVersao, btnAlterarVersao, btnRemoverVersao);
                BallonDialog.Show($"{versaoSelect.VersaoSistema} cadastrado com sucesso !", "Cadastro");
                this.versaoSelect = null;
                this.Load();
            }
            catch (Exception ex)
            {
                BallonDialog.Show(ex.Message.ToString(), "Erro no cadastro");
            }
        }

        private void btnAlterarVersao_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtVersao.Text.Trim()))
            {
                BallonDialog.Show("Necessário preencher o campo número da versão!", "Alerta");
                return;
            }
            if (dgVersao.SelectedIndex == -1)
            {
                BallonDialog.Show("Necessário selecionar um item da tabela!", "Alerta");
                return;
            }

            try
            {
                versaoSelect.VersaoSistema = txtVersao.Text.Trim();
                versaoSelect = daoVersao.SaveOrUpdate(versaoSelect);
                txtVersao.Text = string.Empty;
                MyComponentsUtil.IsEnableComponents(false, txtVersao, btnCadastroVersao, txtVersao, btnAlterarVersao, btnRemoverVersao);
                MyComponentsUtil.IsEnableComponents(true, btnNovaVersao);
                BallonDialog.Show($"{versaoSelect.VersaoSistema} modificado com sucesso !", "Modificação");
                this.versaoSelect = null;
                this.Load();
            }
            catch (Exception ex)
            {
                BallonDialog.Show(ex.Message.ToString(), "Erro na modificação");
            }
        }

        private void btnRemoverVersao_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Content.Equals("Cancelar"))
            {
                MyComponentsUtil.IsEnableComponents(false, txtVersao, btnCadastroVersao, txtVersao, btnAlterarVersao, btnRemoverVersao);
                MyComponentsUtil.IsEnableComponents(true, btnNovaVersao);
                (sender as Button).Content = "Remover";
                txtVersao.Text = string.Empty;
                return;
            }
            if (dgVersao.SelectedIndex == -1)
            {
                BallonDialog.Show("Necessario selecionar um item da tabela !", "Alerta !");
                return;
            }
            try
            {
                if (MessageDialog.Show(Window.GetWindow(this), $"Deseja excluir {versaoSelect.VersaoSistema} ?", "Excluir", MessageBoxButton.YesNo, MessageBoxImage.Question))
                {
                    if (daoVersao.Delete(versaoSelect))
                    {
                        txtVersao.Text = string.Empty;
                        MyComponentsUtil.IsEnableComponents(false, txtVersao, btnCadastroVersao, btnAlterarVersao, btnCadastroVersao, txtVersao);
                        BallonDialog.Show($"{versaoSelect.VersaoSistema} removido com sucesso !", "Remoção");
                        aplicativos.Where(apli => apli.IDAplicativo == versaoSelect.Aplicativo.IDAplicativo).Single().Versoes.Remove(versaoSelect);
                        Load();
                    }
                    else
                    {
                        BallonDialog.Show($"Ocorreu um erro na remoção do seguinte aplicativo: {versaoSelect.VersaoSistema}", "Erro na modificação");
                    }
                }
            }
            catch (Exception ex)
            {
                BallonDialog.Show(ex.Message.ToString(), "Erro na remoção");
            }
        }

        #endregion


    }
}
