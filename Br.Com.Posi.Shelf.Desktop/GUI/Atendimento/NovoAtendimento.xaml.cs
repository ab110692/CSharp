using Br.Com.Posi.Shelf.DAO;
using Br.Com.Posi.Shelf.DTO;
using Br.Com.Posi.Shelf.Enums;
using Br.Com.Posi.Shelf.Model;
using Br.Com.Posi.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Br.Com.Posi.Shelf.Desktop.GUI.Atendimento
{
    /// <summary>
    /// Interaction logic for NovoAtendimento.xaml
    /// </summary>
    public partial class NovoAtendimento : Window, IDisposable
    {
        private Protocolo protocoloSelect;
        private Model.Funcionario funcionarioSelect;

        private ICategoriaDAO daoCategoria;
        private IClienteDAO daoCliente;
        private IAplicativoDAO daoAplicativo;

        private IProtocoloDAO daoProtocolo;
        private IAtendimentoDAO daoAtendimento;
        private IAtendimentoDetalhadoDAO daoAtendimentoDetalhado;
        private IProblemaDAO daoProblema;

        //List
        private List<Categoria> categorias;
        private List<Aplicativo> aplicativos;
        private List<Model.Cliente> clientes;
        private List<DTOTabelaCategoria> DTOTabelaCategoriaList;
        private List<Rede> redes;
        private List<StatusAtendimento> statusAtendimentos;

        public NovoAtendimento(Protocolo protocolo, Model.Funcionario funcionario, List<Rede> redes)
        {
            InitializeComponent();
            //DAO
            this.LoadDAO();
            //List
            this.clientes = new List<Model.Cliente>();
            this.redes = new List<Rede>();
            this.DTOTabelaCategoriaList = new List<DTOTabelaCategoria>();
            this.statusAtendimentos = Enum.GetValues(typeof(StatusAtendimento)).Cast<StatusAtendimento>().ToList();
            //Select
            this.protocoloSelect = protocolo;
            this.funcionarioSelect = funcionario;
            this.redes = redes;
            //Source
            this.dgTabela.ItemsSource = DTOTabelaCategoriaList;
            //Load ComboBox
            this.LoadDataCategoria();
            this.LoadDataAplicativo();
            this.LoadDataRede();
            this.LoadStatusAtendimento(statusAtendimentos);
            //Load Date
            this.txtDataRegistro.Text = DAOFactory.InitAntivirusDAO().GetDateTime().ToString();
            //Carrega atendimento
            this.LoadAtendimento(protocolo);
            this.LoadFuncionario(funcionario);
        }

        public NovoAtendimento(Protocolo protocolo, List<Rede> redes) : this(protocolo, null, redes) { }

        #region Load
        private void LoadDataCategoria()
        {
            categorias = new List<Categoria>();
            categorias.AddRange(daoCategoria.GetList());
            cbCategoria.ItemsSource = categorias;
        }

        private void LoadDAO()
        {
            daoCategoria = DAOFactory.InitCategoriaDAO();
            daoCliente = DAOFactory.InitClienteDAO();
            daoAplicativo = DAOFactory.InitAplicativoDAO();
            //daoRede = DAOFactory.InitRedeDAO();

            daoProtocolo = DAOFactory.InitProtocoloDAO();
            daoAtendimento = DAOFactory.InitAtendimentoDAO();
            daoAtendimentoDetalhado = DAOFactory.InitAtendimentoDetalhadoDAO();
            daoProblema = DAOFactory.InitProblemaDAO();
        }

        private void LoadDataAplicativo()
        {
            aplicativos = new List<Aplicativo>();
            aplicativos.AddRange(daoAplicativo.GetList());
            cbAplicativo.ItemsSource = aplicativos;
        }

        private void LoadDataRede()
        {
            cbRede.ItemsSource = redes;
        }

        private void LoadDataCliente(Rede rede)
        {
            clientes.Clear();
            clientes.AddRange(daoCliente.GetClienteByRede(rede));

            cbRazaoSocial.DisplayMemberPath = "RazaoSocial";
            cbNomeFantasia.DisplayMemberPath = "NomeFantasia";

            cbRazaoSocial.ItemsSource = clientes;
            cbNomeFantasia.ItemsSource = clientes;

            cbRazaoSocial.Items.Refresh();
            cbNomeFantasia.Items.Refresh();
        }

        private void LoadDataCliente(Model.Cliente cliente)
        {
            clientes.Clear();
            clientes.Add(cliente);

            cbRazaoSocial.DisplayMemberPath = "RazaoSocial";
            cbNomeFantasia.DisplayMemberPath = "NomeFantasia";

            cbRazaoSocial.ItemsSource = clientes;
            cbNomeFantasia.ItemsSource = clientes;

            cbRazaoSocial.Items.Refresh();
            cbNomeFantasia.Items.Refresh();
        }

        private void LoadStatusAtendimento(List<StatusAtendimento> statusAtendimentos)
        {
            foreach (StatusAtendimento status in statusAtendimentos)
            {
                cbStatusAtendimento.Items.Add(status.GetName());
            }
        }
        #endregion

        #region Event
        private void cbRazaoSocial_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Model.Cliente cliente = cbRazaoSocial.SelectedItem as Model.Cliente;
            if (cliente != null)
            {
                this.cbNomeFantasia.SelectedItem = cliente;
                this.txtCodigoLoja.Text = cliente.Codigo.ToString();
                this.lbTelefone.Content = cliente.Telefones.FirstOrDefault()?.Numero;
                this.lbCelular.Content = cliente.Telefones.LastOrDefault()?.Numero;
            }
        }

        private void cbNomeFantasia_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Model.Cliente cliente = cbNomeFantasia.SelectedItem as Model.Cliente;
            if (cliente != null)
            {
                this.cbRazaoSocial.SelectedItem = cliente;
                this.txtCodigoLoja.Text = cliente.Codigo.ToString();
                this.lbTelefone.Content = cliente.Telefones.FirstOrDefault()?.Numero;
                this.lbCelular.Content = cliente.Telefones.LastOrDefault()?.Numero;
            }
        }

        private void txtCodigoLoja_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter && !string.IsNullOrEmpty(txtCodigoLoja.Text))
            {
                if (cbRede.SelectedIndex == -1)
                {
                    BallonDialog.Show("Por Favor, selecione uma rede", "Alerta !");
                    return;
                }
                if (!clientes.Where(cli => cli.Codigo == Convert.ToInt64(txtCodigoLoja.Text)).Any())
                {
                    BallonDialog.Show("Cliente, inexistente", "Alerta !");
                    return;
                }

                Model.Cliente cliente = clientes.Where(cli => cli.Codigo == Convert.ToInt64(txtCodigoLoja.Text)).Single();
                this.cbRazaoSocial.SelectedItem = cliente;
                this.cbNomeFantasia.SelectedItem = cliente;
                this.lbTelefone.Content = cliente.Telefones.FirstOrDefault()?.Numero;
                this.lbCelular.Content = cliente.Telefones.LastOrDefault()?.Numero;
            }
        }

        private void cbCategoria_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Categoria categoria = cbCategoria.SelectedItem as Categoria;
            if (categoria != null)
            {
                cbSubCategoria.ItemsSource = categoria.SubCategorias;
                cbSubCategoria.Items.Refresh();
            }
            cbItem.ItemsSource = null;
            cbItem.Items.Refresh();
        }

        private void cbSubCategoria_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SubCategoria subCategoria = cbSubCategoria.SelectedItem as SubCategoria;
            if (subCategoria != null)
            {
                cbItem.ItemsSource = subCategoria.Items;
                cbItem.Items.Refresh();
            }
        }

        private void cbAplicativo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbAplicativo.SelectedIndex != -1)
            {
                Aplicativo aplicativo = cbAplicativo.SelectedItem as Aplicativo;
                cbVersao.ItemsSource = aplicativo.Versoes;
                cbVersao.Items.Refresh();
            }
        }

        private void btnAdicionar_Click(object sender, RoutedEventArgs e)
        {
            if (cbCategoria.SelectedIndex == -1)
            {
                BallonDialog.Show("Por favor, selecione uma categoria", "Alerta !");
                return;
            }
            if (cbSubCategoria.SelectedIndex == -1)
            {
                BallonDialog.Show("Por favor, selecione uma subcategoria", "Alerta !");
                return;
            }
            if (cbItem.SelectedIndex == -1)
            {
                BallonDialog.Show("Por favor, selecione uma item", "Alerta !");
                return;
            }
            if (cbAplicativo.SelectedIndex == -1)
            {
                BallonDialog.Show("Por favor, selecione uma aplicação", "Alerta !");
                return;
            }
            if (cbVersao.SelectedIndex == -1)
            {
                BallonDialog.Show("Por favor, selecione uma versão", "Alerta !");
                return;
            }

            Categoria categoria = cbCategoria.SelectedItem as Categoria;
            SubCategoria subCategoria = cbSubCategoria.SelectedItem as SubCategoria;
            Item item = cbItem.SelectedItem as Item;
            Aplicativo aplicativo = cbAplicativo.SelectedItem as Aplicativo;
            Versao versao = cbVersao.SelectedItem as Versao;

            DTOTabelaCategoria dtoCategoria = new DTOTabelaCategoria() { Categoria = categoria, SubCategoria = subCategoria, Item = item, Aplicativo = aplicativo, Versao = versao };

            if (DTOTabelaCategoriaList.Where(dto => dto.Aplicativo.Equals(aplicativo) && dto.Categoria.Equals(categoria) && dto.SubCategoria.Equals(subCategoria) && dto.Item.Equals(item) && dto.Versao.Equals(versao)).Any())
            {
                BallonDialog.Show("Categoria já cadastrada !", "Alerta !");
                return;
            }

            DTOTabelaCategoriaList.Add(dtoCategoria);

            cbCategoria.SelectedIndex = -1;
            cbSubCategoria.SelectedIndex = -1;
            cbItem.SelectedIndex = -1;
            cbAplicativo.SelectedIndex = -1;
            cbVersao.SelectedIndex = -1;

            dgTabela.Items.Refresh();
        }

        private void btnRemover_Click(object sender, RoutedEventArgs e)
        {
            if (dgTabela.SelectedIndex == -1)
            {
                BallonDialog.Show("Para remover você deve selecionar um item na lista !", "Alerta !");
                return;
            }

            DTOTabelaCategoria dto = dgTabela.SelectedItem as DTOTabelaCategoria;

            DTOTabelaCategoriaList.Remove(dto);

            dgTabela.Items.Refresh();
        }

        private void cbRede_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbRede.SelectedIndex != -1)
            {
                LoadDataCliente(cbRede.SelectedItem as Rede);
            }
        }

        private void btnFinalizar_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Content.Equals("Assumir Atendimento"))
            {
                (sender as Button).Content = "Finalizar";
                this.UnLock();
                this.Clear(false);
                return;
            }
            if ((cbNomeFantasia.SelectedItem as Model.Cliente) == null)
            {
                BallonDialog.Show("Informar o cliente !", "Alerta");
                return;
            }
            if (dgTabela.Items.Count == 0)
            {
                BallonDialog.Show("Informar o(s) problema(s) que ocorre no cliente", "Alerta");
                return;
            }
            if (string.IsNullOrEmpty(txtContato.Text.Trim()))
            {
                BallonDialog.Show("Informar o nome do cliente", "Alerta");
                return;
            }
            if (string.IsNullOrEmpty(txtSolucao.Text.Trim()))
            {
                BallonDialog.Show("Informar a solução", "Alerta");
                return;
            }

            Model.Atendimento atendimento = protocoloSelect.Atendimento ?? new Model.Atendimento();
            atendimento.Protocolo = this.protocoloSelect;
            atendimento.Cliente = cbNomeFantasia.SelectedItem as Model.Cliente;
            atendimento.Cliente.Rede = cbRede.SelectedItem as Rede;

            AtendimentoDetalhado atendimentoDetalhado = new AtendimentoDetalhado();
            atendimentoDetalhado.StatusAtendimento = StatusAtendimento.Aguardando_Feedback;
            atendimentoDetalhado.Funcionario = this.funcionarioSelect;
            atendimentoDetalhado.DataInicio = DateTime.Parse(txtDataRegistro.Text);
            atendimentoDetalhado.DataFinal = daoCliente.GetDateTime();
            atendimentoDetalhado.Solucao = txtSolucao.Text;
            atendimentoDetalhado.Contato = txtContato.Text;

            foreach (DTOTabelaCategoria dto in DTOTabelaCategoriaList)
            {
                Problema problema = new Problema();
                problema.Categoria = dto.Categoria;
                problema.SubCategoria = dto.SubCategoria;
                problema.Item = dto.Item;
                problema.Aplicativo = dto.Aplicativo;
                problema.Versao = dto.Versao;
                problema.Atendimento = atendimento;
                if (!atendimento.Problemas.Where(pro => pro.Aplicativo.Equals(problema.Aplicativo) && pro.Categoria.Equals(problema.Categoria) && pro.SubCategoria.Equals(problema.SubCategoria) && pro.Item.Equals(problema.Item) && pro.Versao.Equals(problema.Versao)).Any())
                {
                    atendimento.Problemas.Add(problema);
                }
            }

            atendimento.AtendimentoDetalhado.Add(atendimentoDetalhado);
            protocoloSelect.Atendimento = atendimento;

            this.protocoloSelect = daoProtocolo.SaveOrUpdate(this.protocoloSelect);

            MainAtendimentoPage.GetInstance().FinalizarAtendimento(protocoloSelect);
        }
        #endregion

        #region Method
        private void LoadAtendimento(Protocolo protocolo)
        {
            this.txtProtocolo.Text = protocolo.NumeroProtocolo.ToString() ?? string.Empty;

            if (protocolo?.Atendimento == null)
            {
                return;
            }

            this.lbNomeFuncionario.Content = protocolo?.Atendimento?.AtendimentoDetalhado?.LastOrDefault().Funcionario?.Nome ?? string.Empty;
            this.txtDataRegistro.Text = protocolo?.Atendimento?.AtendimentoDetalhado?.LastOrDefault().DataFinal.ToShortDateString() ?? string.Empty;
            this.txtContato.Text = protocolo?.Atendimento?.AtendimentoDetalhado?.LastOrDefault().Contato ?? string.Empty;
            Model.Cliente cliente = daoCliente.GetByPK(protocolo?.Atendimento?.Cliente?.IDCliente ?? default(long));
            this.LoadDataCliente(cliente);
            Rede rede = DAOFactory.InitRedeDAO().GetRede(cliente);
            this.redes = new List<Rede>();
            this.redes.Add(rede);
            this.cbRede.ItemsSource = this.redes;
            this.cbRede.Items.Refresh();
            this.cbRede.SelectedItem = redes.Where(r => r.IDRede == rede.IDRede).SingleOrDefault();
            this.txtCodigoLoja.Text = protocolo?.Atendimento?.Cliente?.Codigo.ToString() ?? default(int).ToString();
            this.cbRazaoSocial.SelectedItem = clientes.Where(c => c.IDCliente == cliente.IDCliente).SingleOrDefault();
            this.cbNomeFantasia.SelectedItem = clientes.Where(c => c.IDCliente == cliente.IDCliente).SingleOrDefault();
            this.lbTelefone.Content = clientes.Where(c => c.IDCliente == cliente.IDCliente).SingleOrDefault()?.Telefones.FirstOrDefault()?.Numero;
            this.lbCelular.Content = clientes.Where(c => c.IDCliente == cliente.IDCliente).SingleOrDefault()?.Telefones.LastOrDefault()?.Numero;

            #region Load DataGrid Problema
            foreach (Problema problema in protocolo?.Atendimento?.Problemas.ToList())
            {
                Categoria categoria = problema.Categoria;
                SubCategoria subCategoria = problema.SubCategoria;
                Item item = problema.Item;
                Aplicativo aplicativo = problema.Aplicativo;
                Versao versao = problema.Versao;

                DTOTabelaCategoria dtoCategoria = new DTOTabelaCategoria() { Categoria = categoria, SubCategoria = subCategoria, Item = item, Aplicativo = aplicativo, Versao = versao };

                DTOTabelaCategoriaList.Add(dtoCategoria);
                dgTabela.Items.Refresh();
            }
            #endregion

            this.txtSolucao.Text = protocolo?.Atendimento?.AtendimentoDetalhado?.LastOrDefault().Solucao ?? string.Empty;

            this.Lock();

            this.btnFinalizar.Content = "Assumir Atendimento";
        }

        private void LoadFuncionario(Model.Funcionario funcionario)
        {
            if (funcionario == null)
            {
                return;
            }
            lbNomeFuncionario.Content = funcionario.Nome;
        }

        private void Clear(bool clearAll)
        {
            if (clearAll)
            {
                this.ClearAllComponentWithTextOrItem();
            }
            else
            {
                UIElementExtension.ClearAllComponentWithTextOrItem(txtSolucao, txtContato);
            }
        }

        private void Lock()
        {
            UIElementExtension.IsEnableUIElement(false, txtContato,cbRede, txtCodigoLoja, cbNomeFantasia, cbRazaoSocial, cbCategoria, cbSubCategoria,cbItem,cbAplicativo,cbVersao,btnAdicionar,btnRemover,dgTabela,dgImagem,cbStatusAtendimento,btnHistorico,btnImprimir,txtSolucao);
        }

        private void UnLock()
        {
            UIElementExtension.IsEnableUIElement(true, txtContato,txtSolucao,dgImagem);
        }
        #endregion

        public void Dispose()
        {
            if (categorias != null)
            {
                categorias.Clear();
                categorias = null;
            }
            if (aplicativos != null)
            {
                aplicativos.Clear();
                aplicativos = null;
            }
            if (clientes != null)
            {
                clientes.Clear();
                clientes = null;
            }
            if (DTOTabelaCategoriaList != null)
            {
                DTOTabelaCategoriaList.Clear();
                DTOTabelaCategoriaList = null;
            }
            if (redes != null)
            {
                redes.Clear();
                redes = null;
            }
            cbAplicativo.ItemsSource = null;
            cbCategoria.ItemsSource = null;
            cbItem.ItemsSource = null;
            cbNomeFantasia.ItemsSource = null;
            cbRazaoSocial.ItemsSource = null;
            cbRede.ItemsSource = null;
            cbSubCategoria.ItemsSource = null;
            cbVersao.ItemsSource = null;
        }
    }
}
