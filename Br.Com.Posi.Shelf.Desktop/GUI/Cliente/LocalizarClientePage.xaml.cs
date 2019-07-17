using Br.Com.Posi.Shelf.DAO;
using Br.Com.Posi.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Br.Com.Posi.Shelf.Desktop.GUI.Cliente
{
    /// <summary>
    /// Interaction logic for LocalizarClientePage.xaml
    /// </summary>
    public partial class LocalizarClientePage : Page
    {

        private static LocalizarClientePage _instance;
                
        public static LocalizarClientePage GetInstance()
        {
            if (_instance == null)
            {
                _instance = new LocalizarClientePage();
            }
            return _instance;
        }

        private CancellationTokenSource cancellation;

        private LocalizarClientePage()
        {
            this.InitializeComponent();
            this.IsVisibleChanged += LocalizarClientePage_IsVisibleChanged;
        }

        private void LocalizarClientePage_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(e.NewValue is bool)
            {
                if ((bool)e.NewValue)
                {
                    this.LoadClienteTableAsync();
                }
                else
                {
                    this.CallCancellationListRedes();
                    this.UnloadClienteTableAsync();
                }
            }            
        }
        
        private List<Model.Rede> ListRedes(object state)
        {
            CancellationToken token = (CancellationToken) state;

            try
            {
                IRedeDAO daoRede = DAOFactory.InitRedeDAO();
                token.ThrowIfCancellationRequested();
                var result = daoRede.GetList();
                token.ThrowIfCancellationRequested();
                return result;
            }
            catch
            {
                return null;
            }
        }

        private void CallCancellationListRedes()
        {
            if(this.cancellation != null && !this.cancellation.IsCancellationRequested)
            {
                this.cancellation.Cancel();
                this.cancellation = null;
            }
        }

        private async void LoadClienteTableAsync()
        {
            try
            {

                //lock
                this.CallCancellationListRedes();
                this.cancellation = new CancellationTokenSource();

                using (Task<List<Model.Rede>> listTask = Task.Factory.StartNew(ListRedes, this.cancellation.Token, this.cancellation.Token))
                {
                    List<Model.Rede> redes = await listTask;
                    
                    if(redes != null)
                    {
                        gd_Tabela.ItemsSource = redes.SelectMany(r => r.Clientes);
                        gd_Tabela.Items.Refresh();
                    }
                }
            }
            catch(OperationCanceledException ex)
            {

            }
            catch(Exception ex)
            {

            }
            finally
            {
                //unlock
            }                   
        }

        private void UnloadClienteTableAsync()
        {
            gd_Tabela.ItemsSource = null;
            //gd_Tabela.ClearValue(DataGrid.ItemsSourceProperty);
            gd_Tabela.Items.Refresh();
        }
        
        private void btn_Novo_Click(object sender, RoutedEventArgs e)
        {
            PrincipalWindow.GetInstance().Main.Navigate(new ClientePage(null));
        }

        private void gd_Tabela_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Model.Cliente cliente = (Model.Cliente)gd_Tabela.SelectedItem;
            if (cliente != null)
            {
                PrincipalWindow.GetInstance().Main.Navigate(new ClientePage(cliente));
            }
        }

        private void btnNovoComplemento_Click(object sender, RoutedEventArgs e)
        {
            PrincipalWindow.GetInstance().Main.Navegate(new CadastrosPage());
            
            
            
        }
    }
}
