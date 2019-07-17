using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.IO;
using System.IO.Packaging;
using System.Windows.Xps.Packaging;
using System.Text;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private StackPanel stackPanel;
        private StackPanel margin;


        public MainWindow()
        {
            this.InitializeComponent();

            bool result = Uri.TryCreate("http://comanda.alsaraiva.com.br/Mobile/CE", UriKind.Absolute, out Uri uri);

            MessageBox.Show(this, result.ToString());
        }

        private string teste(string idMasterConta)
        {

            string aux = idMasterConta;
            for (int i = 0; i < (12 - idMasterConta.Length); i++)
            {
                aux = "0" + aux;
            }
            return aux;

            /*string aux = string.Empty;
            aux += Encoding.Default.GetString(new byte[] { 0x1B });
            aux += Encoding.Default.GetString(new byte[] { Convert.ToByte('b') });
            aux += Encoding.Default.GetString(new byte[] { Convert.ToByte(1) });
            aux += Encoding.Default.GetString(new byte[] { Convert.ToByte(3) });
            aux += Encoding.Default.GetString(new byte[] { Convert.ToByte(100) });
            aux += Encoding.Default.GetString(new byte[] { Convert.ToByte(0) });
            aux += Encoding.ASCII.GetString(BitConverter.GetBytes(1597531));
            aux += Encoding.Default.GetString(new byte[] { 0x00 });
            return aux;*/
        }

        private void Print()
        {
            // Set up demo FixedDocument containing text to be searched
            var fixedDocument = new FixedDocument();
            var pageContent = new PageContent();
            var fixedPage = new FixedPage();

            this.PrintText();

            fixedPage.Children.Add(margin);
            pageContent.Child = fixedPage;
            fixedDocument.Pages.Add(pageContent);

            // Set up fresh XpsDocument
            var stream = new MemoryStream();
            var uri = new Uri("pack://document.xps");
            var package = Package.Open(stream, FileMode.Create, FileAccess.ReadWrite);
            PackageStore.AddPackage(uri, package);
            var xpsDoc = new XpsDocument(package, CompressionOption.NotCompressed, uri.AbsoluteUri);

            // Write FixedDocument to the XpsDocument
            var docWriter = XpsDocument.CreateXpsDocumentWriter(xpsDoc);
            docWriter.Write(fixedDocument);

            // Display XpsDocument in DocumentViewer
            documentviewer.Document = xpsDoc.GetFixedDocumentSequence();
        }

        private void PrintText()
        {
            this.SetMargin(40);

            this.Println("COMPROVANTE DE BORDERO", FontStyles.Normal, FontWeights.Bold, TextAlignment.Center);

            this.Println();
            this.Println();

            this.PrintlnFormat(@"Data: {0:dd/MM/yyyy}{1} N: {2:000}", DateTime.Now, new string(' ', 10), 1);

            this.Println();

            this.PrintlnFormat("Hora: {0:HH:mm:ss}", DateTime.Now);

            this.Println();
            this.Println();

            this.Println($"Operador {40}: Teste");

            this.Println();
            this.Println();

            this.Println(@"R$ 0,00", FontStyles.Normal, FontWeights.Normal, TextAlignment.Center);

            this.Println();
            this.Println();
            this.Println();

            this.PrintlnFormat(@"Operador: {0}", new string('_', 20));
            this.Println();
            this.Println();
            this.Println();
            this.PrintlnFormat(@"Gerente: {0}", new string('_', 22));
        }

        private void Println(string text, FontStyle fontStyle, FontWeight fontWeight, TextAlignment textAlignment)
        {
            this.stackPanel.Children.Add(new TextBlock() { Text = text, FontStyle = FontStyle, FontWeight = fontWeight, TextAlignment = textAlignment });
        }

        private void Println(string text = "")
        {
            this.Println(text, FontStyles.Normal, FontWeights.Normal, TextAlignment.Left);
        }

        private void PrintlnFormat(string text, params object[] args)
        {
            this.Println(string.Format(text, args));
        }

        private void SetMargin(double left, double top, double right, double bottom)
        {
            this.margin.Margin = new Thickness(left, top, right, bottom);
        }

        private void SetMargin(double margin)
        {
            this.margin.Margin = new Thickness(margin, margin, margin, margin);
        }
    }
}

