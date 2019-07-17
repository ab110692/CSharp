using Br.Com.Posi.Shelf.DAO;
using Br.Com.Posi.Shelf.Desktop.GUI.Outro;
using Br.Com.Posi.Shelf.Desktop.Outro;
using Br.Com.Posi.Shelf.Model;
using System;
using System.Windows;

namespace Br.Com.Posi.Shelf.Desktop
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {

        IFuncionarioDAO daoFuncionario;
        IPerfilDAO daoPerfil;
        ISetorDAO daoSetor;

        public MainWindow()
        {
            InitializeComponent();
            try
            {
                daoFuncionario = DAOFactory.InitFuncionarioDAO();
                daoPerfil = DAOFactory.InitPerfilDAO();
                daoSetor = DAOFactory.InitSetorDAO();
            }
            catch (Exception ex)
            {
                MessageDialog.Show(null, ex.Message, "Erro", MessageBoxButton.OK);
            }
            this.usuarioTextField.Focus();
        }

        private void b_login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
#if (DEBUG)

                Funcionario func = DAOFactory.InitFuncionarioDAO().GetFirst();
                PrincipalWindow window = PrincipalWindow.GetInstance(func);
                this.Close();
                window.Show();
#else
                try
                {
                    if (string.IsNullOrEmpty(usuarioTextField.Text))
                    {
                        throw new Exception("Usuário deve ser preenchido!");
                    }
                    if (string.IsNullOrEmpty(senhaTextField.Password))
                    {
                        throw new Exception("Senha deve ser preenchido!");
                    }

                    if (daoSetor.VerificaSetor("Administrador") == null)
                    {
                        daoSetor.Save(new Model.Setor() { Nome = "Administrador" });
                    }

                    if (daoPerfil.VerificaPerfil("Administrador") == null)
                    {
                        daoPerfil.Save(new Model.Perfil() { Nome = "Administrador", Setor = daoSetor.VerificaSetor("Administrador"), Atendimento = Posi.Enums.PrivilegioCRUD.SEM_ACESSO, Manutencao = Posi.Enums.PrivilegioCRUD.SEM_ACESSO, Funcionario = Posi.Enums.PrivilegioCRUD.CRIAR_VISUALIZAR_ALTERAR_DELETAR, Cliente = Posi.Enums.PrivilegioCRUD.SEM_ACESSO });
                    }

                    if (daoFuncionario.VerificaUsuario("Administrador") == null)
                    {
                        //daoFuncionario.Save(new Model.Funcionario() { Nome = "Administrador", Senha = "admin", IDFuncionarioLogin = .VerificaPerfil("Administrador"), IDFuncionarioDadosPessoais = new Model.FuncionarioDadosPessoais() {NomeCompleto = "Administrador, Admins", RG = "00.000.000-0", CPF = "000.000.000-00", Telefone = "(11) 00000-0000", Pis = "000.00000.00-0", Email = "administrador@administrador.com", CEP = "00000-000", Endereco = "Rua Administrador", Bairro = "Admin", Numero = "0", Cidade = "Administradores", Estado = Posi.Enums.Estado.SAO_PAULO } });
                    }

                    Model.Funcionario funcionario = daoFuncionario.Find(usuarioTextField.Text, senhaTextField.Password);

                    if (funcionario == null)
                    {
                        throw new Exception("Usuário ou senha inválido!");
                    }
                    else
                    {
                        PrincipalWindow window = PrincipalWindow.GetInstance(funcionario);
                        this.Close();
                        window.Show();
                    }
                }
                catch (Exception ex)
                {
                    MessageDialog.Show(this, ex.Message, "Erro", MessageBoxButton.OK);
                }
#endif
        }
            catch (Exception ex)
            {
                MessageDialog.Show(this, ex.Message.ToString(), "Alerta!");
            }
}

        private void b_encerrar_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Canvas_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Configuracao configuracao = new Configuracao();
            configuracao.ShowDialog();
        }
    }
}
