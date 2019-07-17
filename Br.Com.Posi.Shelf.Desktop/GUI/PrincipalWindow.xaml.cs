using System;
using System.Windows;
using Br.Com.Posi.Shelf.Desktop.GUI.Funcionario;
using Br.Com.Posi.Shelf.Desktop.GUI.Cliente;
using Br.Com.Posi.Shelf.Desktop.GUI.Outro;
using Br.Com.Posi.Shelf.Desktop.GUI.Atendimento;

namespace Br.Com.Posi.Shelf.Desktop
{
    /// <summary>
    /// Interaction logic for PrincipalWindow.xaml
    /// </summary>
    public partial class PrincipalWindow : Window
    {
        private static PrincipalWindow _instance;

        private static Model.Funcionario _funcionario { get; set; }

        public Model.Funcionario Funcionario { get { return _funcionario; } }

        public static PrincipalWindow GetInstance(Model.Funcionario funcionario)
        {
            if (_instance == null)
            {
                _instance = new PrincipalWindow();
            }
            if (_funcionario == null)
            {
                _funcionario = funcionario;
            }
            return _instance;
        }

        public static PrincipalWindow GetInstance()
        {
            if (_instance == null)
            {
                _instance = new PrincipalWindow();
            }
            return _instance;
        }

        private PrincipalWindow()
        {
            InitializeComponent();
            //this.MenuList.ItemsSource = new MenuList[]
            // {

            //     new MenuList { Title = "DashBoard",ImagePath=(IconeVetor.dashboard()) },                 
            //     new MenuList { Title = "Funcionario",ImagePath=(IconeVetor.funcionario()) },
            //     new MenuList { Title = "Cliente",ImagePath=(IconeVetor.cliente()) },
            //     new MenuList { Title = "Atendimento",ImagePath=(IconeVetor.atendimento())},
            //     new MenuList { Title = "Manutenção",ImagePath=(IconeVetor.manutencao())},
            //     new MenuList { Title = "Sair",ImagePath=(IconeVetor.sair())},                

            // };
        }

        private void btn_Dashboard_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Funcionario_Click(object sender, RoutedEventArgs e)
        {
            NavegacaoFuncionarioPage navegacaoFuncionario = new NavegacaoFuncionarioPage();
            Main.Content = navegacaoFuncionario;
        }

        private void btn_Cliente_Click(object sender, RoutedEventArgs e)
        {
            LocalizarClientePage localizarClientePage = LocalizarClientePage.GetInstance();
            Main.Content = localizarClientePage;
        }

        private void btn_Atendimento_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Main.Content = MainAtendimentoPage.GetInstance();
            }
            catch (Exception ex)
            {
                MessageDialog.Show(this, ex.Message.ToString(), "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_Manutencao_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Sair_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }

        //private void MenuList_Selection_Changed(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        //{
        //    if (MenuList.SelectedIndex == 1)
        //    {
        //        NavegacaoFuncionarioPage navegacaoFuncionario = new NavegacaoFuncionarioPage();
        //        Main.Content = navegacaoFuncionario;
        //    }
        //    else if (MenuList.SelectedIndex == 2)
        //    {
        //        NavegacaoClientePage navegacaoCliente = new NavegacaoClientePage();
        //        Main.Content = navegacaoCliente;
        //    }
        //    else if (MenuList.SelectedIndex == 3)
        //    {
        //        MyNavegate.MainFrameNavegate(CadastraAtendimentoPage.GetInstance());
        //    }
        //    else if (MenuList.SelectedIndex == 5)
        //    {
        //        Environment.Exit(0);
        //    }
        //}
    }
}
