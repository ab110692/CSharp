using Br.Com.Posi.Enums;
using Br.Com.Posi.Shelf.DAO;
using Br.Com.Posi.Shelf.Model;
using Br.Com.Posi.Util;
using Br.Com.Posi.Util.Extension;
using Br.Com.Posi.Shelf.Desktop.GUI.Funcionario;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
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


namespace Br.Com.Posi.Shelf.Desktop.GUI.Funcionario
{
    /// <summary>
    /// Interaction logic for SetorPage.xaml
    /// </summary>
    public partial class SetorPage : Page
    {
        //Thread
        BackgroundWorker loadSetorTask;
        //DAO
        ISetorDAO daoSetor;
        IPerfilDAO daoPerfil;
        IFuncionarioDAO daoFuncionario;
        //Select
        Setor setorSelect;
        Perfil perfilSelect;
        Model.Funcionario funcionarioSelect;
        //List

        private List<Setor> setores;

        public SetorPage()
        {
            InitializeComponent();

            setorSelect = new Setor();
            perfilSelect = new Perfil();
            funcionarioSelect = new Model.Funcionario();

            daoSetor = DAOFactory.InitSetorDAO();
            daoPerfil = DAOFactory.InitPerfilDAO();
            daoFuncionario = DAOFactory.InitFuncionarioDAO();

            loadSetorTask = new BackgroundWorker();
            loadSetorTask.DoWork += LoadSetorTask_DoWork;
            loadSetorTask.RunWorkerCompleted += LoadSetorTask_RunWorkerCompleted;


            loadSetorTask.RunWorkerAsync();
        }


        private void habilitaCadastro(string s)
        {
            gd_Setor.Visibility = Visibility.Collapsed;


            btn_SetorSalvar.IsDefault = false;

            (FindResource("FecharSetor") as Storyboard).Begin();
            (FindResource("FecharPerfil") as Storyboard).Begin();
            (FindResource("FecharFuncionario") as Storyboard).Begin();
        }

        private void ReCarregarCampos()
        {
            //Select
            setorSelect = new Setor();
            perfilSelect = new Perfil();
            funcionarioSelect = new Model.Funcionario();

            //Setor
            dg_Setor.ItemsSource = setores;
            dg_Setor.Items.Refresh();

            //DataGrid
            dg_Setor.SelectedIndex = -1;
        }

        private void LimparSetor()
        {
            txt_SetorNome.Text = string.Empty;
        }

        private void btn_SetorSalvar_Click(object sender, RoutedEventArgs e)
        {


            if ((string)btn_SetorSalvar.Content == "Salvar")
            {


                try
                {
                    setorSelect = new Setor();
                    setorSelect.Nome = txt_SetorNome.Text;

                    if (string.IsNullOrEmpty(txt_SetorNome.Text.Trim()))
                    {
                        BallonDialog.Show("O campo setor devem ser preenchido!", "Campo nulo ou inválido");
                        return;
                    }
                    else if (setorSelect != null)
                    {

                        setorSelect = daoSetor.SaveOrUpdate(setorSelect);
                        if (!setores.Any(s => s.IDSetor == setorSelect.IDSetor))
                        {
                            setores.Add(setorSelect);
                        }
                        ReCarregarCampos();
                        LimparSetor();
                        BallonDialog.Show("Setor: " + setorSelect.Nome + " cadastrado com sucesso.", "Salvo com sucesso");
                        btn_Novo.IsEnabled = true;
                        (FindResource("sobeNovoSetor") as Storyboard).Begin();
                        (FindResource("aparecerBtn") as Storyboard).Begin();
                    }
                }
                catch (SqlException sql)
                {
                    if (sql.Number == 2627)
                    {
                        BallonDialog.Show($"Setor: {perfilSelect.Nome} existente", "Operação Negada!");
                    }
                }
            }
            else if ((string)btn_SetorSalvar.Content == "Alterar")
            {
                txt_SetorNome.IsEnabled = true;
                btn_SetorSalvar.Content = "Salvar";
                btn_SetorRemover.Content = "Cancelar";
            }


        }

        private void bt_Setor_Click(object sender, RoutedEventArgs e)
        {
            habilitaCadastro("setor");
        }

        private void btn_SetorRemover_Click(object sender, RoutedEventArgs e)
        {
            if ((string)btn_SetorRemover.Content == "Remover")
            {


                setorSelect = dg_Setor.SelectedItem as Setor;
                if (setorSelect.Perfis.Any())
                {
                    BallonDialog.Show($"Existem funcionario vincula ao perfil!", "Operação Inválida");
                    return;
                }
                if (daoSetor.Delete(setorSelect))
                {
                    setores.Remove(setorSelect);
                    ReCarregarCampos();
                    BallonDialog.Show($"Setor: {perfilSelect.Nome} excluido com sucesso !", "Operação realizado");
                    LimparSetor();
                    (FindResource("sobeNovoSetor") as Storyboard).Begin();
                    (FindResource("aparecerBtn") as Storyboard).Begin();

                }
            }
            else if ((string)btn_SetorRemover.Content == "Cancelar")
            {
                LimparSetor();
                txt_SetorNome.Text = null;
                btn_Novo.IsEnabled = true;
                (FindResource("sobeNovoSetor") as Storyboard).Begin();
                (FindResource("aparecerBtn") as Storyboard).Begin();

            }
        }

        private void dg_Setor_Selected(object sender, RoutedEventArgs e)
        {
            if ((sender as DataGrid).SelectedIndex != -1)
            {
                setorSelect = dg_Setor.SelectedItem as Setor;
                (FindResource("desceNovoSetor") as Storyboard).Begin();
                (FindResource("desaparecerBtn") as Storyboard).Begin();
                txt_SetorNome.Text = setorSelect.Nome;
                txt_SetorNome.IsEnabled = false;
                btn_SetorRemover.Content = "Remover";
                btn_SetorSalvar.Content = "Alterar";
            }
        }


        private void LoadSetorTask_DoWork(object sender, DoWorkEventArgs e)
        {
            setores = daoSetor.GetList();
        }

        private void LoadSetorTask_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Setor
            dg_Setor.ItemsSource = setores;
            dg_Setor.Items.Refresh();
        }

        private void btn_Novo_Click(object sender, RoutedEventArgs e)
        {
            setorSelect = null;
            LimparSetor();
            (FindResource("desceNovoSetor") as Storyboard).Begin();
            (FindResource("desaparecerBtn") as Storyboard).Begin();
            btn_Novo.IsEnabled = false;
            btn_SetorRemover.Content = "Cancelar";
        }

        
    }
}
