using Br.Com.Posi.Enums;
using Br.Com.Posi.Shelf.DAO;
using Br.Com.Posi.Shelf.Model;
using Br.Com.Posi.Util;
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
    /// Interaction logic for PerfilPage.xaml
    /// </summary>
    public partial class PerfilPage : Page
    {
        //Thread
        BackgroundWorker loadSetorTask;
        //DAO
        ISetorDAO daoSetor;
        IPerfilDAO daoPerfil;
       
        //Select
        Setor setorSelect;
        Perfil perfilSelect;
        Model.Funcionario funcionarioSelect;
        //List
        private List<PrivilegioCRUD> privilegios;
        private List<Setor> setores;

        public PerfilPage()
        {
            InitializeComponent();

            setorSelect = new Setor();
            perfilSelect = new Perfil();
            funcionarioSelect = new Model.Funcionario();

           

            daoSetor = DAOFactory.InitSetorDAO();
            daoPerfil = DAOFactory.InitPerfilDAO();

            loadSetorTask = new BackgroundWorker();
            loadSetorTask.DoWork += LoadSetorTask_DoWork;
            loadSetorTask.RunWorkerCompleted += LoadSetorTask_RunWorkerCompleted;

            CarregarPermissao();

            loadSetorTask.RunWorkerAsync();
        }




        private void ReCarregarCampos()
        {
            //Select
            setorSelect = new Setor();
            perfilSelect = new Perfil();
            funcionarioSelect = new Model.Funcionario();

            
            //Perfil
            dg_Perfil.ItemsSource = setores.SelectMany(s => s.Perfis);
            dg_Perfil.Items.Refresh();
            cb_PerfilSetor.ItemsSource = setores;
            cb_PerfilSetor.Items.Refresh();

           
            //DataGrid
            dg_Perfil.SelectedIndex = -1;
        }

        private void habilitaCadastro(string s)
        {

            
            
            (FindResource("FecharPerfil") as Storyboard).Begin();

            


        }

        private void bt_Perfil_Click(object sender, RoutedEventArgs e)
        {
            habilitaCadastro("perfil");
        }

        private void CarregarPermissao()
        {
            privilegios = PrivilegioCRUD.VISUALIZAR_ALTERAR_DELETAR.GetList();
            foreach (PrivilegioCRUD e in privilegios)
            {
                cb_PerfilManutencao.Items.Add(e.GetName());
                cb_PerfilFuncionario.Items.Add(e.GetName());
                cb_PerfilCliente.Items.Add(e.GetName());
                cb_PerfilAtendimento.Items.Add(e.GetName());
            }
        }

        private void LimparPerfil()
        {
            txt_PerfilNome.Text = string.Empty;
            cb_PerfilSetor.SelectedIndex = -1;
            cb_PerfilManutencao.SelectedIndex = -1;
            cb_PerfilFuncionario.SelectedIndex = -1;
            cb_PerfilCliente.SelectedIndex = -1;
            cb_PerfilAtendimento.SelectedIndex = -1;
        }

        private void btn_PerfilCancelar_Click(object sender, RoutedEventArgs e)
        {
            perfilSelect = dg_Perfil.SelectedItem as Perfil;
            if (perfilSelect.FuncionariosDadosPessoais.Any())
            {
                BallonDialog.Show($"Existem funcionario vincula ao perfil!", "Operação Inválida");
                return;
            }
            if (daoPerfil.Delete(perfilSelect))
            {
                setores.Where(s => s.IDSetor == perfilSelect.Setor.IDSetor).Single().Perfis.Remove(perfilSelect);
                ReCarregarCampos();
                BallonDialog.Show($"Perfil: {perfilSelect.Nome} excluido com sucesso !", "Operação realizado");
            }
        }

        private void btn_PerfilSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                perfilSelect.Nome = txt_PerfilNome.Text;
                perfilSelect.Setor = (Setor)cb_PerfilSetor.SelectedItem;
                perfilSelect.Atendimento = PrivilegioCRUD.SEM_ACESSO.GetFromName(cb_PerfilAtendimento.Text);
                perfilSelect.AtendimentoTexto = cb_PerfilAtendimento.Text;
               // perfilSelect.Funcionario = PrivilegioCRUD.SEM_ACESSO.GetFromName(cb_PerfilFuncionario.Text);
                perfilSelect.FuncionarioTexto = cb_PerfilFuncionario.Text;
                perfilSelect.Manutencao = PrivilegioCRUD.SEM_ACESSO.GetFromName(cb_PerfilManutencao.Text);
                perfilSelect.ManutencaoTexto = cb_PerfilManutencao.Text;
                perfilSelect.Cliente = PrivilegioCRUD.CRIAR_VISUALIZAR.GetFromName(cb_PerfilCliente.Text);
                perfilSelect.ClienteTexto = cb_PerfilCliente.Text;

                if (string.IsNullOrEmpty(txt_PerfilNome.Text.Trim()))
                {
                    BallonDialog.Show("O campo Nome devem ser preenchido!", "Campo nulo ou inválido");
                    return;
                }
                else if (cb_PerfilSetor.SelectedIndex == -1)
                {
                    BallonDialog.Show("Selecione um setor", "Seleção inválida");
                    return;
                }
                else if (cb_PerfilManutencao.SelectedIndex == -1)
                {
                    BallonDialog.Show("Selecione uma permissão de manutenção", "Seleção inválida");
                    return;
                }
                else if (cb_PerfilFuncionario.SelectedIndex == -1)
                {
                    BallonDialog.Show("Selecione uma permissão de funcionário", "Seleção inválida");
                    return;
                }
                else if (cb_PerfilCliente.SelectedIndex == -1)
                {
                    BallonDialog.Show("Selecione uma permissão de cliente", "Seleção inválida");
                    return;
                }
                else if (cb_PerfilAtendimento.SelectedIndex == -1)
                {
                    BallonDialog.Show("Selecione uma permissão de atendimento", "Seleção inválida");
                    return;
                }
                else if (perfilSelect != null)
                {
                    perfilSelect = daoPerfil.SaveOrUpdate(perfilSelect);
                    if (!setores.Where(s => s.IDSetor == perfilSelect.Setor.IDSetor).Single().Perfis.Any(p => p.IDPerfil == perfilSelect.IDPerfil))
                    {
                        setores.Where(s => s.IDSetor == perfilSelect.Setor.IDSetor).Single().Perfis.Add(perfilSelect);
                    }
                    LimparPerfil();
                    ReCarregarCampos();
                    BallonDialog.Show("Perfil: " + perfilSelect.Nome + " salvo com sucesso.", "Salvo com sucesso");
                }
            }
            catch (SqlException sql)
            {
                if (sql.Number == 2627)
                {
                    BallonDialog.Show($"Perfil: {perfilSelect.Nome} existente", "Operação Negada!");
                }
            }
        }

        private void dg_Perfil_Selected(object sender, RoutedEventArgs e)
        {
            if ((sender as DataGrid).SelectedIndex != -1)
            {
                perfilSelect = dg_Perfil.SelectedItem as Perfil;
                txt_PerfilNome.Text = perfilSelect.Nome;
                cb_PerfilAtendimento.SelectedItem = perfilSelect.Atendimento.GetName();
                cb_PerfilCliente.SelectedItem = perfilSelect.Cliente.GetName();
               // cb_PerfilFuncionario.SelectedItem = perfilSelect.Funcionario.GetName();
                cb_PerfilManutencao.SelectedItem = perfilSelect.Manutencao.GetName();
                cb_PerfilSetor.SelectedItem = perfilSelect.Setor;
            }
        }



        private void LoadSetorTask_DoWork(object sender, DoWorkEventArgs e)
        {
            setores = daoSetor.GetList();
        }

        private void LoadSetorTask_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Perfil
            dg_Perfil.ItemsSource = setores.SelectMany(s => s.Perfis);
            dg_Perfil.Items.Refresh();
            cb_PerfilSetor.ItemsSource = setores;
            cb_PerfilSetor.Items.Refresh();
            
        }
    }
}
