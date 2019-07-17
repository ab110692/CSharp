using Br.Com.Posi.Shelf.DAO;
using Br.Com.Posi.Shelf.Desktop.GUI.Outro;
using Br.Com.Posi.Shelf.Model;
using Br.Com.Posi.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Linq;

namespace Br.Com.Posi.Shelf.Desktop.GUI.Cliente
{
    /// <summary>
    /// Interaction logic for ClientePage.xaml
    /// </summary>
    public partial class ClientePage : Page
    {
        private static ClientePage _instance;

        //Select
        private Model.Cliente clienteSelect;
        private Contrato contratoSelect;
        private Telefone telefoneSelect;

        //List
        private List<Rede> redes;

        //DAO
        IRedeDAO daoRede;
        IClienteDAO daoCliente;
        ITelefoneDAO daoTelefone;

        public ClientePage(Model.Cliente cliente)
        {
            InitializeComponent();

            //Select
            clienteSelect = cliente;
            contratoSelect = new Contrato();
            telefoneSelect = new Telefone();

            //List
            redes = new List<Rede>();

            //DAO
            daoRede = DAOFactory.InitRedeDAO();
            daoCliente = DAOFactory.InitClienteDAO();
            daoTelefone = DAOFactory.InitTelefoneDAO();

            ConstructRede();
            carregarCliente(clienteSelect);
        }

        private void ConstructRede()
        {
            redes = daoRede.GetList();
            cb_Rede.Items.Clear();
            cb_Rede.ItemsSource = redes;
        }

