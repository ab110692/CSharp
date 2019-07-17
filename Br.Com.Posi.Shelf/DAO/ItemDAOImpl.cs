using Br.Com.Posi.Shelf.Model;
using System.Collections.Generic;
using System.Data;
using Br.Com.Posi.Connection;
using Br.Com.Posi.Connection.Util;

namespace Br.Com.Posi.Shelf.DAO
{
    internal sealed class ItemDAOImpl : DAOImpl<Item>, IItemDAO
    {
        public ItemDAOImpl() : base("Item", "IDItem", FactoryConnection.BuildShelf(Connection.Enums.Modo.XML)) { }

        public override Item parseToDTO(DataRow row)
        {
            Item item = new Item();
            item.IDItem = row.GetValue("IDItem", default(long));
            item.Nome = row.GetValue("Nome", string.Empty);
            return item;
        }

        public override Dictionary<string, object> ParseToParamenters(Item t)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("IDItem", t.IDItem);
            dic.Add("Nome", t.Nome);
            dic.Add("SubCategoria_ID", t.SubCategoria.IDSubCategoria);
            return dic;
        }

        public override Item Save(Item t)
        {
            return SaveSimple(t, (r, id) => r.IDItem = id, $"INSERT INTO {this.GetTableName()} (Nome,SubCategoria_ID) output INSERTED.IDItem VALUES (@Nome,@SubCategoria_ID)", this.ParseToParamenters(t));
        }

        public override Item Update(Item t)
        {
            return UpdateSimple(t, $"UPDATE {this.GetTableName()} SET Nome = @Nome, SubCategoria_ID = @SubCategoria_ID WHERE {this.GetPKColumnName()} = @IDItem", this.ParseToParamenters(t));
        }
    }
}
