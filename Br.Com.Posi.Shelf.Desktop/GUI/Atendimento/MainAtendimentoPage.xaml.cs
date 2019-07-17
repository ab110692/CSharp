using Br.Com.Posi.Event;
using Br.Com.Posi.Shelf.DAO;
using Br.Com.Posi.Shelf.Desktop.Design;
using Br.Com.Posi.Shelf.Enums;
using Br.Com.Posi.Shelf.Model;
using Br.Com.Posi.Util;
using Br.Com.Posi.Util.Network;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Br.Com.Posi.Shelf.Desktop.GUI.Atendimento
{
    /// <summary>
    /// Interaction logic for CadastraAtendimentoPage.xaml
    /// </summary>
    public partial class MainAtendimentoPage : Page
    {

        private const int TIMER_DELAY = 1000;
        private const int MODEL_DELAY = 15000;

        private static MainAtendimentoPage _instance;
        private IProtocoloDAO daoProtocolo;

        private List<Rede> redes;

        public ConcurrentDictionary<long, Tuple<MenuList, object, bool>> pages;

        private Broadcast broadcast;

        private Timer RefreshTimeTask;
        private Timer RefreshModelTask;

        public static MainAtendimentoPage GetInstance()
        {
            if (_instance == null)
            {
                _instance = new MainAtendimentoPage();
            }
            return _instance;
        }

        private MainAtendimentoPage()
        {
            InitializeComponent();

            this.daoProtocolo = DAOFactory.InitProtocoloDAO();
            this.pages = new ConcurrentDictionary<long, Tuple<MenuList, object, bool>>();
            this.redes = new List<Rede>();

            //this.broadcast = new Broadcast("ShelfCommunication");
            //this.broadcast.OnPostReceive += Broadcast_OnPostReceive;
            //this.broadcast.OnSubmit += Broadcast_OnSubmit;

            this.RefreshTimeTask = new Timer();
            this.RefreshTimeTask.Interval = TIMER_DELAY;
            this.RefreshTimeTask.Elapsed += RefreshTimeTask_Elapsed;
            this.RefreshTimeTask.Start();

            this.RefreshModelTask = new Timer();

            this.RefreshModelTask.Elapsed += RefreshModelTask_Elapsed;
            this.RefreshModelTask.Start();

            this.AddPageMain();
        }

        #region Task
        private void RefreshModelTask_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.RefreshModelTask.Interval = MODEL_DELAY;
            IRedeDAO dao = DAOFactory.InitRedeDAO();
            redes = dao.GetList();
        }

        private void RefreshTimeTask_Elapsed(object sender, ElapsedEventArgs e)
        {
            listBox_list.Dispatcher.BeginInvoke(new Action(() => listBox_list.Items.Refresh()));
        }
        #endregion

        #region EventBroadcast
        private Event.MessageArgs Broadcast_OnSubmit()
        {
            List<object> list = new List<object>();
            pages.ToList().ForEach(p => { if (p.Value.Item3 == false) list.Add(p.Value.Item2); });
            foreach (object o in list)
            {
                return (o as NovoAtendimento).cbNomeFantasia.Dispatcher.Invoke
                    (
                    new Func<Event.MessageArgs>(() =>
                    {
                        if ((o as NovoAtendimento).cbNomeFantasia.SelectedIndex >= 0)
                        {
                            long protocolo = Convert.ToInt64((o as NovoAtendimento).txtProtocolo.Text);

                            Tuple<MenuList, object, bool> tuple;
                            Tuple<MenuList, object, bool> newTuple;
                            pages.TryGetValue(protocolo, out tuple);
                            newTuple = new Tuple<MenuList, object, bool>(tuple.Item1, tuple.Item2, true);

                            pages.TryUpdate(protocolo, tuple, newTuple);

                            return new Event.MessageArgs()
                            {
                                Message = $"{(o as NovoAtendimento).lbNomeFuncionario.Content}\nComecou um atendimento com a loja\n{((o as NovoAtendimento).cbNomeFantasia.SelectedItem as Model.Cliente).NomeFantasia}"
                            };
                        }
                        else
                        {
                            return null;
                        }
                    })
                    );
            }
            return null;
        }

        private void Broadcast_OnPostReceive(Event.MessageArgs args)
        {
            BallonDialog.Show(args.Message, "Aviso");
        }
        #endregion

        #region Event
        private void bt_NovoAtendimento_Click(object sender, RoutedEventArgs e)
        {
            Protocolo protocolo = daoProtocolo.GerarProtocolo(TipoProtocolo.Atendimento);

            NovoAtendimento novo = new NovoAtendimento(protocolo, PrincipalWindow.GetInstance().Funcionario, redes);
            this.Main.Content = novo.Content;

            MenuList menuList = null;
            menuList = new MenuList();
            menuList.Title = "Atendimento";
            menuList.ImagePath = IconeVetor.NovoAtendimento();
            menuList.IsTimer = true;
            menuList.Position = 1;
            pages.GetOrAdd(protocolo.NumeroProtocolo, new Tuple<MenuList, object, bool>(menuList, novo, false));

            CarregarMenuList();
            listBox_list.SelectedIndex = pages.Count - 1;
        }

        private void bt_RemoverAtendimento_Click(object sender, RoutedEventArgs e)
        {
            if (listBox_list.SelectedIndex == -1)
            {
                BallonDialog.Show("Selecione um item da lista", "Alerta");
                return;
            }
            if (pages.ToList()[listBox_list.SelectedIndex].Value.Item2 is CategoriaAtendimentoPage)
            {
                BallonDialog.Show("Tela de categoria não pode ser removida", "Alerta");
                return;
            }
            if (pages.ToList()[listBox_list.SelectedIndex].Value.Item2 is ConsultaAtendimentoPage)
            {
                BallonDialog.Show("Tela de consulta não pode ser removidas", "Alerta");
                return;
            }
            if (pages.ToList()[listBox_list.SelectedIndex].Value.Item2 is AplicativoAtendimentoPage)
            {
                BallonDialog.Show("Tela de aplicativo não pode ser removido", "Alerta");
                return;
            }

            long numeroProtocolo = Convert.ToInt64((pages.ToList()[listBox_list.SelectedIndex].Value.Item2 as NovoAtendimento).txtProtocolo.Text);

            (pages.ToList()[listBox_list.SelectedIndex].Value.Item2 as NovoAtendimento).Dispose();
            (pages.ToList()[listBox_list.SelectedIndex].Value.Item2 as NovoAtendimento).Close();

            pages.TryRemove(numeroProtocolo, out Tuple<MenuList, object, bool> tuple);
            CarregarMenuList();
            listBox_list.SelectedIndex = (pages.Count - 1);
        }

        private void listBox_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox_list.SelectedIndex == -1)
            {
                return;
            }
            else if (pages.ToList()[listBox_list.SelectedIndex].Value.Item2 is Page)
            {
                Main.Content = (pages.ToList()[listBox_list.SelectedIndex].Value.Item2 as Page);
            }
            else if (pages.ToList()[listBox_list.SelectedIndex].Value.Item2 is Window)
            {
                Main.Content = (pages.ToList()[listBox_list.SelectedIndex].Value.Item2 as Window).Content;
            }
        }
        #endregion

        #region Method
        private void AddPageMain()
        {
            MenuList categoria = new MenuList();
            categoria.Title = "Categoria";
            categoria.ImagePath = IconeVetor.categoria();
            categoria.Position = default(int);

            MenuList aplicativo = new MenuList();
            aplicativo.Title = "Aplicativo";
            aplicativo.ImagePath = IconeVetor.aplicativo();
            aplicativo.Position = default(int);

            MenuList consulta = new MenuList();
            consulta.Title = "Consulta";
            consulta.ImagePath = IconeVetor.ConsutarAtendimento();
            aplicativo.Position = default(int);

            pages.GetOrAdd(pages.Count, new Tuple<MenuList, object, bool>(categoria, new CategoriaAtendimentoPage(), true));
            pages.GetOrAdd(pages.Count, new Tuple<MenuList, object, bool>(aplicativo, new AplicativoAtendimentoPage(), true));
            pages.GetOrAdd(pages.Count, new Tuple<MenuList, object, bool>(consulta, ConsultaAtendimentoPage.GetInstance(), true));

            CarregarMenuList();
        }

        public void CarregarMenuList()
        {
            List<MenuList> list = new List<MenuList>();
            pages.ToList().OrderBy(p => p.Value.Item1.Position).ToList().ForEach(p => list.Add(p.Value.Item1));
            listBox_list.ItemsSource = list;
            listBox_list.Items.Refresh();
        }

        /// <summary>
        /// Encerra o atendimento informando o protocolo
        /// </summary>
        /// <param name="protocolo"></param>
        public void FinalizarAtendimento(Protocolo protocolo)
        {
            for (int i = 3; i < pages.Count; i++)
            {
                if ((pages.ToList()[listBox_list.SelectedIndex].Value.Item2 as NovoAtendimento).txtProtocolo.Equals(protocolo.NumeroProtocolo.ToString()))
                {
                    listBox_list.SelectedIndex = i;
                }

                bt_RemoverAtendimento_Click(null, null);
            }
        }

        private BitmapImage LoadImage(string filename)
        {
            return new BitmapImage(new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @filename)));
        }

        #endregion
    }
}

