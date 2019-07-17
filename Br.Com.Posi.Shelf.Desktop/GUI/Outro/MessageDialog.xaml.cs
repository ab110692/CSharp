using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Br.Com.Posi.Shelf.Desktop.GUI.Outro
{
    /// <summary>
    /// Interaction logic for MessageDialog.xaml
    /// </summary>
    public partial class MessageDialog : Window
    {
        private static MessageDialog _instance;

        private static Button btnConfirm;
        private static Button btnCancel;
        private static Path pathImagem;

        private static MessageDialog GetInstance()
        {
            if (_instance == null)
            {
                _instance = new MessageDialog();
            }
            return _instance;
        }

        private MessageDialog()
        {
            InitializeComponent();

            btnConfirm = new Button();
            btnConfirm.Click += BtnConfirm_Click;

            btnCancel = new Button();
            btnCancel.Click += BtnCancel_Click;

            pathImagem = new Path();
        }

        #region Evento
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            GetInstance().messageGrid.Children.Remove(btnConfirm);
            GetInstance().messageGrid.Children.Remove(btnCancel);

            _instance.DialogResult = false;
            _instance.Close();
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            GetInstance().messageGrid.Children.Remove(btnConfirm);
            GetInstance().messageGrid.Children.Remove(btnCancel);

            _instance.DialogResult = true;
            _instance.Close();
        }
        #endregion

        public static bool Show(string message, string titulo)
        {
            LoadDefault(null, message, titulo);
            LoadButton(MessageBoxButton.OK);
            return ShowDefault();
        }

        public static bool Show(Window owner, string message, string titulo)
        {
            LoadDefault(owner, message, titulo);
            LoadButton(MessageBoxButton.OK);
            return ShowDefault();
        }

        public static bool Show(Window owner, string message, string titulo, MessageBoxButton MessageBoxButton)
        {
            LoadDefault(owner, message, titulo);
            LoadButton(MessageBoxButton);
            return ShowDefault();
        }

        public static bool Show(Window owner, string message, string titulo, MessageBoxButton MessageBoxButton, MessageBoxImage MessageBoxImage)
        {
            LoadDefault(owner, message, titulo);
            LoadButton(MessageBoxButton);
            LoadIcon(MessageBoxImage);
            return ShowDefault();
        }

        #region LOAD
        private static void LoadDefault(Window owner, string message, string titulo)
        {
            if (_instance != null)
            {
                _instance = new MessageDialog();
            }
            GetInstance().Owner = owner;
            GetInstance().txtTitulo.Text = titulo;
            GetInstance().txtMensagem.Text = message;
        }

        private static void LoadButton(MessageBoxButton button)
        {
            switch (button)
            {
                case MessageBoxButton.OKCancel:
                    btnConfirm.Content = "OK";
                    btnConfirm.IsDefault = true;
                    btnCancel.Content = "Cancelar";
                    GetInstance().messageGrid.Children.Add(btnConfirm);
                    Grid.SetRow(btnConfirm, 5);
                    Grid.SetColumn(btnConfirm, 7);
                    GetInstance().messageGrid.Children.Add(btnCancel);
                    Grid.SetRow(btnCancel, 5);
                    Grid.SetColumn(btnCancel, 5);
                    break;
                case MessageBoxButton.YesNo:
                case MessageBoxButton.YesNoCancel:
                    btnConfirm.Content = "Sim";
                    btnConfirm.IsDefault = true;
                    btnCancel.Content = "Não";
                    GetInstance().messageGrid.Children.Add(btnConfirm);
                    Grid.SetRow(btnConfirm, 5);
                    Grid.SetColumn(btnConfirm, 7);
                    GetInstance().messageGrid.Children.Add(btnCancel);
                    Grid.SetRow(btnCancel, 5);
                    Grid.SetColumn(btnCancel, 5);
                    break;
                default:
                case MessageBoxButton.OK:
                    btnConfirm.Content = "OK";
                    btnConfirm.IsDefault = true;
                    GetInstance().messageGrid.Children.Add(btnConfirm);
                    Grid.SetRow(btnConfirm, 5);
                    Grid.SetColumn(btnConfirm, 7);
                    break;

            }
        }


        private static void LoadIcon(MessageBoxImage MessageBoxImage)
        {
            switch (MessageBoxImage)
            {


                case MessageBoxImage.None:
                    break;
                case MessageBoxImage.Error:
                    pathImagem.Data = Geometry.Parse("M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm1 15h-2v-2h2v2zm0-4h-2V7h2v6z");
                    pathImagem.Stretch = Stretch.Fill;
                    pathImagem.Fill = new SolidColorBrush(Colors.DarkGray);
                    pathImagem.Height = 72;
                    pathImagem.Width = 72;
                    GetInstance().messageGrid.Children.Add(pathImagem);
                    Grid.SetRow(pathImagem, 3);
                    Grid.SetColumn(pathImagem, 1);
                    break;

                case MessageBoxImage.Question:
                    pathImagem.Data = Geometry.Parse("M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm1 17h-2v-2h2v2zm2.07-7.75l-.9.92C13.45 12.9 13 13.5 13 15h-2v-.5c0-1.1.45-2.1 1.17-2.83l1.24-1.26c.37-.36.59-.86.59-1.41 0-1.1-.9-2-2-2s-2 .9-2 2H8c0-2.21 1.79-4 4-4s4 1.79 4 4c0 .88-.36 1.68-.93 2.25z");
                    pathImagem.Stretch = Stretch.Fill;
                    pathImagem.Fill = new SolidColorBrush(Colors.DarkGray);
                    pathImagem.Height = 72;
                    pathImagem.Width = 72;
                    GetInstance().messageGrid.Children.Add(pathImagem);
                    Grid.SetRow(pathImagem, 3);
                    Grid.SetColumn(pathImagem, 1);
                    break;


                default:
                case MessageBoxImage.Exclamation:
                    pathImagem.Data = Geometry.Parse("M1 21h22L12 2 1 21zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z");
                    pathImagem.Stretch = Stretch.Fill;
                    pathImagem.Fill = new SolidColorBrush(Colors.DarkGray);
                    pathImagem.Height = 72;
                    pathImagem.Width = 72;
                    GetInstance().messageGrid.Children.Add(pathImagem);
                    Grid.SetRow(pathImagem, 3);
                    Grid.SetColumn(pathImagem, 1);
                    break;
                case MessageBoxImage.Information:
                    pathImagem.Data = Geometry.Parse("M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm1 15h-2v-6h2v6zm0-8h-2V7h2v2z");
                    pathImagem.Stretch = Stretch.Fill;
                    pathImagem.Fill = new SolidColorBrush(Colors.DarkGray);
                    pathImagem.Height = 72;
                    pathImagem.Width = 72;
                    GetInstance().messageGrid.Children.Add(pathImagem);
                    Grid.SetRow(pathImagem, 3);
                    Grid.SetColumn(pathImagem, 1);
                    break;
            }
        }
        #endregion

        private static bool ShowDefault()
        {
            bool? result = GetInstance().ShowDialog();
            bool retorno = false;
            switch (result)
            {
                case null:
                    retorno = false;
                    break;
                case true:
                    retorno = true;
                    break;
                case false:
                    retorno = false;
                    break;
            }
            return retorno;
        }
    }
}
