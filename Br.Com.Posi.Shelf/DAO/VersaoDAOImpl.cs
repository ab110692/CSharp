using Br.Com.Posi.Shelf.Model;
using System.Collections.Generic;
using System.Data;
using Br.Com.Posi.Connection;
using Br.Com.Posi.Connection.Util;

namespace Br.Com.Posi.Shelf.DAO
{
    internal sealed class VersaoDAOImpl : DAOImpl<Versao>, IVersaoDAO
    {
        public VersaoDAOImpl() : base("Versao", "IDVersao", FactoryConnection.BuildShelf(Connection.Enums.Modo.XML)) { }

        public override Versao parseToDTO(DataRow row)
        {
            Versao versao = new Versao();
            versao.IDVersao = row.GetValue("IDVersao", default(long));
            versao.VersaoSistema = row.GetValue("Versao", string.Empty);
            return versao;
        }

        public override Dictionary<string, object> ParseToParamenters(Versao t)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("IDVersao", t.IDVersao);
            dic.Add("Aplicativo_ID", t.Aplicativo.IDAplicativo);
            dic.Add("Versao", t.VersaoSistema);
            return dic;
        }

        public override Versao Save(Versao t)
        {
            return SaveSimple(t, (r, id) => r.IDVersao = id, $"INSERT INTO {this.GetTableName()} (Aplicativo_ID,Versao) output INSERTED.IDVersao VALUES (@Aplicativo_ID,@Versao)", this.ParseToParamenters(t));
        }

        public override Versao Update(Versao t)
        {
            return UpdateSimple(t, $"UPDATE {this.GetTableName()} SET Aplicativo_ID = @Aplicativo_ID, Versao = @Versao WHERE {this.GetPKColumnName()} = @IDVersao", this.ParseToParamenters(t));
        }
    }
}
