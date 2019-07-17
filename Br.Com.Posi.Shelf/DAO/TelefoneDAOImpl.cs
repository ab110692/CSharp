using Br.Com.Posi.Shelf.Model;
using System;
using System.Collections.Generic;
using System.Data;
using Br.Com.Posi.Connection.Util;
using Br.Com.Posi.Connection;

namespace Br.Com.Posi.Shelf.DAO
{

    internal class TelefoneDAOImpl : DAOImpl<Telefone>, ITelefoneDAO
    {

        public TelefoneDAOImpl() : base("Telefone", "IDTelefone", FactoryConnection.BuildShelf(Connection.Enums.Modo.XML)) { }
        
        public override Telefone parseToDTO(DataRow row)
        {
            Telefone telefone = new Telefone();
            telefone.IDTelefone = row.GetValue("IDTelefone", default(long));
            telefone.Numero = row.GetValue("Numero", string.Empty);
            telefone.Nome = row.GetValue("Nome", string.Empty);
            telefone.Cargo = row.GetValue("Cargo", string.Empty);
            return telefone;
        }

        public override Dictionary<string, object> ParseToParamenters(Telefone t)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("IDTelefone", t.IDTelefone);
            dic.Add("Numero", t.Numero);
            dic.Add("Nome", t.Nome);
            dic.Add("Cargo", t.Cargo);
            dic.Add("Cliente_ID", t.Cliente.IDCliente);
            return dic;
        }

        public override Telefone Save(Telefone t)
        {
            return this.SaveSimple(t, (i, id) => i.IDTelefone = id, $"INSERT INTO {this.GetTableName()} (Nome,Numero,Cargo,Cliente_ID) output INSERTED.IDTelefone VALUES (@Nome,@Numero,@Cargo,@Cliente_ID)", this.ParseToParamenters(t));
        }

        public override Telefone Update(Telefone t)
        {
            if (t.IDTelefone <= 0)
            {
                throw new ArgumentNullException("O campo IDTelefone não pode ser menor ou igual a 0");
            }
            return this.UpdateSimple(t, $"UPDATE {this.GetTableName()} SET  Nome = @Nome, Numero = @Numero, Cargo = @Cargo, Cliente_ID = @Cliente_ID WHERE {this.GetPKColumnName()} = @IDTelefone", ParseToParamenters(t));
        }

        public List<Telefone> List(Cliente cliente)
        {
            using (DataTable dataTable = this.GetDataTable($"select * from {this.GetTableName()} WHERE Cliente_ID = {cliente.IDCliente}"))
            {
                List<Telefone> list = new List<Telefone>();
                foreach (DataRow row in dataTable.Rows)
                {
                    list.Add(this.parseToDTO(row));
                }
                return list;
            }
        }

    }
}
