using Br.Com.Posi.Connection.Model;
using Br.Com.Posi.Connection.Util;
using System;
using System.Windows;

namespace Br.Com.Posi.Shelf.Desktop.Outro
{
    /// <summary>
    /// Interaction logic for Configuracao.xaml
    /// </summary>
    public partial class Configuracao : Window
    {
        public Configuracao()
        {
            InitializeComponent();
            carregar();
        }

        private void b_aplicar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Connection.Model.Configuracao configuration = new Connection.Model.Configuracao();
                configuration.BancoDeDados = bancoDeDadosTextBox.Text;
                configuration.Servidor = servidorTextBox.Text;
                configuration.Porta = portaTextBox.Text;
                configuration.Usuario = usuarioTextBox.Text;
                configuration.Senha = senhaTextBox.Password;
                //MyRegister.Instance(Connection.Enums.BancoDeDados.Shelf).Writerconfiguraction(configuration);
                MyConfiguracaoXML.Instance(Connection.Enums.BancoDeDados.Shelf).Gravar(configuration);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void carregar()
        {
            //Connection.Model.Configuracao configuration = MyRegister.Instance(Connection.Enums.BancoDeDados.Shelf).Readerconfiguraction();
            Connection.Model.Configuracao configuracao = MyConfiguracaoXML.Instance(Connection.Enums.BancoDeDados.Shelf).Ler();
            if (configuracao != null)
            {
                bancoDeDadosTextBox.Text = configuracao.BancoDeDados;
                servidorTextBox.Text = configuracao.Servidor;
                portaTextBox.Text = configuracao.Porta;
                usuarioTextBox.Text = configuracao.Usuario;
                senhaTextBox.Password = configuracao.Senha;
            }
        }

        private void b_fechar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
