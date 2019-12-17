using Br.Com.Posi.NoteAnalyzer.DAO;
using Br.Com.Posi.NoteAnalyzer.DataGrid.Model;
using Br.Com.Posi.NoteAnalyzer.Model;
using Br.Com.Posi.NoteAnalyzer.Util;
using NoteAnalyzer.Br.Com.Posi.NoteAnalyzer.Enums;
using NoteAnalyzer.GUI;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace NoteAnalyzer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataTable dt = new DataTable();
        private List<Cliente> clientes;
        private List<Object[]> run = new List<object[]>();

        private BackgroundWorker background1;
        private const int QUANTIDADE_WORK = 8;

        private object locks = new object();
        //@"\\192.193.10.252\d\xml\";
        String pathRaiz = @"\\192.193.10.252\d\xml\";//@"D:\xml\";
        String PathLocal = @"D:\xml\";
        String server = "192.193.10.252";
        String user = "administrador";
        String pass = "Dko5cv4k!";

        DateTime timeNow;

        private ConcurrentStack<NotePerState> NotePerStates = new ConcurrentStack<NotePerState>();

        private DispatcherTimer time;

        public MainWindow()
        {
            InitializeComponent();

            this.pathTextBox.Text = PathLocal;
            pathRaiz = this.pathTextBox.Text;

            LoadNetwork();
            Loading.Visibility = Visibility.Collapsed;
            //noteDataGrid.ItemsSource = NotePerStates.ToList();
            LoadGrid();

            

            time = new DispatcherTimer();
            time.Interval = new TimeSpan(1000);
            time.Tick += Time_Tick;

            clientes = new ClienteDAOImpl().GetList();
            noteDataGrid.Sorting += NoteDataGrid_Sorting;
        }

        private void NoteDataGrid_Sorting(object sender, DataGridSortingEventArgs e)
        {
            ListSortDirection sort = e.Column.SortDirection == null ? ListSortDirection.Ascending : e.Column.SortDirection.Value;

            switch (e.Column.Header.ToString())
            {
                case "Número":
                    if (sort == ListSortDirection.Ascending)
                    {
                        NotePerStates = new ConcurrentStack<NotePerState>(NotePerStates.OrderBy(c => c.numeroLoja));
                    }
                    else
                    {
                        NotePerStates = new ConcurrentStack<NotePerState>(NotePerStates.OrderByDescending(c => c.numeroLoja));
                    }
                    break;

                case "Nome":
                    if (sort == ListSortDirection.Ascending)
                    {
                        NotePerStates = new ConcurrentStack<NotePerState>(NotePerStates.OrderBy(c => c.nomeLoja));
                    }
                    else
                    {
                        NotePerStates = new ConcurrentStack<NotePerState>(NotePerStates.OrderByDescending(c => c.nomeLoja));
                    }
                    break;

                case "Data":
                    if (sort == ListSortDirection.Ascending)
                    {
                        NotePerStates = new ConcurrentStack<NotePerState>(NotePerStates.OrderBy(c => c.dataCriado));
                    }
                    else
                    {
                        NotePerStates = new ConcurrentStack<NotePerState>(NotePerStates.OrderByDescending(c => c.dataCriado));
                    }
                    break;

                case "Nota Inicial":
                    if (sort == ListSortDirection.Ascending)
                    {
                        NotePerStates = new ConcurrentStack<NotePerState>(NotePerStates.OrderBy(c => c.notaInicial));
                    }
                    else
                    {
                        NotePerStates = new ConcurrentStack<NotePerState>(NotePerStates.OrderByDescending(c => c.notaInicial));
                    }
                    break;

                case "Nota Final":
                    if (sort == ListSortDirection.Ascending)
                    {
                        NotePerStates = new ConcurrentStack<NotePerState>(NotePerStates.OrderBy(c => c.notaFinal));
                    }
                    else
                    {
                        NotePerStates = new ConcurrentStack<NotePerState>(NotePerStates.OrderByDescending(c => c.notaFinal));
                    }
                    break;

                case "Quantidade Total":
                    if (sort == ListSortDirection.Ascending)
                    {
                        NotePerStates = new ConcurrentStack<NotePerState>(NotePerStates.OrderBy(c => c.quantidadeTotal));
                    }
                    else
                    {
                        NotePerStates = new ConcurrentStack<NotePerState>(NotePerStates.OrderByDescending(c => c.quantidadeTotal));
                    }
                    break;

                case "Faltantes":
                    if (sort == ListSortDirection.Ascending)
                    {
                        NotePerStates = new ConcurrentStack<NotePerState>(NotePerStates.OrderBy(c => c.faltantes));
                    }
                    else
                    {
                        NotePerStates = new ConcurrentStack<NotePerState>(NotePerStates.OrderByDescending(c => c.faltantes));
                    }
                    break;
            }
        }

        private void Time_Tick(object sender, EventArgs e)
        {
            DateTime t = DateTime.Now;
            cronometroTextBlock.Text = String.Format("{0:HH:mm:ss}", new DateTime(t.Subtract(timeNow).Ticks));
        }

        #region Threads
        private void Background1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Object[] arg = (Object[])e.Argument;
                NoteCalcule(arg[0].ToString(), arg[1].ToString(), arg[2].ToString(), arg[3].ToString(), arg[4].ToString(), (BackgroundWorker)arg[5]);
                e.Result = arg;
            }
            catch (Exception s)
            {
                MessageBox.Show("Ocorreu um erro na operação DO: " + s.Message);
            }
        }

        private void Background1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                object[] aux = (object[])e.UserState;

                Br.Com.Posi.NoteAnalyzer.Model.Note nota = (Br.Com.Posi.NoteAnalyzer.Model.Note)aux[0];
                nota.Rede = (string)aux[1];
                nota.Ano = (string)aux[3];
                nota.Mes = (string)aux[4];
                nota.Fiscal = (string)aux[5];
                //nota.Estado = estadoComboBox.SelectedItem.ToString();

                new Br.Com.Posi.NoteAnalyzer.DAO.NoteDAOImpl().Save(nota);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message.ToString());
            }
        }

        private void Background1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lock (locks)
            {
                try
                {
                    BackgroundWorker back = ((BackgroundWorker)((Object[])e.Result)[5]);
                    back.DoWork -= Background1_DoWork;
                    back.ProgressChanged -= Background1_ProgressChanged;
                    back.RunWorkerCompleted -= Background1_RunWorkerCompleted;
                    back.Dispose();

                    run.Remove((Object[])e.Result);

                    if (!run.Any())
                    {
                        Loading.Visibility = Visibility.Collapsed;
                        isComponentEnable(true);
                        time.Stop();
                    }
                    if (run.Count >= QUANTIDADE_WORK)
                    {
                        if (!((BackgroundWorker)run.ElementAt(QUANTIDADE_WORK - 1)[5]).IsBusy)
                        {
                            ((BackgroundWorker)run.ElementAt(QUANTIDADE_WORK - 1)[5]).RunWorkerAsync(run.ElementAt(QUANTIDADE_WORK - 1));
                        }
                    }
                    RefreshGrid();

                    threadsTextBlock.Text = "Lojas: " + run.Count;
                }
                catch (Exception exx)
                {
                    MessageBox.Show("Ocorreu um erro na operação COMPLETED: " + exx.Message);
                }
            }
        }
        #endregion

        #region LoadInContruct
        private void LoadGrid()
        {
            dt.Columns.Add(new DataColumn("Número", typeof(String)));
            dt.Columns.Add(new DataColumn("Nome", typeof(String)));
            dt.Columns.Add(new DataColumn("Data", typeof(String)));
            dt.Columns.Add(new DataColumn("Nota Inicial", typeof(String)));
            dt.Columns.Add(new DataColumn("Nota Final", typeof(String)));
            dt.Columns.Add(new DataColumn("Quantidade Total", typeof(int)));
            dt.Columns.Add(new DataColumn("Faltantes", typeof(int)));
            noteDataGrid.ItemsSource = dt.DefaultView;
            noteDataGrid.CanUserSortColumns = true;
        }

        private void LoadYear()
        {
            anoComboBox.Items.Clear();
            for (int i = 2015; i <= DateTime.Now.Year; i++)
            {
                anoComboBox.Items.Add(i.ToString());
            }
        }

        private void LoadMonth()
        {
            mesComboBox.Items.Clear();            
            for (int i = 1; i <= 12; i++)
            {
                mesComboBox.Items.Add(i < 10 ? "0" + i.ToString() : i.ToString());
            }
        }

        private void LoadNetwork()
        {
            try
            {
                Note.Connect(server, user, pass);
                redeComboBox.Items.Clear();
                foreach (String d in Note.ListNetwork(pathRaiz))
                {
                    redeComboBox.Items.Add(d.Substring(d.LastIndexOf("\\") + 1));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Ocorreu um erro para acessar os arquivos XML: \n\r" + e.Message);
            }

        }
        #endregion

        private void RefreshGrid()
        {
            dt.Clear();                        
            foreach (NotePerState n in NotePerStates.Distinct())
            {
                dt.Rows.Add(n.numeroLoja, n.nomeLoja, n.dataCriado,
                        n.notaInicial, n.notaFinal, n.quantidadeTotal,
                        n.faltantes);
            }
        }

        #region Event
        private void redeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (redeComboBox.SelectedIndex != -1)
            {
                estadoComboBox.Items.Clear();
                anoComboBox.Items.Clear();
                mesComboBox.Items.Clear();
                List<Cliente> clientes = new ClienteDAOImpl().ListState();
                foreach (Cliente c in clientes)
                {
                    estadoComboBox.Items.Add(c.Estado);
                }
                LoadYear();
                LoadMonth();
            }
        }

        private void estadoComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            run.Clear();
            /*if (estadoComboBox.SelectedIndex != -1)
            {
                LoadYear();
                LoadMonth();
            }*/
        }

        private void anoComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                NotePerStates.Clear();
                run.Clear();
                dt.Rows.Clear();
                if (mesComboBox.SelectedIndex != -1 && anoComboBox.SelectedIndex != -1)
                {
                    CarregarTabela();
                }
            }
            catch (Exception ex)
            {
                Debug.Print("Ocorreu um erro na atualização do ano: " + ex.Message);
            }
        }

        private void mesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                NotePerStates.Clear();
                run.Clear();
                dt.Rows.Clear();
                if (mesComboBox.SelectedIndex != -1 && anoComboBox.SelectedIndex != -1)
                {
                    CarregarTabela();
                }
            }
            catch (Exception t)
            {
                Debug.Print("Change Month: " + t.Message);
            }
        }

        #endregion

        private void CarregarTabela()
        {
            try
            {
                List<Cliente> clientes = new ClienteDAOImpl().ListPerState(string.Empty);//estadoComboBox.SelectedItem.ToString());
                if (clientes.Any())
                {
                    Loading.Visibility = Visibility.Visible;
                    timeNow = DateTime.Now;
                    time.Start();

                    var list = clientes.GroupBy(g => g.NumeroDoCliente).ToList().Select(s => s.First()).ToList();

                    foreach (Cliente c in list)
                    {
                        String rede = redeComboBox.SelectedItem.ToString();
                        String numero = "0" + c.NumeroDoCliente;
                        String ano = anoComboBox.SelectedItem.ToString();
                        String mes = mesComboBox.SelectedItem.ToString();

                        background1 = new BackgroundWorker();

                        background1.DoWork += Background1_DoWork;
                        background1.RunWorkerCompleted += Background1_RunWorkerCompleted;
                        background1.ProgressChanged += Background1_ProgressChanged;
                        background1.WorkerSupportsCancellation = true;
                        background1.WorkerReportsProgress = true;

                        String type = satRadioButton.IsChecked == true ? "SAT" : "NFCE";

                        Object[] arg = new Object[] { rede, numero, ano, mes, type, background1 };

                        run.Add(arg);

                        threadsTextBlock.Text = "Lojas: " + run.Count;
                        if (run.Count <= QUANTIDADE_WORK)
                        {
                            background1.RunWorkerAsync(arg);
                        }
                    }

                    isComponentEnable(false);
                }
            }
            catch (Exception c)
            {
                MessageBox.Show("Ocorreu um erro na operação LoadTable: " + c.Message);
            }
        }

        private void NoteCalcule(String rede, String store, String ano, String mes, String type, BackgroundWorker w)
        {
            List<NoteModel> models = new List<NoteModel>();

            bool result = false;
            Dispatcher.Invoke(() => {
                result = serverRadioButton.IsChecked.Value;
            });

            if (result)
            {
                if (!Directory.Exists(Path.Combine(pathRaiz, rede, store, ano, mes)))
                {
                    return;
                }
                models = Note.ListNotePerMonthPerStore(pathRaiz, rede, store, ano, mes, type, w);
            }
            else
            {
                if (!Directory.Exists(Path.Combine(pathRaiz, rede, store)))
                {
                    return;
                }
                models = Note.ListNotePerMonthPerStore(pathRaiz, rede, store, type, w);
            }

            try
            {
                if (models.Any())
                {
                    NotePerState note = new NotePerState();
                    List<int> falt = new List<int>();
                    List<int> inicio = new List<int>();
                    List<int> fim = new List<int>();

                    #region Controle por Serie ou por SAT
                    if (type.Equals("NFCE"))
                    {
                        inicio.Add(models.Where(s => s.Inutilizado == false).OrderBy(s => s.NumeroNota).First().NumeroNota);
                        fim.Add(models.Where(s => s.Inutilizado == false).OrderBy(s => s.NumeroNota).Last().NumeroNota);
                        if (inicio.Any() && fim.Any())
                        {
                            falt = Enumerable.Range(inicio.First(), (fim.First() - inicio.First())).Except(models.Where(s => s.Inutilizado == false).Select(s => s.NumeroNota)).ToList();
                        }
                    }
                    else
                    {
                        var list = models.GroupBy(c => c.Serie).ToList();
                        foreach (var group in list)
                        {
                            inicio.Add(group.OrderBy(s => s.NumeroNota).First().NumeroNota);
                            fim.Add(group.OrderBy(s => s.NumeroNota).Last().NumeroNota);
                            falt.AddRange(Enumerable.Range(inicio.Last(), (fim.Last() - inicio.Last())).Except(group.Select(c => c.NumeroNota)).ToList());
                        }
                    }
                    #endregion

                    note.dataCriado = String.Format("{0:dd/MM/yyyy}", models.OrderBy(c => c.DataCriado).Last().DataCriado);
                    note.numeroLoja = store;
                    note.notaInicial = "";
                    note.notaFinal = "";


                    #region Valida Número de serie com os mês anterior e posterior
                    if (type.Equals("SAT"))
                    {
                        for (int i = 0; i < inicio.Count; i++)
                        {
                            List<NoteModel> NoteFirst = new List<NoteModel>();
                            try
                            {
                                NoteFirst = Note.GetFirstNote(pathRaiz, rede, store, ano, mes, type);
                            }
                            catch (Exception e)
                            {
                                Debug.Print("Não existe notas no mês passado! " + e.Message);
                            }
                            List<IGrouping<String, NoteModel>> firstNote = new List<IGrouping<string, NoteModel>>();
                            if (NoteFirst.Any())
                            {
                                firstNote = NoteFirst.GroupBy(s => s.Serie).ToList();
                            }
                            else
                            {
                                note.notaInicial += inicio[i].ToString() + "*,";
                            }
                            String aux = "";
                            if (inicio.Count == firstNote.Count)
                            {
                                aux = (firstNote[i].OrderBy(c => c.NumeroNota).Last().NumeroNota + 1) == inicio[i] ? inicio[i].ToString() + "," : inicio[i].ToString() + "*,";
                            }
                            else
                            {
                                aux = inicio[i].ToString() + "*,";
                            }
                            note.notaInicial += !note.notaInicial.Contains(aux) ? aux : "";
                        }

                        for (int i = 0; i < fim.Count; i++)
                        {
                            List<NoteModel> NoteFirst = new List<NoteModel>();
                            try
                            {
                                NoteFirst = Note.GetLastNote(pathRaiz, rede, store, ano, mes, type);
                            }
                            catch (Exception e)
                            {
                                Debug.Print("Não existe notas no mês posterior!" + e.Message);
                            }
                            List<IGrouping<String, NoteModel>> lastNote = new List<IGrouping<string, NoteModel>>();
                            if (NoteFirst.Any())
                            {
                                lastNote = NoteFirst.GroupBy(s => s.Serie).ToList();
                            }
                            else
                            {
                                note.notaFinal += fim[i].ToString() + "*,";
                            }
                            String aux = "";
                            if (fim.Count == lastNote.Count)
                            {
                                aux = (lastNote[i].OrderBy(c => c.NumeroNota).First().NumeroNota - 1) == fim[i] ? fim[i].ToString() + "," : fim[i].ToString() + "*,";
                            }
                            else
                            {
                                aux = fim[i].ToString() + "*,";
                            }
                            note.notaFinal += !note.notaFinal.Contains(aux) ? aux : "";
                        }
                    }
                    else
                    {
                        List<NoteModel> NoteFirst = new List<NoteModel>();
                        try
                        {
                            NoteFirst = Note.GetFirstNote(pathRaiz, rede, store, ano, mes, type);
                        }
                        catch (Exception e)
                        {
                            Debug.Print("Não existe notas no mês anterior!" + e.Message);
                        }
                        int firstNote = NoteFirst.Any() ? NoteFirst.OrderBy(s => s.NumeroNota).Last().NumeroNota : 0;

                        List<NoteModel> NoteLast = new List<NoteModel>();
                        try
                        {
                            NoteLast = Note.GetLastNote(pathRaiz, rede, store, ano, mes, type);
                        }
                        catch (Exception e)
                        {
                            Debug.Print("Não existe notas no mês posterior!" + e.Message);
                        }
                        int LastNote = NoteLast.Any() ? NoteLast.OrderBy(s => s.NumeroNota).First().NumeroNota : 0;

                        note.notaInicial = (firstNote + 1) == inicio.First() ? note.notaInicial = inicio.First().ToString() : note.notaInicial = inicio.First().ToString() + "*";
                        note.notaFinal = (LastNote - 1) == fim.First() ? note.notaFinal = fim.First().ToString() : note.notaFinal = fim.First().ToString() + "*";
                    }
                    #endregion

                    note.nomeLoja = clientes.Where(c => c.NumeroDoCliente.Equals(note.numeroLoja.Substring(1))).First().NomeDoCliente;
                    note.numerosFaltantes = falt;
                    note.numerosInutilizado = models.Where(s => s.extrato == Extrato.Inutilizado).Select(s => s.NumeroNota).ToList();
                    note.numerosCancelado = models.Where(s => s.extrato == Extrato.Cancelado).Select(s => s.NumeroNota).ToList();
                    foreach (int i in note.numerosInutilizado)
                    {
                        note.numerosFaltantes.Remove(i);
                    }
                    foreach (int c in note.numerosCancelado)
                    {
                        note.numerosFaltantes.Remove(c);
                    }

                    note.faltantes = Convert.ToInt32(falt.Count().ToString());
                    note.quantidadeTotal = models.Count;
                    NotePerStates.Push(note);

                    Br.Com.Posi.NoteAnalyzer.Model.Note nota = new Br.Com.Posi.NoteAnalyzer.Model.Note();
                    nota.NumeroLoja = note.numeroLoja;
                    nota.NomeLoja = note.nomeLoja;
                    nota.NotaInicial = note.notaInicial;
                    nota.NotaFinal = note.notaFinal;
                    nota.UltimaTransferencia = note.dataCriado;
                    nota.QuantidadeNota = note.quantidadeTotal.ToString();
                    nota.Faltantes = note.faltantes.ToString();
                    nota.Rede = string.Empty;//redeComboBox.SelectedItem.ToString();
                    nota.Estado = string.Empty;//estadoComboBox.SelectedItem.ToString();
                    nota.Ano = string.Empty;//anoComboBox.SelectedItem.ToString();
                    nota.Mes = string.Empty;//mesComboBox.SelectedItem.ToString();
                    nota.Fiscal = string.Empty;//satRadioButton.IsChecked == true ? "SAT" : "NFCE";

                    w.ReportProgress(0, new object[] { nota, rede, store, ano, mes, type });

                    //new Br.Com.Posi.NoteAnalyzer.DAO.NoteDAOImpl().Save(nota);
                }
                models.Clear();
            }
            catch (Exception t)
            {
                MessageBox.Show("Ocorreu um erro na operação Note Calculo: " + t.Message);
            }
        }

        private void noteDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (noteDataGrid.SelectedIndex != -1)
                {
                    string numeroLoja = dt.Rows[noteDataGrid.SelectedIndex][0].ToString();
                    NotePerState note = NotePerStates.Where(s => s.numeroLoja.Equals(numeroLoja)).First();
                    NotesMissing notes = new NotesMissing(note.numerosFaltantes, note.numerosInutilizado, note.numerosCancelado);
                    notes.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    notes.ShowDialog();
                }
            }
            catch (Exception p)
            {
                MessageBox.Show("Ocorreu um erro na exibição NOTESMISSIONG: " + p.Message);
            }
        }

        private void CancelarButton_Click(object sender, RoutedEventArgs e)
        {
            List<BackgroundWorker> work = new List<BackgroundWorker>();
            foreach (Object[] o in run)
            {
                work.Add((BackgroundWorker)o[5]);
            }
            foreach (BackgroundWorker w in work)
            {
                w.CancelAsync();
                w.Dispose();
            }
            work.Clear();
            run.Clear();

            Loading.Visibility = Visibility.Collapsed;

            threadsTextBlock.Text = "Lojas: 0";

            isComponentEnable(true);
        }

        private void isComponentEnable(bool enable)
        {
            redeComboBox.IsEnabled = enable;
            estadoComboBox.IsEnabled = enable;
            mesComboBox.IsEnabled = enable;
            anoComboBox.IsEnabled = enable;
            satRadioButton.IsEnabled = enable;
            nfceRadioButton.IsEnabled = enable;
        }

        private void satRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (mesComboBox.SelectedIndex != -1 && anoComboBox.SelectedIndex != -1)
            {
                NotePerStates.Clear();
                run.Clear();
                dt.Rows.Clear();
                CarregarTabela();
            }
        }

        private void nfceRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (mesComboBox.SelectedIndex != -1 && anoComboBox.SelectedIndex != -1)
            {
                NotePerStates.Clear();
                run.Clear();
                dt.Rows.Clear();
                CarregarTabela();
            }
        }

        private void VerificarTextBox_Click(object sender, RoutedEventArgs e)
        {
            pathRaiz = this.pathTextBox.Text;
            LoadNetwork();
        }
    }
}
