using Br.Com.Posi.NotaFiscal.Util;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Br.Com.Posi.NotaFiscal.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.btnLocalizarOrigem.Click += BtnLocalizarOrigem_Click;
            this.btnLocalizarDestino.Click += BtnLocalizarDestino_Click;

            this.btnAnalisar.Click += BtnAnalisar_Click;
            this.btnExaminar.Click += BtnExaminar_Click;
            this.btnCompactar.Click += BtnCompactar_Click;

            this.init();
        }

        private void init()
        {
            for (int i = 1; i <= 12; i++)
            {
                cbMes.Items.Add(i);
            }

            for (int i = 2010; i <= DateTime.Now.Year; i++)
            {
                cbAno.Items.Add(i);
            }
#if DEBUG
            this.txtOrigem.Text = "G:\\REX";
            this.txtDestino.Text = "G:\\Teste Compactacao";
#endif
        }


        private void Analisar()
        {
            try
            {
                if (string.IsNullOrEmpty(txtOrigem.Text.Trim()))
                {
                    throw new Exception("O caminho origem não pode ser vazio!");
                }
                if (string.IsNullOrEmpty(txtDestino.Text.Trim()))
                {
                    throw new Exception("O caminho destino não pode ser vazio!");
                }
                if (cbMes.SelectedIndex < 0)
                {
                    throw new Exception("O mês não pode ser vazio!");
                }
                if (cbAno.SelectedIndex < 0)
                {
                    throw new Exception("O ano não pode ser vazio!");
                }
                string origem = txtOrigem.Text;
                int mes = int.Parse(cbMes.SelectedItem.ToString());
                int ano = int.Parse(cbAno.SelectedItem.ToString());
                lblLoja.Text = Note.ListaLojaPorAnoMes(origem, mes, ano).ToString();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(this, ex.Message.ToString());
            }
        }

        private void Examinar()
        {
            try
            {
                if (string.IsNullOrEmpty(txtOrigem.Text.Trim()))
                {
                    throw new Exception("O caminho origem não pode ser vazio!");
                }
                if (string.IsNullOrEmpty(txtDestino.Text.Trim()))
                {
                    throw new Exception("O caminho destino não pode ser vazio!");
                }
                if (cbMes.SelectedIndex < 0)
                {
                    throw new Exception("O mês não pode ser vazio!");
                }
                if (cbAno.SelectedIndex < 0)
                {
                    throw new Exception("O ano não pode ser vazio!");
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(this, ex.Message.ToString());
            }
        }

        private void Compactar()
        {
            try
            {
                if (string.IsNullOrEmpty(txtOrigem.Text.Trim()))
                {
                    throw new Exception("O caminho origem não pode ser vazio!");
                }
                if (string.IsNullOrEmpty(txtDestino.Text.Trim()))
                {
                    throw new Exception("O caminho destino não pode ser vazio!");
                }
                if (cbMes.SelectedIndex < 0)
                {
                    throw new Exception("O mês não pode ser vazio!");
                }
                if (cbAno.SelectedIndex < 0)
                {
                    throw new Exception("O ano não pode ser vazio!");
                }
                string origem = txtOrigem.Text;
                string destino = txtDestino.Text;
                int mes = int.Parse(cbMes.SelectedItem.ToString());
                int ano = int.Parse(cbAno.SelectedItem.ToString());
                this.Lock();
                Task task = Task.Run(() =>
                {
                    string total = Note.ListaLojaPorAnoMes(origem, mes, ano).ToString();
                    this.Dispatcher.Invoke(() => this.userLoading.TrocarMensagem($"Compactado: 0 de: {total}"));
                    foreach (int qts in Note.CompactarPorAnoMes(origem, destino, mes, ano)) {
                        this.Dispatcher.Invoke(() => this.userLoading.TrocarMensagem($"Compactado: {qts + 1} de: {total}"));
                    }
                    this.Dispatcher.Invoke(() => this.UnLock());
                });


            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(this, ex.Message.ToString());
            }
        }

        private void Lock()
        {
            this.gridMain.Visibility = Visibility.Collapsed;
            this.gridLoading.Visibility = Visibility.Visible;
        }

        private void UnLock()
        {
            this.gridMain.Visibility = Visibility.Visible;
            this.gridLoading.Visibility = Visibility.Collapsed;
        }

        #region Events
        private void BtnLocalizarOrigem_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Abrir Caminho Raiz da Rede";

            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.txtOrigem.Text = fbd.SelectedPath;
            }
        }

        private void BtnLocalizarDestino_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Informar Destino da Estrutura";

            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.txtDestino.Text = fbd.SelectedPath;
            }
        }

        private void BtnAnalisar_Click(object sender, RoutedEventArgs e)
        {
            this.Analisar();
        }

        private void BtnExaminar_Click(object sender, RoutedEventArgs e)
        {
            this.Examinar();
        }

        private void BtnCompactar_Click(object sender, RoutedEventArgs e)
        {
            this.Compactar();
        }
        #endregion
    }
}
