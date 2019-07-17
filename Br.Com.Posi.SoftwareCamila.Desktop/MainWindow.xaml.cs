using Br.Com.Posi.SoftwareCamila.Desktop.model;
using Br.Com.Posi.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Windows;
using Excel = Microsoft.Office.Interop.Excel;

namespace Br.Com.Posi.SoftwareCamila.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static readonly string DefaultFilePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        public static readonly string DefaultImagePath = DefaultFilePath + @"\Nota Fiscal de Serviço.png";
        public static readonly string DefaultDataBasePath = DefaultFilePath + @"\DataBase.xls";
        public static readonly string DefaultContaPath = DefaultFilePath + @"\Conta.xml";

        private List<Empresa> empresaList;
        private List<Santander> santanderList;

        private Conta conta;

        private BackgroundWorker backgroundWorker;

        private int line = 0;

        public MainWindow()
        {
            InitializeComponent();
            Console.WriteLine(DefaultFilePath);
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {



            this.backgroundWorker = new BackgroundWorker();
            this.backgroundWorker.DoWork += BackgroundWorker_DoWork;
            this.backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
            this.backgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
            this.backgroundWorker.WorkerReportsProgress = true;

            this.Lock();

            this.backgroundWorker.RunWorkerAsync();
        }

        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.loadingTextBlock.Text = $"Carregando {e.ProgressPercentage}% completo";
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Unlock();
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!File.Exists(DefaultDataBasePath))
            {
                return;
            }
            this.santanderList = ReaderFileXLS(DefaultDataBasePath);
        }

        private void Lock()
        {
            this.mainGrid.Visibility = Visibility.Collapsed;
            this.loadGrid.Visibility = Visibility.Visible;
        }

        private void Unlock()
        {
            this.mainGrid.Visibility = Visibility.Visible;
            this.loadGrid.Visibility = Visibility.Collapsed;
        }

        private bool LoadContaXML()
        {
            try
            {
                if (File.Exists(DefaultContaPath))
                {
                    string xml = File.ReadAllText(DefaultContaPath);
                    this.conta = MySerializer<Conta>.Deserialize(xml);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        private List<Empresa> ReaderFileCSV(string path)
        {
            List<Empresa> empresaList = new List<Empresa>();
            string[] lines = System.IO.File.ReadAllLines(path).Skip(1).ToArray();
            foreach (string line in lines)
            {
                string[] columns = line.Split(';');
                Empresa empresa = new Empresa();
                empresa.TipoRegistro = columns[0];
                empresa.NumeroNFSe = columns[1];
                empresa.DataHoraNFe = columns[2];
                empresa.CodigoVerificacaoNFSe = columns[3];
                empresa.TipoRPS = columns[4];
                empresa.SerieRPS = columns[5];
                empresa.NumeroRPS = columns[6];
                empresa.DataFatoGerador = columns[7];
                empresa.InscricaoMunicipalPrestador = columns[8];
                empresa.IndicadorCPFCNPJPrestador = columns[9];
                empresa.CPFCNPJPrestador = columns[10];
                empresa.RazaoSocialPrestador = columns[11];
                empresa.TipoEnderecoPrestador = columns[12];
                empresa.EnderecoPrestador = columns[13];
                empresa.NumeroEnderecoPrestador = columns[14];
                empresa.ComplementoEnderecoPrestador = columns[15];
                empresa.BairroPrestador = columns[16];
                empresa.CidadePrestador = columns[17];
                empresa.UFPrestador = columns[18];
                empresa.CEPPrestador = columns[19];
                empresa.EmailPrestador = columns[20];
                empresa.OpcaoSimples = columns[21];
                empresa.SituacaoNotaFiscal = columns[22];
                empresa.DataCancelamento = columns[23];
                empresa.NumeroGuia = columns[24];
                empresa.DataQuitacaoGuiaVinculadaNotaFiscal = columns[25];
                empresa.ValorServico = columns[26];
                empresa.ValorDeducao = columns[27];
                empresa.CodigoServicoPrestadoNotaFiscal = columns[28];
                empresa.Aliquota = columns[29];
                empresa.ISSDevido = columns[30];
                empresa.ValorCredito = columns[31];
                empresa.ISSRetido = columns[32];
                empresa.IndicadorCPFCNPJTomador = columns[33];
                empresa.CPFCNPJTomador = columns[34];
                empresa.InscricaoMunicipalTomador = columns[35];
                empresa.InscricaoEstadualTomador = columns[36];
                empresa.RazaoSocialTomador = columns[37];
                empresa.TipoEnderecoTomador = columns[38];
                empresa.EnderecoTomador = columns[39];
                empresa.NumeroEnderecoTomador = columns[40];
                empresa.ComplementoEnderecoTomador = columns[41];
                empresa.BairroTomador = columns[42];
                empresa.CidadeDoTomador = columns[43];
                empresa.UFTomador = columns[44];
                empresa.CEPTomador = columns[45];
                empresa.EmailTomador = columns[46];
                empresa.DiscriminacaoServico = columns[47];
                empresaList.Add(empresa);
            }
            return empresaList;
        }

        private List<Santander> ReaderFileXLS(string path)
        {
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(path);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;
            double porcentagem = 100d / rowCount;
            double result = 0;

            List<Santander> santanderList = new List<Santander>();
            //iterate over the rows and columns and print to the console as it appears in the file
            //excel is not zero based!!
            for (int i = 2; i <= rowCount; i++)
            {
                Santander santander = new Santander();
                santander.NomeRazaoSocialPagador = xlRange.Cells[i, 1] != null ? xlRange.Cells[i, 1].Value2 != null ? xlRange.Cells[i, 1].Value2.ToString() : string.Empty : string.Empty;
                santander.TipoInscricao = xlRange.Cells[i, 2] != null ? xlRange.Cells[i, 2].Value2 != null ? xlRange.Cells[i, 2].Value2.ToString() : string.Empty : string.Empty;
                santander.NumeroInscricao = xlRange.Cells[i, 3] != null ? xlRange.Cells[i, 3].Value2 != null ? xlRange.Cells[i, 3].Value2.ToString() : string.Empty : string.Empty;
                santander.Endereco = xlRange.Cells[i, 4] != null ? xlRange.Cells[i, 4].Value2 != null ? xlRange.Cells[i, 4].Value2.ToString() : string.Empty : string.Empty;
                santander.Numero = xlRange.Cells[i, 5] != null ? xlRange.Cells[i, 5].Value2 != null ? xlRange.Cells[i, 5].Value2.ToString() : string.Empty : string.Empty;
                santander.Complemento = xlRange.Cells[i, 6] != null ? xlRange.Cells[i, 6].Value2 != null ? xlRange.Cells[i, 6].Value2.ToString() : string.Empty : string.Empty;
                santander.Bairro = xlRange.Cells[i, 7] != null ? xlRange.Cells[i, 7].Value2 != null ? xlRange.Cells[i, 7].Value2.ToString() : string.Empty : string.Empty;
                santander.Cidade = xlRange.Cells[i, 8] != null ? xlRange.Cells[i, 8].Value2 != null ? xlRange.Cells[i, 8].Value2.ToString() : string.Empty : string.Empty;
                santander.UnidadeFederativa = xlRange.Cells[i, 9] != null ? xlRange.Cells[i, 9].Value2 != null ? xlRange.Cells[i, 9].Value2.ToString() : string.Empty : string.Empty;
                santander.CEP = xlRange.Cells[i, 10] != null ? xlRange.Cells[i, 10].Value2 != null ? xlRange.Cells[i, 10].Value2.ToString() : string.Empty : string.Empty;
                santander.DDD = xlRange.Cells[i, 11] != null ? xlRange.Cells[i, 11].Value2 != null ? xlRange.Cells[i, 11].Value2.ToString() : string.Empty : string.Empty;
                santander.Telefone = xlRange.Cells[i, 12] != null ? xlRange.Cells[i, 12].Value2 != null ? xlRange.Cells[i, 12].Value2.ToString() : string.Empty : string.Empty;
                santander.Ramal = xlRange.Cells[i, 13] != null ? xlRange.Cells[i, 13].Value2 != null ? xlRange.Cells[i, 13].Value2.ToString() : string.Empty : string.Empty;
                santander.Contato = xlRange.Cells[i, 14] != null ? xlRange.Cells[i, 14].Value2 != null ? xlRange.Cells[i, 14].Value2.ToString() : string.Empty : string.Empty;
                santander.CodigoPagador = xlRange.Cells[i, 15] != null ? xlRange.Cells[i, 15].Value2 != null ? xlRange.Cells[i, 15].Value2.ToString() : string.Empty : string.Empty;
                santander.Email = xlRange.Cells[i, 16] != null ? xlRange.Cells[i, 16].Value2 != null ? xlRange.Cells[i, 16].Value2.ToString() : string.Empty : string.Empty;
                santander.Status = xlRange.Cells[i, 17] != null ? xlRange.Cells[i, 17].Value2 != null ? xlRange.Cells[i, 17].Value2.ToString() : string.Empty : string.Empty;
                santander.PagadorDDA = xlRange.Cells[i, 18] != null ? xlRange.Cells[i, 18].Value2 != null ? xlRange.Cells[i, 18].Value2.ToString() : string.Empty : string.Empty;
                santanderList.Add(santander);

                result += porcentagem;
                this.backgroundWorker.ReportProgress(Convert.ToInt32(result));

                Console.WriteLine(santander.NumeroInscricao + "-" + santander.Email);
            }

            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //release com objects to fully kill excel process from running in the background
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
            return santanderList;
        }

        private SmtpClient ConfigureEmail(string host, string user, string password, int port)
        {
            SmtpClient client = new SmtpClient();
            client.Port = port;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.UseDefaultCredentials = false;
            client.Host = host;
            client.Credentials = new NetworkCredential(user, password);
            return client;
        }

        private void SendEmail(string fromEmail, string toEmail, string copyEmail, SmtpClient smtpClient, string numeroSerie, string verificacao, string pathAttach)
        {
            using (MailMessage mail = new MailMessage(fromEmail, toEmail))
            {
                if (!string.IsNullOrEmpty(copyEmail))
                {
                    mail.To.Add(copyEmail);
                }
                mail.Subject = "Informação de Faturamento";
                mail.Body = "Prezado cliente, \n\n"
                    + "Esta é uma confirmação de Faturamento. Segue em anexo 2º via da NFSe\n\n"
                    + "Para emitir a nota fiscal, acesse http://nfpaulistana.prefeitura.sp.gov.br , clique na opção\n"
                    + "'Informações Gerais' e depois na opção 'Autenticidade' e informe os dados abaixo:\n"
                    + "\n"
                    + "CNPJ do prestador de serviços: 07.042.372/0001-20\n\n"
                    + "Número da NFS-e: " + numeroSerie + "\n\n"
                    + "Código de verificação: " + verificacao + "\n\n"
                    + "Você está recebendo este e-mail por estar cadastrado na POSI TECNOLOGIA EM AUTOMAÇÃO LTDA EPP para envio de mensagens\n"
                    + "Caso você deseje cancelar o recebimento, solicite a exclusão.\n\n"
                    + "Se houver dúvidas, favor nos contatar através dos e-mails cobranca@positecnologia.com.br / financeiro@positecnologia.com.br ou telefone 11 3624-1780.\n\n"
                    + "OBRIGADA PELA PARCERIA.\n"
                    + "\n"
                    + "Atenciosamente,"
                    + "Departamento Financeiro.";
                mail.Attachments.Add(new Attachment(pathAttach));
                smtpClient.Send(mail);
            }
        }

        private void GetImageUrl(string numeroFiscal, string verificacao)
        {
            using (WebClient client = new WebClient())
            {
                string url = @"https://nfe.prefeitura.sp.gov.br/contribuinte/notaprintimg.aspx?nf=" + numeroFiscal + "&inscricao=33584460&verificacao=" + verificacao;
                byte[] bytes = client.DownloadData(url);
                File.WriteAllBytes(DefaultImagePath, bytes);
            }
        }

        private void localizarArquivoButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".csv";
            dlg.Filter = "Comma-Separated Values (*.csv)|*.csv";
            Nullable<bool> result = dlg.ShowDialog();
            List<Empresa> empresaList;
            if (result == true)
            {
                string fileName = dlg.FileName;
                arquivoCSVTextBox.Text = fileName;
                empresaList = ReaderFileCSV(fileName);
                foreach (Empresa empresa in empresaList)
                {
                    if (string.IsNullOrEmpty(empresa.CodigoVerificacaoNFSe))
                    {
                        continue;
                    }
                    string cpfSantander = santanderList.First().NumeroInscricao;
                    string cpfCVS = empresa.CPFCNPJTomador.Replace(",", "").Replace(".", "").Replace("/", "").Replace("-", "").Trim();

                    long cnpjCVS = Convert.ToInt64(cpfCVS);

                    string email = santanderList.Where(s => Convert.ToInt64(s.NumeroInscricao.Trim()) == cnpjCVS).FirstOrDefault()?.Email.Trim();
                    empresa.EmailTomador = string.IsNullOrEmpty(email) ? null : email;

                    if (string.IsNullOrEmpty(empresa.EmailTomador))
                    {
                        empresa.EmailTomador = santanderList.Where(s => cpfCVS.Trim().Contains(s.NumeroInscricao.Trim())).FirstOrDefault()?.Email.Trim();
                    }

                    if (string.IsNullOrEmpty(empresa.EmailTomador))
                    {
                        empresa.EmailTomador = santanderList.Where(s => empresa.RazaoSocialTomador.Trim().Contains(s.NomeRazaoSocialPagador.Trim())).FirstOrDefault()?.Email.Trim();
                    }

                    if (string.IsNullOrEmpty(empresa.EmailTomador))
                    {
                        empresa.EmailTomador = empresa.EmailTomador;
                    }

                    empresa.Status = "-";
                    dataGridView.Items.Add(empresa);
                }
                /*if (empresaList != null)
                {
                    dataGridView.ItemsSource = empresaList;
                    dataGridView.Items.Refresh();
                }*/
            }
        }

        private void configuracaoButton_Click(object sender, RoutedEventArgs e)
        {
            ConfiguracaoDialog configuracaoDialog = new ConfiguracaoDialog();
            configuracaoDialog.ShowDialog();
        }

        private void enviarTeste_Click(object sender, RoutedEventArgs e)
        {
            Empresa empresa = null;
            try
            {
                if (LoadContaXML())
                {
                    empresa = this.dataGridView.SelectedItem as Empresa;
                    if (empresa == null)
                    {
                        MessageBox.Show(this, "Selecione uma linha da tabela!", "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    if (empresa.EmailTomador == null || string.IsNullOrEmpty(empresa.EmailTomador.Trim()))
                    {
                        empresa.Status = "Sem email!";
                        this.dataGridView.Items.Refresh();
                        return;
                    }
                    GetImageUrl(empresa.NumeroNFSe, empresa.CodigoVerificacaoNFSe);
                    SmtpClient smtpClient = this.ConfigureEmail(this.conta.ServidorSMTP, this.conta.Usuario, this.conta.Senha, this.conta.PortaSMTP);
                    this.SendEmail(this.conta.Usuario, emailTesteTextBox.Text, null, smtpClient, empresa.NumeroNFSe, empresa.CodigoVerificacaoNFSe, DefaultImagePath);
                    empresa.Status = "OK";
                }
                else
                {
                    MessageBox.Show(this, "Por favor, realize a configuração primeiro!", "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message.ToString(), "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
                if (empresa != null)
                {
                    empresa.Status = "Erro";
                }
            }
            this.dataGridView.Items.Refresh();
        }

        private void enviarEmailButton_Click(object sender, RoutedEventArgs e)
        {
            Empresa empresa = null;
            try
            {
                if (LoadContaXML())
                {
                    empresa = this.dataGridView.SelectedItem as Empresa;
                    if (empresa == null)
                    {
                        MessageBox.Show(this, "Selecione uma linha da tabela!", "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    if (empresa.EmailTomador == null || string.IsNullOrEmpty(empresa.EmailTomador.Trim()))
                    {
                        empresa.Status = "Sem email!";
                        this.dataGridView.Items.Refresh();
                        return;
                    }
                    GetImageUrl(empresa.NumeroNFSe, empresa.CodigoVerificacaoNFSe);
                    SmtpClient smtpClient = this.ConfigureEmail(this.conta.ServidorSMTP, this.conta.Usuario, this.conta.Senha, this.conta.PortaSMTP);
                    this.SendEmail(this.conta.Usuario, empresa.EmailTomador, emailTesteTextBox.Text, smtpClient, empresa.NumeroNFSe, empresa.CodigoVerificacaoNFSe, DefaultImagePath);
                    empresa.Status = "OK";
                }
                else
                {
                    MessageBox.Show(this, "Por favor, realize a configuração primeiro!", "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message.ToString(), "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
                if (empresa != null)
                {
                    empresa.Status = "Erro";
                }
            }
            this.dataGridView.Items.Refresh();
        }

        private void enviarDezButton_Click(object sender, RoutedEventArgs e)
        {
            Empresa empresa = null;
            try
            {
                if (LoadContaXML())
                {
                    int total = line + 10;
                    if (total > this.dataGridView.Items.Count)
                    {
                        total = this.dataGridView.Items.Count;
                    }
                    for (int i = line; i <= total; i++)
                    {
                        empresa = (Empresa)this.dataGridView.Items.GetItemAt(i);
                        if (empresa.EmailTomador == null || string.IsNullOrEmpty(empresa.EmailTomador.Trim()))
                        {
                            empresa.Status = "Sem email!";
                            this.dataGridView.Items.Refresh();
                            return;
                        }
                        GetImageUrl(empresa.NumeroNFSe, empresa.CodigoVerificacaoNFSe);
                        SmtpClient smtpClient = this.ConfigureEmail(this.conta.ServidorSMTP, this.conta.Usuario, this.conta.Senha, this.conta.PortaSMTP);
                        this.SendEmail(this.conta.Usuario, empresa.EmailTomador, emailTesteTextBox.Text, smtpClient, empresa.NumeroNFSe, empresa.CodigoVerificacaoNFSe, DefaultImagePath);
                        empresa.Status = "OK";
                    }
                    line += 10;
                }
                else
                {
                    MessageBox.Show(this, "Por favor, realize a configuração primeiro!", "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message.ToString(), "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
                if (empresa != null)
                {
                    empresa.Status = "Erro";
                }
            }
            this.dataGridView.Items.Refresh();
        }
    }
}
