using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using Br.Com.Posi.Shelf.Model;
using Br.Com.Posi.Shelf.DAO;
using Br.Com.Posi.Shelf.Enums;
using Br.Com.Posi.Enums;
using Br.Com.Posi.Util;
using System.ComponentModel;
using System.Linq;
using System.Data.SqlClient;
using Br.Com.Posi.Util.Extension;

namespace Br.Com.Posi.Shelf.Desktop.GUI.Funcionario
{
    /// <summary>
    /// Interaction logic for CadastroFuncionarioPage.xaml
    /// </summary>
    public partial class NavegacaoFuncionarioPage : Page
    {
        public Storyboard sbNav;
        public NavegacaoFuncionarioPage()
        {
            InitializeComponent();
            f_Setor.Content = null;
            f_Setor.Content = new SetorPage();
            f_Perfil.Content = null;
            f_Perfil.Content = new PerfilPage();
            f_Funcionario.Content = null;
            f_Funcionario.Content = new FuncionarioPage();
        }

        private void bt_Setor_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = (FindResource("dirEsqSair") as Storyboard);
            foreach (var a in sb.Children)
            {
                Storyboard.SetTargetName(a, "gd_navegacao");
            }
            sb.Begin();

            Storyboard sb2 = (FindResource("esqDir") as Storyboard);
            foreach (var a in sb2.Children)
            {
                Storyboard.SetTargetName(a, "f_Setor");
                

            }
            sb2.Begin();          

            Storyboard sb3 = (FindResource("esqDir") as Storyboard);
            foreach (var a in sb3.Children)
            {
                Storyboard.SetTargetName(a, "gd_Setor");

            }
            sb3.Begin();
            Storyboard sb4 = (FindResource("desaparece") as Storyboard);
            foreach (var a in sb4.Children)
            {
                Storyboard.SetTargetName(a, "gd_navegacao");

            }
            sb4.Begin();
            Storyboard sb5 = (FindResource("aparece") as Storyboard);
            foreach (var a in sb5.Children)
            {
                Storyboard.SetTargetName(a, "f_Setor");

            }
            sb5.Begin();
            Storyboard sb6 = (FindResource("aparece") as Storyboard);
            foreach (var a in sb6.Children)
            {
                Storyboard.SetTargetName(a, "gd_Setor");

            }
            sb6.Begin();


        }

        private void bt_Perfil_Click(object sender, RoutedEventArgs e)
        {

            Storyboard sb = (FindResource("dirEsqSair") as Storyboard);
            foreach (var a in sb.Children)
            {
                Storyboard.SetTargetName(a, "gd_navegacao");
            }
            sb.Begin();

            Storyboard sb2 = (FindResource("esqDir") as Storyboard);
            foreach (var a in sb2.Children)
            {
                Storyboard.SetTargetName(a, "f_Perfil");


            }
            sb2.Begin();

            Storyboard sb3 = (FindResource("esqDir") as Storyboard);
            foreach (var a in sb3.Children)
            {
                Storyboard.SetTargetName(a, "gd_Perfil");

            }
            sb3.Begin();
            Storyboard sb4 = (FindResource("desaparece") as Storyboard);
            foreach (var a in sb4.Children)
            {
                Storyboard.SetTargetName(a, "gd_navegacao");

            }
            sb4.Begin();
            Storyboard sb5 = (FindResource("aparece") as Storyboard);
            foreach (var a in sb5.Children)
            {
                Storyboard.SetTargetName(a, "f_Perfil");

            }
            sb5.Begin();
            Storyboard sb6 = (FindResource("aparece") as Storyboard);
            foreach (var a in sb6.Children)
            {
                Storyboard.SetTargetName(a, "gd_Perfil");

            }
            sb6.Begin();
        }

        private void bt_Funcionario_Click(object sender, RoutedEventArgs e)
        {

            Storyboard sb = (FindResource("dirEsqSair") as Storyboard);
            foreach (var a in sb.Children)
            {
                Storyboard.SetTargetName(a, "gd_navegacao");
            }
            sb.Begin();

            Storyboard sb2 = (FindResource("esqDir") as Storyboard);
            foreach (var a in sb2.Children)
            {
                Storyboard.SetTargetName(a, "f_Funcionario");


            }
            sb2.Begin();

            Storyboard sb3 = (FindResource("esqDir") as Storyboard);
            foreach (var a in sb3.Children)
            {
                Storyboard.SetTargetName(a, "gd_Funcionario");

            }
            sb3.Begin();
            Storyboard sb4 = (FindResource("desaparece") as Storyboard);
            foreach (var a in sb4.Children)
            {
                Storyboard.SetTargetName(a, "gd_navegacao");

            }
            sb4.Begin();
            Storyboard sb5 = (FindResource("aparece") as Storyboard);
            foreach (var a in sb5.Children)
            {
                Storyboard.SetTargetName(a, "f_Funcionario");

            }
            sb5.Begin();
            Storyboard sb6 = (FindResource("aparece") as Storyboard);
            foreach (var a in sb6.Children)
            {
                Storyboard.SetTargetName(a, "gd_Funcionario");

            }
            sb6.Begin();
        }

