using Br.Com.Posi.SoftwareCamila.Desktop.model;
using Br.Com.Posi.Util;
using System;
using System.IO;
using System.Windows;

namespace Br.Com.Posi.SoftwareCamila.Desktop
{
    /// <summary>
    /// Interaction logic for ConfiguracaoDialog.xaml
    /// </summary>
    public partial class ConfiguracaoDialog : Window
    {
        public ConfiguracaoDialog()
        {
            InitializeComponent();
            this.Loaded += ConfiguracaoDialog_Loaded;
        }

        private void ConfiguracaoDialog_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                string path = MainWindow.DefaultContaPath;
                if (!File.Exists(path))
                {
                    return;
                }
                string xml = File.ReadAllText(path);
                Conta conta = MySerializer<Conta>.Deserialize(xml);
                if (conta == null)
                {
                    return;
                }
                servidorSMTPTextBox.Text = conta.ServidorSMTP;
                portaSMTPTextBox.Text = conta.PortaSMTP.ToString();
                usuarioTextBox.Text = conta.Usuario;
                senhaPasswordBox.Password = conta.Senha;
                dataBaseTextBox.Text = conta.DataBase;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void salvarButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(servidorSMTPTextBox.Text))
                {
                    throw new Exception("Campo servidor smtp não pode ser vazio!");
                }
                if (string.IsNullOrEmpty(portaSMTPTextBox.Text))
                {
                    throw new Exception("Campo porta smtp não pode ser vazio!");
                }
                if (string.IsNullOrEmpty(usuarioTextBox.Text))
                {
                    throw new Exception("Campo usuário não pode ser vazio!");
                }
                if (string.IsNullOrEmpty(senhaPasswordBox.Password))
                {
                    throw new Exception("Campo senha não pode ser vazio!");
                }
                if (string.IsNullOrEmpty(dataBaseTextBox.Text))
                {
                    throw new Exception("Campo base de dados não pode ser vazio!");
                }
                string path = MainWindow.DefaultContaPath;

                Conta conta = new Conta();
                conta.ServidorSMTP = servidorSMTPTextBox.Text;
                conta.PortaSMTP = Convert.ToInt32(portaSMTPTextBox.Text);
                conta.Usuario = usuarioTextBox.Text;
                conta.Senha = senhaPasswordBox.Password;
                conta.DataBase = dataBaseTextBox.Text;

                string xml = MySerializer<Conta>.Serialize(conta);
                File.WriteAllText(path, xml);

                MessageBox.Show(this, "Necessario fechar e abrir a aplicação", "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void fecharButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void localizarButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".xls";
            dlg.Filter = "Excel Files (*.xls)|*.xls";
            Nullable<bool> result = dlg.ShowDialog();
            if (result==true)
            {
                string fileName = dlg.FileName;
                dataBaseTextBox.Text = fileName;
                File.Copy(fileName, MainWindow.DefaultDataBasePath, true);
            }
        }
    }
}
