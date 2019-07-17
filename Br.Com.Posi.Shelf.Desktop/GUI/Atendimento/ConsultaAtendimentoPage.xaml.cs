using Br.Com.Posi.Enums;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Linq;
using Br.Com.Posi.Shelf.DAO;
using Br.Com.Posi.Shelf.Model;
using Br.Com.Posi.Shelf.DTO;
using System.Text;
using System.ComponentModel;
using System.Timers;
using Br.Com.Posi.Shelf.Desktop.Design;
using Br.Com.Posi.Util;
using Br.Com.Posi.Util.Network;
using System.Net;
using System.Windows.Input;

namespace Br.Com.Posi.Shelf.Desktop.GUI.Atendimento
{
    /// <summary>
    /// Interaction logic for ConsultaAtendimentoPage.xaml
    /// </summary>
    public partial class ConsultaAtendimentoPage : Page
    {

        private const int ANO_INICIAL = 2018;

        private IProtocoloDAO daoProtocolo;

        private List<Mes> Meses;
        private List<Protocolo> Protocolos;
        private List<DTOTabelaAtendimento> DTOTabelaAtendimentos;

        private DateTime DataAtual;

        private Timer atendimentoLoadTask;

        private static ConsultaAtendimentoPage _instance;

        public static ConsultaAtendimentoPage GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ConsultaAtendimentoPage();
            }
            return _instance;
        }

        private ConsultaAtendimentoPage()
        {
            InitializeComponent();

            daoProtocolo = DAOFactory.InitProtocoloDAO();

            Meses = Enum.GetValues(typeof(Mes)).Cast<Mes>().ToList();
            DTOTabelaAtendimentos = new List<DTOTabelaAtendimento>();

            DataAtual = daoProtocolo.GetDateTime();

            this.LoadMonth();
            this.LoadYear();
            this.LoadFilter();

            atendimentoLoadTask = new Timer
            {
                Interval = 3000
            };
            atendimentoLoadTask.Elapsed += AtendimentoLoadTask_Elapsed;
            atendimentoLoadTask.Start();

            this.LoadTable();
        }

        private void AtendimentoLoadTask_Elapsed(object sender, ElapsedEventArgs e)
        {
            Protocolos = DAOFactory.InitProtocoloDAO().GetList();
            DTOTabelaAtendimentos.Clear();

            foreach (Protocolo p in Protocolos)
            {
                if (p.Atendimento == null || !p.Atendimento.AtendimentoDetalhado.Any() || !p.Atendimento.Problemas.Any())
                {
                    continue;
                }
                DTOTabelaAtendimento tabela = new DTOTabelaAtendimento();
                tabela.Protocolo = p.NumeroProtocolo;
                tabela.Data = p.Atendimento.AtendimentoDetalhado.OrderByDescending(a => a.IDAtendimentoDetalhado).First().DataFinal;
                tabela.Atendente = p.Atendimento.AtendimentoDetalhado.OrderByDescending(a => a.IDAtendimentoDetalhado).First().Funcionario.Nome;
                tabela.NumeroCliente = p.Atendimento.Cliente.Codigo.ToString();
                tabela.Cliente = p.Atendimento.Cliente.NomeFantasia;
                tabela.Contato = p.Atendimento.AtendimentoDetalhado.OrderByDescending(a => a.IDAtendimentoDetalhado).First().Contato;
                StringBuilder sb = new StringBuilder();
                sb.Append(p.Atendimento.Problemas.OrderByDescending(pro => pro.IDProblema).First().Categoria);
                sb.Append(" - ");
                sb.Append(p.Atendimento.Problemas.OrderByDescending(pro => pro.IDProblema).First().SubCategoria);
                sb.Append(" - ");
                sb.Append(p.Atendimento.Problemas.OrderByDescending(pro => pro.IDProblema).First().Item);
                tabela.Problema = sb.ToString();
                DTOTabelaAtendimentos.Add(tabela);
            }
            this.Dispatcher.Invoke((Action)delegate ()
            {
                this.SearchTable();
                //gdTabela.Items.Refresh();
            });
        }

        private void LoadFilter()
        {
            cbFiltro.Items.Add("Atendente");
            cbFiltro.Items.Add("Cliente");
            cbFiltro.SelectedIndex = 0;
        }

        private void LoadMonth()
        {
            cbMes.ItemsSource = Meses;
            cbMes.Items.Refresh();

            Mes m = Mes.Dezembro.FromCodeForMonth(DataAtual.Month);

            cbMes.SelectedItem = m;
        }

        private void LoadYear()
        {
            for (int i = ANO_INICIAL; i <= DataAtual.Year; i++)
            {
                cbAno.Items.Add(i);
            }

            cbAno.SelectedItem = DataAtual.Year;
        }

        private void LoadTable()
        {
            gdTabela.ItemsSource = DTOTabelaAtendimentos;
            gdTabela.Items.Refresh();
        }

        private void SearchTable()
        {
            string aux = cbFiltro.SelectedItem as string;
            if (aux.Equals("Atendente"))
            {
                gdTabela.ItemsSource = DTOTabelaAtendimentos.Where(dto => dto.Atendente.StartsWith(this.txtLocalizar.Text, StringComparison.InvariantCultureIgnoreCase));
            }
            else if (aux.Equals("Cliente"))
            {
                gdTabela.ItemsSource = DTOTabelaAtendimentos.Where(dto => dto.Cliente.StartsWith(this.txtLocalizar.Text, StringComparison.InvariantCultureIgnoreCase) || dto.NumeroCliente.StartsWith(this.txtLocalizar.Text, StringComparison.InvariantCultureIgnoreCase));
            }
            gdTabela.Items.Refresh();
        }

        private void gdTabela_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if ((sender as DataGrid).SelectedIndex > -1)
            {
                Protocolo protocolo = Protocolos.Where(p => p.NumeroProtocolo == ((sender as DataGrid).SelectedItem as DTOTabelaAtendimento).Protocolo).FirstOrDefault();

                NovoAtendimento novo = new NovoAtendimento(protocolo, null);

                MainAtendimentoPage cadastro = MainAtendimentoPage.GetInstance();
                cadastro.Main.Content = novo.Content;

                MenuList menuList = null;
                menuList = new MenuList();
                menuList.Title = "Atendimento";
                menuList.ImagePath = IconeVetor.NovoAtendimento();
                menuList.Position = 1;

                cadastro.pages.GetOrAdd(protocolo.NumeroProtocolo, new Tuple<MenuList, object, bool>(menuList, novo, true));

                cadastro.CarregarMenuList();
                cadastro.listBox_list.SelectedIndex = cadastro.pages.Count - 1;
            }
        }

        private void txtLocalizar_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.SearchTable();
            }
        }

        private void btn_Localizar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.SearchTable();
        }
    }
}