        private void btn_SetorVoltar_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = (FindResource("esqDirSair") as Storyboard);
            foreach (var a in sb.Children)
            {
                Storyboard.SetTargetName(a, "gd_Setor");


            }
            sb.Begin();
            Storyboard sb2 = (FindResource("dirEsq") as Storyboard);
            foreach (var a in sb2.Children)
            {
                Storyboard.SetTargetName(a, "gd_navegacao");


            }
            sb2.Begin();
            Storyboard sb3 = (FindResource("aparece") as Storyboard);
            foreach (var a in sb3.Children)
            {
                Storyboard.SetTargetName(a, "gd_navegacao");


            }
            sb3.Begin();
            Storyboard sb4 = (FindResource("desaparece") as Storyboard);
            foreach (var a in sb4.Children)
            {
                Storyboard.SetTargetName(a, "gd_Setor");


            }
            sb4.Begin();
            Storyboard sb5 = (FindResource("esqDirSair") as Storyboard);
            foreach (var a in sb5.Children)
            {
                Storyboard.SetTargetName(a, "f_Setor");


            }
            sb5.Begin();

        }

        private void btn_PerfilVoltar_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = (FindResource("esqDirSair") as Storyboard);
            foreach (var a in sb.Children)
            {
                Storyboard.SetTargetName(a, "gd_Perfil");


            }
            sb.Begin();
            Storyboard sb2 = (FindResource("dirEsq") as Storyboard);
            foreach (var a in sb2.Children)
            {
                Storyboard.SetTargetName(a, "gd_navegacao");


            }
            sb2.Begin();
            Storyboard sb3 = (FindResource("aparece") as Storyboard);
            foreach (var a in sb3.Children)
            {
                Storyboard.SetTargetName(a, "gd_navegacao");


            }
            sb3.Begin();
            Storyboard sb4 = (FindResource("desaparece") as Storyboard);
            foreach (var a in sb4.Children)
            {
                Storyboard.SetTargetName(a, "gd_Perfil");


            }
            sb4.Begin();
            Storyboard sb5 = (FindResource("esqDirSair") as Storyboard);
            foreach (var a in sb5.Children)
            {
                Storyboard.SetTargetName(a, "f_Perfil");


            }
            sb5.Begin();
        }

        private void btn_FuncionarioVoltar_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = (FindResource("esqDirSair") as Storyboard);
            foreach (var a in sb.Children)
            {
                Storyboard.SetTargetName(a, "gd_Funcionario");


            }
            sb.Begin();
            Storyboard sb2 = (FindResource("dirEsq") as Storyboard);
            foreach (var a in sb2.Children)
            {
                Storyboard.SetTargetName(a, "gd_navegacao");


            }
            sb2.Begin();
            Storyboard sb3 = (FindResource("aparece") as Storyboard);
            foreach (var a in sb3.Children)
            {
                Storyboard.SetTargetName(a, "gd_navegacao");


            }
            sb3.Begin();
            Storyboard sb4 = (FindResource("desaparece") as Storyboard);
            foreach (var a in sb4.Children)
            {
                Storyboard.SetTargetName(a, "gd_Funcionario");


            }
            sb4.Begin();
            Storyboard sb5 = (FindResource("esqDirSair") as Storyboard);
            foreach (var a in sb5.Children)
            {
                Storyboard.SetTargetName(a, "f_Funcionario");


            }
            sb5.Begin();
        }

        private void btn_SetorPerfil_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = (FindResource("esqDirSair") as Storyboard);
            foreach (var a in sb.Children)
            {
                Storyboard.SetTargetName(a, "gd_Setor");


            }
            sb.Begin();
            Storyboard sb2 = (FindResource("esqDirSair") as Storyboard);
            foreach (var a in sb2.Children)
            {
                Storyboard.SetTargetName(a, "f_Setor");


            }
            sb2.Begin();
            Storyboard sb3 = (FindResource("dirEsq") as Storyboard);
            foreach (var a in sb3.Children)
            {
                Storyboard.SetTargetName(a, "gd_Perfil");


            }
            sb3.Begin();
            Storyboard sb4 = (FindResource("dirEsq") as Storyboard);
            foreach (var a in sb4.Children)
            {
                Storyboard.SetTargetName(a, "f_Perfil");


            }
            sb4.Begin();
            Storyboard sb5 = (FindResource("aparece") as Storyboard);
            foreach (var a in sb5.Children)
            {
                Storyboard.SetTargetName(a, "f_Perfil");


            }
            sb5.Begin();
            Storyboard sb6 = (FindResource("aparece") as Storyboard);
            foreach (var a in sb6.Children)
            {
                Storyboard.SetTargetName(a, "gd_Perfil");


            }
            sb6.Begin();
            Storyboard sb7 = (FindResource("desaparece") as Storyboard);
            foreach (var a in sb7.Children)
            {
                Storyboard.SetTargetName(a, "f_Setor");


            }
            sb7.Begin();
            Storyboard sb8 = (FindResource("desaparece") as Storyboard);
            foreach (var a in sb8.Children)
            {
                Storyboard.SetTargetName(a, "gd_Setor");


            }
            sb8.Begin();

        }

        private void btn_PerfilFuncionario_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = (FindResource("esqDirSair") as Storyboard);
            foreach (var a in sb.Children)
            {
                Storyboard.SetTargetName(a, "gd_Perfil");


            }
            sb.Begin();
            Storyboard sb2 = (FindResource("esqDirSair") as Storyboard);
            foreach (var a in sb2.Children)
            {
                Storyboard.SetTargetName(a, "f_Perfil");


            }
            sb2.Begin();
            Storyboard sb3 = (FindResource("dirEsq") as Storyboard);
            foreach (var a in sb3.Children)
            {
                Storyboard.SetTargetName(a, "gd_Funcionario");


            }
            sb3.Begin();
            Storyboard sb4 = (FindResource("dirEsq") as Storyboard);
            foreach (var a in sb4.Children)
            {
                Storyboard.SetTargetName(a, "f_Funcionario");


            }
            sb4.Begin();
            Storyboard sb5 = (FindResource("aparece") as Storyboard);
            foreach (var a in sb5.Children)
            {
                Storyboard.SetTargetName(a, "f_Funcionario");


            }
            sb5.Begin();
            Storyboard sb6 = (FindResource("aparece") as Storyboard);
            foreach (var a in sb6.Children)
            {
                Storyboard.SetTargetName(a, "gd_Funcionario");


            }
            sb6.Begin();
            Storyboard sb7 = (FindResource("desaparece") as Storyboard);
            foreach (var a in sb7.Children)
            {
                Storyboard.SetTargetName(a, "f_Perfil");


            }
            sb7.Begin();
            Storyboard sb8 = (FindResource("desaparece") as Storyboard);
            foreach (var a in sb8.Children)
            {
                Storyboard.SetTargetName(a, "gd_Perfil");


            }
            sb8.Begin();
        }

        private void btn_FuncionarioSetor_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = (FindResource("esqDirSair") as Storyboard);
            foreach (var a in sb.Children)
            {
                Storyboard.SetTargetName(a, "gd_Funcionario");


            }
            sb.Begin();
            Storyboard sb2 = (FindResource("esqDirSair") as Storyboard);
            foreach (var a in sb2.Children)
            {
                Storyboard.SetTargetName(a, "f_Funcionario");


            }
            sb2.Begin();
            Storyboard sb3 = (FindResource("dirEsq") as Storyboard);
            foreach (var a in sb3.Children)
            {
                Storyboard.SetTargetName(a, "gd_Setor");


            }
            sb3.Begin();
            Storyboard sb4 = (FindResource("dirEsq") as Storyboard);
            foreach (var a in sb4.Children)
            {
                Storyboard.SetTargetName(a, "f_Setor");


            }
            sb4.Begin();
            Storyboard sb5 = (FindResource("aparece") as Storyboard);
            foreach (var a in sb5.Children)
            {
                Storyboard.SetTargetName(a, "f_Setor");


            }
            sb5.Begin();
            Storyboard sb6 = (FindResource("aparece") as Storyboard);
            foreach (var a in sb6.Children)
            {
                Storyboard.SetTargetName(a, "gd_Setor");


            }
            sb6.Begin();
            Storyboard sb7 = (FindResource("desaparece") as Storyboard);
            foreach (var a in sb7.Children)
            {
                Storyboard.SetTargetName(a, "f_Funcionario");


            }
            sb7.Begin();
            Storyboard sb8 = (FindResource("desaparece") as Storyboard);
            foreach (var a in sb8.Children)
            {
                Storyboard.SetTargetName(a, "gd_Funcionario");


            }
            sb8.Begin();
        }
    }
    }

