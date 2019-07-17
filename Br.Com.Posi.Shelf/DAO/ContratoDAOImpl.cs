using Br.Com.Posi.Shelf.Model;
using System.Collections.Generic;
using System.Data;
using Br.Com.Posi.Connection;
using Br.Com.Posi.Connection.Util;

namespace Br.Com.Posi.Shelf.DAO
{
    internal class ContratoDAOImpl : DAOImpl<Contrato>, IContratoDAO
    {

        public ContratoDAOImpl() : base("Contrato", "IDContrato", FactoryConnection.BuildShelf(Connection.Enums.Modo.XML)) { }


        public override Contrato parseToDTO(DataRow row)
        {
            Contrato contrato = new Contrato();
            contrato.IDContrato = row.GetValue("IDContrato", default(long));
            contrato.Nome = row.GetValue("Nome", string.Empty);
            contrato.Ativo = row.GetValue("Ativo", default(bool));
            return contrato;
        }

        public override Dictionary<string, object> ParseToParamenters(Contrato t)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("IDContrato", t.IDContrato);
            dic.Add("Nome", t.Nome);
            dic.Add("Ativo", t.Ativo);
            dic.Add("Cliente_ID", t.Cliente.IDCliente);
            return dic;
        }

        public override Contrato Save(Contrato t)
        {
            return SaveSimple(t, (c, id) => c.IDContrato = id, $"INSERT INTO {this.GetTableName()} (Nome,Ativo,Cliente_ID) "
                + "output INSERTED.IDContrato VALUES (@Nome,@Ativo,@Cliente_ID)", this.ParseToParamenters(t));
        }


        public override Contrato Update(Contrato t)
        {
            return this.ExecuteNonQuery($"UPDATE {this.GetTableName()} SET Nome = @Nome, Ativo = @Ativo,"
                + $"Cliente_ID = @Cliente_ID WHERE {this.GetPKColumnName()} = @IDContrato", this.ParseToParamenters(t)) > 0 ? t : default(Contrato);
        }

        public List<Contrato> List(Cliente cliente)
        {
            using (DataTable dataTable = this.GetDataTable($"select * from {this.GetTableName()} WHERE Cliente_ID = {cliente.IDCliente}"))
            {
                List<Contrato> list = new List<Contrato>();
                foreach (DataRow row in dataTable.Rows)
                {
                    list.Add(this.parseToDTO(row));
                }
                return list;
            }
        }
    }
}
