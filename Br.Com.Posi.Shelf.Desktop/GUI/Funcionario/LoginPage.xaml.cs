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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Br.Com.Posi.Shelf.Desktop.GUI.Funcionario
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        //DAO
        private IFuncionarioDAO daoFuncionario;
        private IPerfilDAO daoPerfil;
        private ISetorDAO daoSetor;
        private IFuncionarioDadosPessoaisDAO daoFuncionarioDados;


        //Select
        private Perfil perfilSelect;
        private Setor setorSelect;
        private Model.Funcionario funcionarioSelect;
        private FuncionarioDadosPessoais funcionarioDadosSelect;


        //ISetorDAO daoSetor;
        private List<Setor> setores;
        private List<Perfil> perfis;
        private List<Model.Funcionario> funcionarios;
        private List<FuncionarioDadosPessoais> funcionariosDados;



        
        public LoginPage(FuncionarioDadosPessoais funcionarioDadosPessoais)
        {
            InitializeComponent();

            //Select
            perfilSelect = new Perfil();
            setorSelect = new Setor();
            funcionarioSelect = new Model.Funcionario();
            funcionarioDadosSelect = new FuncionarioDadosPessoais();
            //List
            setores = new List<Setor>();
            perfis = new List<Perfil>();
            funcionarios = new List<Model.Funcionario>();
            funcionariosDados = new List<FuncionarioDadosPessoais>();
            //DAO
            daoSetor = DAOFactory.InitSetorDAO();
            daoPerfil = DAOFactory.InitPerfilDAO();
            daoFuncionario = DAOFactory.InitFuncionarioDAO();
            daoFuncionarioDados = DAOFactory.InitFuncionarioDadosPessoais();
            CarregarLogin(funcionarioDadosPessoais);





        }

        public void CarregarLogin(FuncionarioDadosPessoais funcionarioDadosPessoais)
        {
            funcionarios = daoSetor.GetList().SelectMany(s => s.Perfis).SelectMany(d => d.FuncionariosDadosPessoais).SelectMany(f => f.Funcionarios).ToList();//daoFuncionario.GetList();
            perfis = daoPerfil.GetList();
            
            dg_Funcionario.ItemsSource = funcionarios;
            cb_FuncionarioPerfil.ItemsSource = perfis ;

            dg_Funcionario.Items.Refresh();
            cb_FuncionarioPerfil.Items.Refresh();
        }

        
        private void ReCarregarCampos()
        {
            //Select
            funcionarioSelect = new Model.Funcionario();


            //Funcionario
            dg_Funcionario.ItemsSource = setores.SelectMany(s => s.Perfis).SelectMany(p => p.FuncionariosDadosPessoais);
            dg_Funcionario.Items.Refresh();
            cb_FuncionarioPerfil.ItemsSource = setores.SelectMany(s => s.Perfis);
            cb_FuncionarioPerfil.Items.Refresh();

            //DataGrid
            dg_Funcionario.SelectedIndex = -1;
        }
        private void LimparFuncionario()
        {
            txt_Usuario.Text = string.Empty;
            txt_Senha.Password = string.Empty;
            cb_FuncionarioPerfil.SelectedIndex = -1;
            
        }
        private void dg_Funcionario_Selected(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dg_Funcionario_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void btn_FuncionarioSalvar_Click(object sender, RoutedEventArgs e)
        {
            if (funcionarioSelect != null)
            {
                if (string.IsNullOrEmpty(txt_Usuario.Text.Trim()))
                {
                    BallonDialog.Show("O campo Usuário deve ser preenchido !", "Campo Nulo ou Inválido");
                    return;

                }
                if (string.IsNullOrEmpty(txt_Senha.Password))
                {
                    BallonDialog.Show("O campo Senha deve ser preenchido !", "Campo Nulo ou Inválido");
                    return;

                }
                if (cb_FuncionarioPerfil.SelectedIndex == -1)
                {
                    BallonDialog.Show("O campo Perfil deve ser Selecionado !", "Campo Nulo ou Inválido");
                    return;
                }
                SalvarLogin();
            }
            
        }

        private void SalvarLogin()
        {
            funcionarioSelect.FuncionarioDadosPessoais = funcionarioDadosSelect;
            
            funcionarioSelect.Nome = txt_Usuario.Text;
            funcionarioSelect.Senha = txt_Senha.Password.ToString();

            IFuncionarioDAO funcionarioDAO = DAOFactory.InitFuncionarioDAO();
            funcionarioDAO.Save(funcionarioSelect);
            BallonDialog.Show("Login: "+ funcionarioSelect.Nome.ToString() +" criado com sucesso!", "Criação de Login");
        }
    }
}
