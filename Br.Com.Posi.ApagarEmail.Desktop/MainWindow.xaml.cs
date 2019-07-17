using OpenPop.Mime;
using OpenPop.Mime.Header;
using OpenPop.Pop3;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace Br.Com.Posi.ApagarEmail.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private const string SERVER = "pop.positecnologia.com.br";
        private int total, excluido, erro, totalParaExcluir = 50;
        private string MENSAGEM;

        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += MainWindow_Loaded;
            this.btnRemover.Click += BtnRemover_Click;

            this.MENSAGEM = $"Excluindo: {excluido} até: {total} erros: {erro}";
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.LoadMes();
            this.LoadAno();
        }

        private void BtnRemover_Click(object sender, RoutedEventArgs e)
        {
            this.ApagarEmail();
        }

        private void LoadMes()
        {
            for (int i = 1; i <= 12; i++)
            {
                cbMes.Items.Add(i);
            }
        }

        private void LoadAno()
        {
            for (int i = 2010; i <= DateTime.Now.Year; i++)
            {
                cbAno.Items.Add(i);
            }
        }

        private void ApagarEmail()
        {
            this.total = 0;
            this.excluido = 0;
            this.erro = 0;

            this.gridMain.Visibility = Visibility.Collapsed;
            this.gridLoading.Visibility = Visibility.Visible;

            Task t = Task.Run(() =>
            {
                try
                {
                    string user = string.Empty, pass = string.Empty;
                    Application.Current.Dispatcher.Invoke(() => { user = txtUsuario.Text; });
                    Application.Current.Dispatcher.Invoke(() => { pass = txtSenha.Password; });

                    Pop3Client pop3 = new Pop3Client();
                    
                    pop3.Connect("pop.positecnologia.com.br", 110, false);
                    pop3.Authenticate(user, pass);

                    List<MessageInfo> emails = pop3.GetMessageInfos();

                    this.total = emails.Count;

                    foreach (MessageInfo email in emails)
                    {
                        try
                        {
                            int ano = 0, mes = 0;

                            if (!pop3.Connected)
                            {
                                pop3.Connect("pop.positecnologia.com.br", 110, false);
                            }

                            Application.Current.Dispatcher.Invoke(() => { ano = int.Parse(cbAno.SelectedItem.ToString()); });
                            Application.Current.Dispatcher.Invoke(() => { mes = int.Parse(cbMes.SelectedItem.ToString()); });

                            MessageHeader header = pop3.GetMessageHeaders(email.Number);
                            
                            if (header.DateSent < new DateTime(ano, mes, 1))
                            {
                                pop3.DeleteMessage(email.Number);
                                this.excluido++;
                                if (this.excluido == this.totalParaExcluir)
                                {
                                    pop3.Disconnect();
                                    this.totalParaExcluir += this.totalParaExcluir;
                                }
                            }
                            else
                            {
                                this.excluido++;
                            }
                        }
                        catch (Exception ex2)
                        {
                            this.erro++;
                        }
                        finally
                        {
                            Application.Current.Dispatcher.Invoke(() => { this.userLoading.TrocarMensagem($"Excluindo: {excluido} até: {total} erros: {erro}"); });
                        }
                    }
                    pop3.Disconnect();
                }
                catch (Exception ex)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        MessageBox.Show(this, ex.Message.ToString());
                    });
                }
                Application.Current.Dispatcher.Invoke(() =>
                {
                    this.gridMain.Visibility = Visibility.Visible;
                    this.gridLoading.Visibility = Visibility.Collapsed;
                });
            });
        }
    }
}
