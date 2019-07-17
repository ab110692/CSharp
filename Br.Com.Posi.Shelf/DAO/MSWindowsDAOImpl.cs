using Br.Com.Posi.Shelf.Model;
using System.Collections.Generic;
using System.Data;
using Br.Com.Posi.Connection;
using Br.Com.Posi.Connection.Util;

namespace Br.Com.Posi.Shelf.DAO
{
    internal class MSWindowsDAOImpl : DAOImpl<MSWindows>, IMSWindowsDAO
    {
        public MSWindowsDAOImpl() : base("MSWindows", "IDWindows", FactoryConnection.BuildShelf(Connection.Enums.Modo.XML)) { }

        public override MSWindows parseToDTO(DataRow row)
        {
            MSWindows msWindows = new MSWindows();
            msWindows.IDWindows = row.GetValue("IDWindows", default(long));
            msWindows.Nome = row.GetValue("Nome", string.Empty);
            return msWindows;
        }

        public override Dictionary<string, object> ParseToParamenters(MSWindows t)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("IDWindows", t.IDWindows);
            dic.Add("Nome", t.Nome);
            return dic;
        }

        public override MSWindows Save(MSWindows t)
        {
            return SaveSimple(t, (m, id) => m.IDWindows = id, $"INSERT INTO {this.GetTableName()} (Nome) output INSERTED.IDWindows VALUES (@Nome)", this.ParseToParamenters(t));
        }

        public override MSWindows Update(MSWindows t)
        {
            return ExecuteNonQuery($"UPDATE {this.GetTableName()} SET Nome = @Nome WHERE {this.GetPKColumnName()} = @IDWindows", this.ParseToParamenters(t)) > 0 ? t : default(MSWindows);
        }
    }
}
