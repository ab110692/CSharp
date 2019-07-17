using Br.Com.Posi.Shelf.Model;
using System.Collections.Generic;
using System.Data;
using Br.Com.Posi.Connection.Util;
using Br.Com.Posi.Connection;

namespace Br.Com.Posi.Shelf.DAO
{
    internal class AntiVirusDAOImpl : DAOImpl<AntiVirus>, IAntiVirusDAO
    {
        public AntiVirusDAOImpl() : base("AntiVirus", "IDAntiVirus", FactoryConnection.BuildShelf(Connection.Enums.Modo.XML)) { }
        

        public override AntiVirus parseToDTO(DataRow row)
        {
            AntiVirus antiVirus = new AntiVirus();
            antiVirus.IDAntiVirus = row.GetValue("IDAntiVirus", default(long));
            antiVirus.Nome = row.GetValue("Nome", string.Empty);
            return antiVirus;
        }

        public override Dictionary<string, object> ParseToParamenters(AntiVirus t)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("IDAntiVirus", t.IDAntiVirus);
            dic.Add("Nome", t.Nome);
            return dic;
        }

        public override AntiVirus Save(AntiVirus t)
        {
            return SaveSimple(t, (a, id) => a.IDAntiVirus = id, $"INSERT INTO {this.GetTableName()} (Nome) output INSERTED.IDAntiVirus VALUES (@Nome)", this.ParseToParamenters(t));
        }

        public override AntiVirus Update(AntiVirus t)
        {
            return ExecuteNonQuery($"UPDATE {this.GetTableName()} SET Nome = @Nome WHERE {this.GetPKColumnName()} = @IDAntiVirus", this.ParseToParamenters(t)) > 0 ? t : default(AntiVirus);
        }
    }
}
