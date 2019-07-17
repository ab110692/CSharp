using Br.Com.Posi.Enums;
using Br.Com.Posi.Shelf.DAO;
using Br.Com.Posi.Shelf.Model;
using Br.Com.Posi.Util;
using Br.Com.Posi.Util.Extension;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Br.Com.Posi.Shelf.Desktop.GUI.Funcionario
{
    /// <summary>
    /// Interaction logic for FuncionarioPage.xaml
    /// </summary>
    public partial class FuncionarioPage : Page
    {

        //Thread
        BackgroundWorker loadSetorTask;
        //DAO
        ISetorDAO daoSetor;
        IPerfilDAO daoPerfil;
        IFuncionarioDadosPessoaisDAO daoFuncionarioDados;

        //Select
        Setor setorSelect;
        Perfil perfilSelect;
        FuncionarioDadosPessoais funcionarioDadosSelect;
        
        //List
        private List<Setor> setores;
        private List<Perfil> perfis;
        private List<Estado> estados;
        private List<FuncionarioDadosPessoais> funcionariosDados;

        public FuncionarioPage()
        {
            InitializeComponent();

            //Select
            setorSelect = new Setor();
            perfilSelect = new Perfil();
            funcionarioDadosSelect = new FuncionarioDadosPessoais();

            //List
            setores = new List<Setor>();
            perfis = new List<Perfil>();
            funcionariosDados = new List<FuncionarioDadosPessoais>();

            //DAO
            daoSetor = DAOFactory.InitSetorDAO();
            daoPerfil = DAOFactory.InitPerfilDAO();
            daoFuncionarioDados = DAOFactory.InitFuncionarioDadosPessoais();

            CarregaFuncionarios();

            estados = Estado.ACRE.GetAll();
            foreach (Estado e in estados)
            {
                cb_Estado.Items.Add(e.GetNomeEstado());
            }            
            loadSetorTask = new BackgroundWorker();
            loadSetorTask.RunWorkerAsync();           
        }
        
        private void CarregaFuncionarios()
        {
            funcionariosDados = daoSetor.GetList().SelectMany(s => s.Perfis).SelectMany(p => p.FuncionariosDadosPessoais).ToList();//daoFuncionarioDados.GetList();

            setores = daoSetor.GetList();
            perfis = daoPerfil.GetList();

            cb_Setor.ItemsSource = setores;

            dg_Funcionario.ItemsSource = funcionariosDados;
            dg_Funcionario.Items.Refresh();
        }
        private void ReCarregarCampos()
        {
            //Select            
            funcionarioDadosSelect = new FuncionarioDadosPessoais();


            //Funcionario
            funcionariosDados = daoFuncionarioDados.GetList();
            dg_Funcionario.ItemsSource = funcionariosDados;
            dg_Funcionario.Items.Refresh();
            

            //DataGrid
            dg_Funcionario.SelectedIndex = -1;
        }
        private void LimparFuncionario()
        {
            
            txt_NomeCompleto.Text = string.Empty;
            txt_Rg.Text = string.Empty;
            txt_CPF.Text = string.Empty;
            txt_Celular.Text = string.Empty;
            txt_Pis.Text = string.Empty;
            txt_Email.Text = string.Empty;
            txt_CEP.Text = string.Empty;
            txt_Endereco.Text = string.Empty;
            txt_Bairro.Text = string.Empty;
            txt_Numero.Text = string.Empty;
            txt_Cidade.Text = string.Empty;
            cb_Estado.SelectedIndex = -1;
        }
        private void btn_FuncionarioSalvar_Click(object sender, RoutedEventArgs e)
        {
            funcionarioDadosSelect = new FuncionarioDadosPessoais();
            try
            {
                funcionarioDadosSelect.NomeCompleto = txt_NomeCompleto.Text;
                funcionarioDadosSelect.RG = txt_Rg.Text;
                funcionarioDadosSelect.CPF = txt_CPF.Text;
                funcionarioDadosSelect.Telefone = txt_Celular.Text;
                funcionarioDadosSelect.Pis = txt_Pis.Text;
                funcionarioDadosSelect.Email = txt_Email.Text;
                funcionarioDadosSelect.CEP = txt_CEP.Text;
                funcionarioDadosSelect.Endereco = txt_Endereco.Text;
                funcionarioDadosSelect.Bairro = txt_Bairro.Text;
                funcionarioDadosSelect.Numero = txt_Numero.Text;
                funcionarioDadosSelect.Cidade = txt_Cidade.Text;
                funcionarioDadosSelect.Estado = Estado.ACRE.FromNomeEstado(cb_Estado.SelectedItem as string);
                funcionarioDadosSelect.Perfil = (Perfil)cb_Perfil.SelectedItem;
                if (string.IsNullOrEmpty(txt_NomeCompleto.Text))
                {
                    BallonDialog.Show("O campo nome completo deve ser preenchido!", "Preenchimento do Nome", txt_NomeCompleto.Focus());
                    return;
                }
                else if (string.IsNullOrEmpty(txt_Rg.Text))
                {
                    BallonDialog.Show("O campo rg deve ser preenchido!", "Preenchimento do RG", txt_Rg.Focus());
                    return;
                }
                else if (!txt_CPF.IsValid)
                {
                    BallonDialog.Show("O campo cpf esta inválido!", "Campo inválida", txt_CPF.Focus());
                    return;
                }
                else if (!txt_Celular.IsValid)
                {
                    BallonDialog.Show("O campo celular esta inválido!", "Campo inválida", txt_Celular.Focus());
                    return;
                }
                else if (!txt_Pis.IsValid)
                {
                    BallonDialog.Show("O campo pis esta inválido!", "Campo inválida", txt_Pis.Focus());
                    return;
                }
                else if (!txt_Email.IsValid)
                {
                    BallonDialog.Show("O campo email esta inválido!", "Campo inválida", txt_Email.Focus());
                    return;
                }
                else if (!txt_CEP.IsValid)
                {
                    BallonDialog.Show("O campo cep esta inválido!", "Campo inválida", txt_CEP.Focus());
                    return;
                }
                else if (string.IsNullOrEmpty(txt_Endereco.Text))
                {
                    BallonDialog.Show("O campo endereço esta inválido!", "Campo inválida", txt_Endereco.Focus());
                    return;
                }
                else if (string.IsNullOrEmpty(txt_Bairro.Text))
                {
                    BallonDialog.Show("O campo bairro esta inválido!", "Campo inválida", txt_Bairro.Focus());
                    return;
                }
                else if (string.IsNullOrEmpty(txt_Numero.Text))
                {
                    BallonDialog.Show("O campo número esta inválido!", "Campo inválida", txt_Numero.Focus());
                    return;
                }
                else if (string.IsNullOrEmpty(txt_Cidade.Text))
                {
                    BallonDialog.Show("O campo cidade esta inválido!", "Campo inválida", txt_Cidade.Focus());
                    return;
                }
                else if (cb_Estado.SelectedIndex == -1)
                {
                    BallonDialog.Show("Selecione um estado", "Seleção inválida");
                    return;
                }
                /*else if (daoFuncionario.VerificaUsuario(funcionarioSelect.Nome) != null)
                {
                    BallonDialog.Show("Nome de usuário ja existe", "Usuário já existe");
                    return;
                }*/
                else if (funcionarioDadosSelect != null)
                {

                    funcionarioDadosSelect = daoFuncionarioDados.SaveOrUpdate(funcionarioDadosSelect);
                    if (!perfis.Where(p => p.IDPerfil == funcionarioDadosSelect.Perfil.IDPerfil).Single().FuncionariosDadosPessoais.Any(f => f.IDFuncionario == funcionarioDadosSelect.IDFuncionario))
                    {

                        perfis.Where(p => p.IDPerfil == funcionarioDadosSelect.Perfil.IDPerfil).Single().FuncionariosDadosPessoais.Add(funcionarioDadosSelect);
                    }




                    BallonDialog.Show("Funcionário: " + funcionarioDadosSelect.NomeCompleto + " cadastrado com sucesso.", "Salvo com sucesso");
                    LimparFuncionario();
                    ReCarregarCampos();

                }
            }
            catch (SqlException sql)
            {
                if (sql.Number == 2627)
                {
                    BallonDialog.Show($"Funcionário: {perfilSelect.Nome} existente", "Operação Negada!");
                }
                else
                {
                    BallonDialog.Show("Erro",sql.ToString());
                }
            }
}
            
        private void btn_FuncionarioRemover_Click(object sender, RoutedEventArgs e)
        {
        }
        private void dg_Funcionario_Selected(object sender, RoutedEventArgs e)
        {
            if ((sender as DataGrid).SelectedIndex != -1)
            {
                try
                {   txt_NomeCompleto.Text = funcionarioDadosSelect.NomeCompleto.Trim();
                    txt_Rg.Text = funcionarioDadosSelect.RG.Trim();
                    txt_CPF.Text = funcionarioDadosSelect.CPF.Trim();
                    txt_Celular.Text = funcionarioDadosSelect.Telefone.Trim();
                    txt_Pis.Text = funcionarioDadosSelect.Pis.Trim();
                    txt_Email.Text = funcionarioDadosSelect.Email.Trim();
                    txt_CEP.Text = funcionarioDadosSelect.CEP.Trim();
                    txt_Endereco.Text = funcionarioDadosSelect.Endereco.Trim();
                    txt_Bairro.Text = funcionarioDadosSelect.Bairro.Trim();
                    txt_Numero.Text = funcionarioDadosSelect.Numero.Trim();
                    txt_Cidade.Text = funcionarioDadosSelect.Cidade.Trim();
                    cb_Estado.SelectedItem = funcionarioDadosSelect.Estado.GetNomeEstado();
                   // cb_Perfil.SelectedItem = funcionarioDadosSelect.Perfil.IDPerfil;
                }
                catch
                {

                }
            }
        }
        private void LoadSetorTask_DoWork(object sender, DoWorkEventArgs e)
        {
            setores = daoSetor.GetList();
        }
        private void LoadSetorTask_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {            

            //Funcionario
            dg_Funcionario.ItemsSource = setores.SelectMany(s => s.Perfis).SelectMany(p => p.FuncionariosDadosPessoais);
            dg_Funcionario.Items.Refresh();
           
        }
        private void dg_Funcionario_Selected(object sender, SelectionChangedEventArgs e)
        {

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            

            LoginPage loginPage = new LoginPage(funcionarioDadosSelect);
            
            f_Login.Content = loginPage;
            gd_F.Visibility = Visibility.Collapsed;
        }
        private void dg_Funcionario_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            FuncionarioDadosPessoais funcionariodados = (FuncionarioDadosPessoais)dg_Funcionario.SelectedItem;
            if (funcionariodados != null)
            {
                LoginPage loginPage = new LoginPage(funcionariodados);
                f_Login.Content = loginPage;
                gd_F.Visibility = Visibility.Collapsed;
            }
            
        }
        private void cb_Setor_Selected(object sender, RoutedEventArgs e)
        {

            Setor setor = cb_Setor.SelectedItem as Setor;
            //MessageBox.Show(setores.SelectMany(s => s.Perfils).SelectMany(p => p.Funcionarios).ToString());
               
            

            cb_Perfil.ItemsSource = setor.Perfis;

        }
    }
}
