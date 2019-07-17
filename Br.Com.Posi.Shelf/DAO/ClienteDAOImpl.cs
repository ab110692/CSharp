using Br.Com.Posi.Connection;
using Br.Com.Posi.Connection.Util;
using Br.Com.Posi.Shelf.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;

namespace Br.Com.Posi.Shelf.DAO
{
    sealed class ClienteDAOImpl : DAOImpl<Cliente>, IClienteDAO
    {        
        private IContratoDAO contratoDAO;
        private ITelefoneDAO telefoneDAO;
        private IComputadorDAO computadorDAO;

        public ClienteDAOImpl() : base("Cliente", "IdCliente", FactoryConnection.BuildShelf(Connection.Enums.Modo.XML))
        {            
            contratoDAO = DAOFactory.InitContratoDAO();
            telefoneDAO = DAOFactory.InitTelefoneDAO();
            computadorDAO = DAOFactory.InitComputadorDAO();
        }

        public override Cliente parseToDTO(DataRow row)
        {
            Cliente cliente = new Cliente();

            cliente.IDCliente = row.GetValue("IDCliente", default(long));
            cliente.Codigo = row.GetValue("Codigo", default(long));
            cliente.RazaoSocial = row.GetValue("RazaoSocial", string.Empty);
            cliente.NomeFantasia = row.GetValue("NomeFantasia", string.Empty);
            cliente.CpfCnpj = row.GetValue("CPFCNPJ", string.Empty);
            cliente.InscricaoEstadual = row.GetValue("InscricaoEstadual", string.Empty);
            cliente.Email = row.GetValue("Email", string.Empty);
            cliente.Cep = row.GetValue("Cep", string.Empty);
            cliente.Endereco = row.GetValue("Endereco", string.Empty);
            cliente.Bairro = row.GetValue("Bairro", string.Empty);
            cliente.Numero = row.GetValue("Numero", string.Empty);
            cliente.Cidade = row.GetValue("Cidade", string.Empty);
            cliente.Estado = row.GetValue("Estado", string.Empty);

            contratoDAO.List(cliente).ForEach(c => cliente.Contratos.Add(c));
            telefoneDAO.List(cliente).ForEach(c => cliente.Telefones.Add(c));
            computadorDAO.List(cliente).ForEach(c => cliente.Computadores.Add(c));

            return cliente;
        }

        public override Dictionary<string, object> ParseToParamenters(Cliente t)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("IDCliente", t.IDCliente);
            dic.Add("Codigo", t.Codigo);
            dic.Add("RazaoSocial", t.RazaoSocial);
            dic.Add("NomeFantasia", t.NomeFantasia);
            dic.Add("CPFCNPJ", t.CpfCnpj);
            dic.Add("Rede_ID", t.Rede.IDRede);
            dic.Add("InscricaoEstadual", t.InscricaoEstadual);
            dic.Add("Email", t.Email);
            dic.Add("Cep", t.Cep);
            dic.Add("Endereco", t.Endereco);
            dic.Add("Bairro", t.Bairro);
            dic.Add("Numero", t.Numero);
            dic.Add("Cidade", t.Cidade);
            dic.Add("Estado", t.Estado);

            return dic;
        }

        public override Cliente Save(Cliente t)
        {
            Cliente cliente = SaveSimple(t, (c, id) => c.IDCliente = id, $"INSERT INTO {this.GetTableName()} (Rede_ID,Codigo,RazaoSocial,NomeFantasia,CPFCNPJ,InscricaoEstadual,Email,CEP,Endereco,Bairro,Numero,Cidade,Estado) "
             + $"output INSERTED.IDCliente VALUES (@Rede_ID,@Codigo,@RazaoSocial,@NomeFantasia,@CPFCNPJ,@InscricaoEstadual,@Email,@CEP,@Endereco,@Bairro,@Numero,@Cidade,@Estado)",
             this.ParseToParamenters(t));

            this.SaveOrUpdate(cliente.Telefones);

            return cliente;
        }

        private void SaveOrUpdate(ObservableCollection<Telefone> t)
        {
            ITelefoneDAO tdao = DAOFactory.InitTelefoneDAO();
            tdao.SaveOrUpdate(t.ToList());
        }

        public override Cliente Update(Cliente t)
        {
            Cliente cliente = ExecuteNonQuery($"UPDATE {this.GetTableName()} SET "
            + "Rede_ID = @Rede_ID, Codigo = @Codigo, RazaoSocial = @RazaoSocial, NomeFantasia = @NomeFantasia, CPFCNPJ = @CPFCNPJ, InscricaoEstadual = @InscricaoEstadual, Email = @Email, CEP = @CEP, Endereco = @Endereco, Bairro = @Bairro, Numero = @Numero, Cidade = @Cidade, Estado = @Estado WHERE IDCliente = @IDCliente",
            this.ParseToParamenters(t)) > 0 ? t : default(Cliente);
            this.SaveOrUpdate(cliente.Telefones);
            return cliente;
        }

        public List<Cliente> GetListInner()
        {
            using (DataTable dataTable = this.GetDataTable("select * from Cliente INNER JOIN Rede ON Rede.IDRede = Cliente.Rede_ID"))
            {
                List<Cliente> list = new List<Cliente>();
                foreach (DataRow row in dataTable.Rows)
                {
                    list.Add(this.parseToDTO(row));
                }
                return list;
            }
        }

        public List<Cliente> GetClienteByRede(Rede rede)
        {
            using (DataTable dataTable = this.GetDataTable($"select * from Cliente WHERE Rede_ID = {rede.IDRede}"))
            {
                List<Cliente> list = new List<Cliente>();
                foreach (DataRow row in dataTable.Rows)
                {
                    list.Add(this.parseToDTO(row));
                }
                return list;
            }
        }
    }
}
