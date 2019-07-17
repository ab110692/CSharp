using Br.Com.Posi.Shelf.Model;
using System.Collections.Generic;
using System.Data;
using Br.Com.Posi.Connection.Util;
using Br.Com.Posi.Connection;
using System.Linq;

namespace Br.Com.Posi.Shelf.DAO
{
    internal class RedeDAOImpl : DAOImpl<Rede>, IRedeDAO
    {

        private IClienteDAO daoCliente;

        public RedeDAOImpl() : base("Rede", "IDRede", FactoryConnection.BuildShelf(Connection.Enums.Modo.XML))
        {
            daoCliente = DAOFactory.InitClienteDAO();
        }

        public override Rede parseToDTO(DataRow row)
        {
            Rede rede = new Rede();
            rede.IDRede = row.GetValue("IDRede", default(long));
            rede.Codigo = row.GetValue("Codigo", default(long));
            rede.Nome = row.GetValue("Nome", string.Empty);
            daoCliente.GetClienteByRede(rede).ForEach(r => rede.Clientes.Add(r));
            return rede;
        }

        public override Dictionary<string, object> ParseToParamenters(Rede t)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("IDRede", t.IDRede);
            dic.Add("Codigo", t.Codigo);
            dic.Add("Nome", t.Nome);
            return dic;
        }

        public Rede GetRede(Cliente cliente)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>
            {
                { "IDCliente", cliente.IDCliente }
            };
            using (DataTable dataTable = this.GetDataTable("SELECT * FROM Rede WHERE IDRede = (SELECT Rede_ID FROM Cliente WHERE IDCliente = @IDCliente)", dic))
            {
                return this.parseToDTO(dataTable?.Rows[0]) ?? default(Rede);
            }
        }

        public override Rede Save(Rede t)
        {
            return SaveSimple(t, (r, id) => r.IDRede = id, $"INSERT INTO {this.GetTableName()} (Codigo,Nome) output INSERTED.IDRede VALUES (@Codigo,@Nome)", this.ParseToParamenters(t));
        }

        public override Rede Update(Rede t)
        {
            return ExecuteNonQuery($"UPDATE {this.GetTableName()} SET Codigo = @Codigo, Nome = @Nome WHERE {this.GetPKColumnName()} = @IDRede", this.ParseToParamenters(t)) > 0 ? t : default(Rede);
        }
    }
}