        #region Button Event
        private void btn_Novo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((sender as Button).Content.Equals("Novo"))
                {
                    //MyNavegate.MainFrameNavegate(new ClientePage(null));
                    this.ModifyComponentToNew();
                    return;
                }
                else
                {
                    if (cb_Rede.SelectedIndex == -1)
                    {
                        BallonDialog.Show("O campo rede deve ser preenchido !", "Campo Nulo ou Inválido");
                        return;
                    }
                    if (string.IsNullOrEmpty(txt_Codigo.Text.Trim()))
                    {
                        BallonDialog.Show("O campo Código deve ser preenchido !", "Campo Nulo ou Inválido");
                        return;
                    }
                    /*if (string.IsNullOrEmpty(txt_CPF.Text.Trim()))
                    {
                        BallonDialog.Show("O campo CPF deve ser preenchido !", "Campo Nulo ou Inválido");
                        return;
                    }*/
                    if (string.IsNullOrEmpty(txt_CNPJ.Text.Trim()))
                    {
                        BallonDialog.Show("O campo CNPJ deve ser preenchido !", "Campo Nulo ou Inválido");
                        return;
                    }
                    if (string.IsNullOrEmpty(txt_RazaoSocial.Text.Trim()))
                    {
                        BallonDialog.Show("O campo Razão Social deve ser preenchido !", "Campo Nulo ou Inválido");
                        return;
                    }
                    if (string.IsNullOrEmpty(txt_NomeFantasia.Text.Trim()))
                    {
                        BallonDialog.Show("O campo Nome Fantasia deve ser preenchido !", "Campo Nulo ou Inválido");
                        return;
                    }
                    if (string.IsNullOrEmpty(txt_Email.Text.Trim()))
                    {
                        BallonDialog.Show("O campo Email deve ser preenchido !", "Campo Nulo ou Inválido");
                        return;
                    }
                    if (string.IsNullOrEmpty(txt_CEP.Text.Trim()))
                    {
                        BallonDialog.Show("O campo CEP deve ser preenchido !", "Campo Nulo ou Inválido");
                        return;
                    }
                    if (string.IsNullOrEmpty(txt_Endereco.Text.Trim()))
                    {
                        BallonDialog.Show("O campo Apelido deve ser preenchido !", "Campo Nulo ou Inválido");
                        return;
                    }
                    if (string.IsNullOrEmpty(txt_Bairro.Text.Trim()))
                    {
                        BallonDialog.Show("O campo Bairro deve ser preenchido !", "Campo Nulo ou Inválido");
                        return;
                    }
                    if (string.IsNullOrEmpty(txt_Numero.Text.Trim()))
                    {
                        BallonDialog.Show("O campo Número deve ser preenchido !", "Campo Nulo ou Inválido");
                        return;
                    }
                    if (string.IsNullOrEmpty(txt_Cidade.Text.Trim()))
                    {
                        BallonDialog.Show("O campo Cidade deve ser preenchido !", "Campo Nulo ou Inválido");
                        return;
                    }
                    if (string.IsNullOrEmpty(txt_Estado.Text.Trim()))
                    {
                        BallonDialog.Show("O campo Estado deve ser preenchido !", "Campo Nulo ou Inválido");
                        return;
                    }
                }
                if ((sender as Button).Content.Equals("Salvar"))
                {
                    novoCadastro();
                }
                else if ((sender as Button).Content.Equals("Alterar"))
                {
                    alterarCadastro();
                }
            }
            catch (Exception ex)
            {
                BallonDialog.Show(ex.Message, "Erro");
            }
        }

        private void btn_InformacoesAdicionais_Click(object sender, RoutedEventArgs e)
        {
            PrincipalWindow.GetInstance().Main.Navegate(new ComputadorPage(clienteSelect));
        }

        private void btn_Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.ModiftComponentToCancel();
            this.Clear();
        }

        private void btn_Voltar_Click(object sender, RoutedEventArgs e)
        {
            if (btn_Novo.Visibility == Visibility.Visible)
            {
                if (MessageDialog.Show(Window.GetWindow(this), "Foi identificado que houveram alterações que ainda não foram salvas, deseja salva-las?", "Sair", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == true)
                {
                    alterarCadastro();
                }
            }
            PrincipalWindow.GetInstance().Main.Navegate(LocalizarClientePage.GetInstance());
        }

        private void btn_AdicionarContato_Click(object sender, RoutedEventArgs e)
        {
            habilitarDesabilitarTelefones(true);
            btn_AdicionarContato.Visibility = Visibility.Collapsed;
            btn_SalvarContato.Visibility = Visibility.Visible;
        }

        private void btn_SalvarContato_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_Contato.Text.Trim()) &&
                !string.IsNullOrEmpty(txt_Tel.Text.Trim()) &&
                !string.IsNullOrEmpty(txt_Cargo.Text.Trim()))
            {
                Telefone telefone = new Telefone();
                telefone.Nome = txt_Contato.Text;
                telefone.Numero = txt_Tel.Text;
                telefone.Cargo = txt_Cargo.Text;
                clienteSelect.Telefones.Add(telefone);
                habilitarDesabilitarTelefones(false);
                dg_Contato.Items.Refresh();
                txt_Contato.Text = string.Empty;
                txt_Tel.Text = string.Empty;
                txt_Cargo.Text = string.Empty;

                btn_AdicionarContato.Visibility = Visibility.Visible;
                btn_SalvarContato.Visibility = Visibility.Collapsed;
                btn_Alterar.Visibility = Visibility.Collapsed;
                btn_Novo.Visibility = Visibility.Visible;
                btn_Novo.Content = "Alterar";
            }
        }

        private void btn_Alterar_Click(object sender, RoutedEventArgs e)
        {
            btn_Alterar.Visibility = Visibility.Collapsed;
            btn_Novo.Visibility = Visibility.Visible;
            habilitarDesabilitarDadosCadastrais(true);
            habilitarDesabilitarEndereço(true);
            habilitarDesabilitarTelefones(true);
        }

        private void btn_RemoverContato_Click(object sender, RoutedEventArgs e)
        {
            if (btn_RemoverContato.Content.Equals("Remover"))
            {
                if (!string.IsNullOrWhiteSpace(txt_Contato.Text) || !string.IsNullOrWhiteSpace(txt_Cargo.Text))
                {
                    if (MessageDialog.Show(Window.GetWindow(this), "Deseja realmente excluir este contato?", "Exclusão de contato", MessageBoxButton.YesNo, MessageBoxImage.Question) == true)
                    {
                        try
                        {
                            Telefone t = (Telefone)dg_Contato.SelectedItem;

                            telefoneSelect = t;
                            daoTelefone.Delete(telefoneSelect);
                            dg_Contato.ItemsSource = clienteSelect.Telefones;

                            txt_Contato.Text = string.Empty;
                            txt_Tel.Text = string.Empty;
                            txt_Cargo.Text = string.Empty;
                            BallonDialog.Show("Contato excluído com sucesso", "Exclusão de contato");
                            return;
                        }
                        catch (Exception ex)
                        {
                            BallonDialog.Show(ex.Message, "Erro");
                        }
                    }
                    else
                    {
                        BallonDialog.Show("Contato não excluído", "Exclusão de contato");
                    }

                }
                else
                {
                    BallonDialog.Show("Contato não selecionado", "Exclusão de contato");
                }
            }
        }
        #endregion

        #region Manipuladores
        private void ModifyComponentToNew()
        {
            this.btn_Novo.Content = "Salvar";
            this.btn_Cancelar.IsEnabled = true;
        }

        private void ModiftComponentToSave()
        {
            this.btn_Novo.Content = "Novo";
            this.btn_Cancelar.IsEnabled = false;
        }

        private void ModiftComponentToAlter()
        {
            this.btn_Novo.Content = "Novo";
            this.btn_Cancelar.IsEnabled = false;
        }

        private void ModiftComponentToCancel()
        {
            this.btn_Novo.Content = "Novo";
            this.btn_Cancelar.IsEnabled = false;
        }

        private void Clear()
        {
            this.cb_Rede.SelectedIndex = -1;
            this.txt_Codigo.Text = string.Empty;
            this.txt_CNPJ.Text = string.Empty;
            //this.txt_CPF.Text = string.Empty;
            this.txt_RazaoSocial.Text = string.Empty;
            this.txt_NomeFantasia.Text = string.Empty;
            this.txt_IE.Text = string.Empty;
            this.txt_Email.Text = string.Empty;
            this.txt_CEP.Text = string.Empty;
            this.txt_Endereco.Text = string.Empty;
            this.txt_Numero.Text = string.Empty;
            this.txt_Bairro.Text = string.Empty;
            this.txt_Cidade.Text = string.Empty;
            this.txt_Estado.Text = string.Empty;

            this.txt_Tel.Text = string.Empty;
            this.txt_Contato.Text = string.Empty;
            this.txt_Cargo.Text = string.Empty;
        }
        #endregion

        #region Text Events
        private void txt_Codigo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void textBox_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Size n = e.NewSize;
            Size p = e.PreviousSize;
            double l = n.Width / p.Width;
            if (l != double.PositiveInfinity)
            {
                txt_Codigo.FontSize = txt_Codigo.FontSize * l;
                txt_CNPJ.FontSize = txt_CNPJ.FontSize * l;
                //txt_CPF.FontSize = txt_CPF.FontSize * l;
                txt_RazaoSocial.FontSize = txt_RazaoSocial.FontSize * l;
                txt_NomeFantasia.FontSize = txt_NomeFantasia.FontSize * l;
                txt_IE.FontSize = txt_IE.FontSize * l;
                txt_Email.FontSize = txt_Email.FontSize * l;
                txt_Tel.FontSize = txt_Tel.FontSize * 1;
                txt_Contato.FontSize = txt_Contato.FontSize * 1;
                txt_Cargo.FontSize = txt_Cargo.FontSize * 1;
                txt_CEP.FontSize = txt_CEP.FontSize * 1;
                txt_Endereco.FontSize = txt_Endereco.FontSize * 1;
                txt_Bairro.FontSize = txt_Bairro.FontSize * 1;
                txt_Cidade.FontSize = txt_Cidade.FontSize * 1;
                txt_Estado.FontSize = txt_Cidade.FontSize * 1;
            }
        }

        private void txt_CEP_TextChanged(object sender, TextChangedEventArgs e)
        {
            CEP cep = new CEP();

            try
            {
                if (txt_CEP.Text.Length == 9)
                {
                    cep = buscarCEP(txt_CEP.Text);

                    if (cep.Resultado == "1")
                    {
                        txt_Endereco.Text = cep.Logradouro;
                        txt_Bairro.Text = cep.Bairro;
                        txt_Cidade.Text = cep.Cidade;
                        txt_Estado.Text = cep.UF;
                        habilitarDesabilitarEndereço(false);
                        txt_CEP.IsEnabled = true;
                    }
                    else if (cep.Resultado == "0")
                    {
                        BallonDialog.Show("Por favor, verifique o CEP ou digite o endereço completo.", "CEP inválido ou inxistente");
                        txt_Endereco.Text = string.Empty;
                        txt_Bairro.Text = string.Empty;
                        txt_Cidade.Text = string.Empty;
                        txt_Estado.Text = string.Empty;
                        habilitarDesabilitarEndereço(true);

                        return;
                    }
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    var response = ex.Response as HttpWebResponse;
                    if (response != null)
                    {
                        if (cep.Resultado == "1")
                        {
                            txt_Endereco.Text = cep.Logradouro;
                            txt_Bairro.Text = cep.Bairro;
                            txt_Cidade.Text = cep.Cidade;
                            txt_Estado.Text = cep.UF;
                            habilitarDesabilitarEndereço(false);
                        }
                        else if (cep.Resultado == "0")
                        {
                            BallonDialog.Show("Por favor, verifique o CEP ou digite o endereço completo.", "CEP inválido ou inxistente");
                            habilitarDesabilitarEndereço(true);
                            return;
                        }
                    }
                    else
                    {
                        BallonDialog.Show("Impossível se comunicar com o servidor de busca para encontrar o endereço. Por favor preencha o endereço manualmente", "Sem resposta do servidor de busca");
                        habilitarDesabilitarEndereço(true);
                        return;
                    }
                }
                else
                {
                    BallonDialog.Show("Impossível se conectar à internet. Por favor preencha o endereço manualmente", "Erro de conexão com a internet");
                    habilitarDesabilitarEndereço(true);
                    return;
                }
            }
        }

        private void txt_CEP_TextInput(object sender, TextCompositionEventArgs e)
        {

        }
        #endregion

        #region CEP
        public class CEP
        {
            public string Logradouro { get; set; }
            public string TipoLogradouro { get; set; }
            public string Bairro { get; set; }
            public string Cidade { get; set; }
            public string UF { get; set; }
            public string Resultado { get; set; }
            public string ResultadoMensagem { get; set; }

        }

        public CEP buscarCEP(string c)
        {
            CEP modeloRetorno = new CEP();

            string caminhoXML = "http://cep.republicavirtual.com.br/web_cep.php?cep=" + c.Replace("-", "").Trim() + "&formato=xml";
            XDocument documentoXML = XDocument.Load(caminhoXML);
            modeloRetorno.TipoLogradouro = documentoXML.Descendants().Elements("tipo_logradouro").First().Value;
            modeloRetorno.Logradouro = documentoXML.Descendants().Elements("logradouro").First().Value;
            modeloRetorno.Bairro = documentoXML.Descendants().Elements("bairro").First().Value;
            modeloRetorno.Cidade = documentoXML.Descendants().Elements("cidade").First().Value;
            modeloRetorno.UF = documentoXML.Descendants().Elements("uf").First().Value;
            modeloRetorno.Resultado = documentoXML.Descendants().Elements("resultado").First().Value;
            modeloRetorno.ResultadoMensagem = documentoXML.Descendants().Elements("resultado_txt").First().Value;

            return modeloRetorno;

        }

        private void habilitarDesabilitarDadosCadastrais(bool b)
        {
            cb_Rede.IsEnabled = b;
            txt_Codigo.IsEnabled = b;
            //txt_CPF.IsEnabled = b;
            txt_CNPJ.IsEnabled = b;
            txt_RazaoSocial.IsEnabled = b;
            txt_NomeFantasia.IsEnabled = b;
            txt_IE.IsEnabled = b;
            txt_Email.IsEnabled = b;
            if (b == false)
            {
                cb_Rede.Background = new SolidColorBrush(Color.FromRgb(204, 204, 204));
                txt_Codigo.Background = new SolidColorBrush(Color.FromRgb(204, 204, 204));
                //txt_CPF.Background = new SolidColorBrush(Color.FromRgb(204, 204, 204));
                txt_CNPJ.Background = new SolidColorBrush(Color.FromRgb(204, 204, 204));
                txt_RazaoSocial.Background = new SolidColorBrush(Color.FromRgb(204, 204, 204));
                txt_NomeFantasia.Background = new SolidColorBrush(Color.FromRgb(204, 204, 204));
                txt_IE.Background = new SolidColorBrush(Color.FromRgb(204, 204, 204));
                txt_Email.Background = new SolidColorBrush(Color.FromRgb(204, 204, 204));
            }
            else
            {
                cb_Rede.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                txt_Codigo.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                //txt_CPF.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                txt_CNPJ.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                txt_RazaoSocial.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                txt_NomeFantasia.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                txt_IE.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                txt_Email.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            }
        }

        private void habilitarDesabilitarTelefones(bool b)
        {
            txt_Tel.IsEnabled = b;
            txt_Contato.IsEnabled = b;
            txt_Cargo.IsEnabled = b;
            if (b == false)
            {
                txt_Tel.Background = new SolidColorBrush(Color.FromRgb(204, 204, 204));
                txt_Contato.Background = new SolidColorBrush(Color.FromRgb(204, 204, 204));
                txt_Cargo.Background = new SolidColorBrush(Color.FromRgb(204, 204, 204));
            }
            else
            {
                txt_Tel.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                txt_Contato.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                txt_Cargo.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            }
        }

        private void habilitarDesabilitarEndereço(bool b)
        {
            txt_CEP.IsEnabled = b;
            txt_Endereco.IsEnabled = b;
            txt_Numero.IsEnabled = b;
            txt_Bairro.IsEnabled = b;
            txt_Cidade.IsEnabled = b;
            txt_Estado.IsEnabled = b;

            if (b == false)
            {
                txt_CEP.Background = new SolidColorBrush(Color.FromRgb(204, 204, 204));
                txt_Endereco.Background = new SolidColorBrush(Color.FromRgb(204, 204, 204));
                txt_Numero.Background = new SolidColorBrush(Color.FromRgb(204, 204, 204));
                txt_Bairro.Background = new SolidColorBrush(Color.FromRgb(204, 204, 204));
                txt_Cidade.Background = new SolidColorBrush(Color.FromRgb(204, 204, 204));
                txt_Estado.Background = new SolidColorBrush(Color.FromRgb(204, 204, 204));
            }
            else
            {
                txt_CEP.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                txt_Endereco.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                txt_Numero.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                txt_Bairro.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                txt_Cidade.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                txt_Estado.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            }
        }

        private void dg_Contato_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Telefone t = (Telefone)dg_Contato.SelectedItem;
            txt_Tel.Text = t.Numero;
            txt_Contato.Text = t.Nome;
            txt_Cargo.Text = t.Cargo;
        }

        #endregion

        private void alterarCadastro()
        {
            clienteSelect.Rede = (Rede)cb_Rede.SelectedItem;
            clienteSelect.CpfCnpj = txt_CNPJ.Text;
            clienteSelect.Codigo = Convert.ToInt64(txt_Codigo.Text);
            clienteSelect.RazaoSocial = txt_RazaoSocial.Text;
            clienteSelect.NomeFantasia = txt_NomeFantasia.Text;
            clienteSelect.InscricaoEstadual = txt_IE.Text;
            clienteSelect.Email = txt_Email.Text;
            clienteSelect.Cep = txt_CEP.Text;
            clienteSelect.Endereco = txt_Endereco.Text;
            clienteSelect.Bairro = txt_Bairro.Text;
            clienteSelect.Numero = txt_Numero.Text;
            clienteSelect.Cidade = txt_Cidade.Text;
            clienteSelect.Estado = txt_Estado.Text;

            daoCliente.Update(clienteSelect);

            habilitarDesabilitarDadosCadastrais(false);
            habilitarDesabilitarEndereço(false);
            habilitarDesabilitarTelefones(false);

            btn_Alterar.Visibility = Visibility.Visible;
            btn_Novo.Visibility = Visibility.Collapsed;

            this.ModiftComponentToAlter();

            BallonDialog.Show("Cliente modificado com sucesso !", "Mensagem");
            //MyNavegate.MainFrameNavegate(new LocalizarClientePage());
        }

        private void novoCadastro()
        {
            clienteSelect = new Br.Com.Posi.Shelf.Model.Cliente();
            clienteSelect.Rede = (Rede)cb_Rede.SelectedItem;
            clienteSelect.CpfCnpj = txt_CNPJ.Text;
            clienteSelect.Codigo = Convert.ToInt64(txt_Codigo.Text);
            clienteSelect.RazaoSocial = txt_RazaoSocial.Text;
            clienteSelect.NomeFantasia = txt_NomeFantasia.Text;
            clienteSelect.InscricaoEstadual = txt_IE.Text;
            clienteSelect.Email = txt_Email.Text;
            clienteSelect.Cep = txt_CEP.Text;
            clienteSelect.Endereco = txt_Endereco.Text;
            clienteSelect.Bairro = txt_Bairro.Text;
            clienteSelect.Numero = txt_Numero.Text;
            clienteSelect.Cidade = txt_Cidade.Text;
            clienteSelect.Estado = txt_Estado.Text;

            daoCliente.Save(clienteSelect);
            this.ModiftComponentToSave();

            habilitarDesabilitarDadosCadastrais(false);
            habilitarDesabilitarEndereço(false);
            habilitarDesabilitarTelefones(false);

            BallonDialog.Show("Novo cliente adicionado com sucesso !", "Mensagem");
            this.Clear();
        }

        private void carregarCliente(Br.Com.Posi.Shelf.Model.Cliente cliente)
        {
            this.Clear();

            if (cliente != null)
            {
                this.txt_CEP.TextChanged -= new TextChangedEventHandler(txt_CEP_TextChanged);
                this.clienteSelect = cliente;
                this.btn_Novo.Content = "Alterar";

                dg_Contato.ItemsSource = cliente.Telefones;
                dg_Contato.Items.Refresh();

                btn_Novo.Visibility = Visibility.Collapsed;
                btn_Alterar.Visibility = Visibility.Visible;
                cb_Rede.SelectedItem = clienteSelect.Rede;
                txt_Codigo.Text = clienteSelect.Codigo.ToString();

                txt_RazaoSocial.Text = clienteSelect.RazaoSocial;
                txt_NomeFantasia.Text = clienteSelect.NomeFantasia;
                txt_IE.Text = clienteSelect.InscricaoEstadual;
                txt_Email.Text = clienteSelect.Email;
                txt_CEP.Text = clienteSelect.Cep;
                txt_Endereco.Text = clienteSelect.Endereco;
                txt_Bairro.Text = clienteSelect.Bairro;
                txt_Numero.Text = clienteSelect.Numero;
                txt_Cidade.Text = clienteSelect.Cidade;
                txt_Estado.Text = clienteSelect.Estado;

                habilitarDesabilitarDadosCadastrais(false);
                habilitarDesabilitarEndereço(false);
                habilitarDesabilitarTelefones(false);
                btn_Cancelar.IsEnabled = false;
            }
            else
            {
                this.btn_Novo.Content = "Salvar";
                this.btn_InformacoesAdicionais.Visibility = Visibility.Collapsed;
                this.txt_CEP.TextChanged += new TextChangedEventHandler(txt_CEP_TextChanged);
                this.btn_Voltar.SetValue(Grid.ColumnProperty, 1);
                habilitarDesabilitarDadosCadastrais(true);
                habilitarDesabilitarEndereço(true);
                habilitarDesabilitarTelefones(true);
            }
        }
    }
}