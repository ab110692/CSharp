using Br.Com.Posi.Shelf.Model;
using System.Collections.Generic;
using System.Data;
using Br.Com.Posi.Connection;
using Br.Com.Posi.Connection.Util;
using System.Linq;

namespace Br.Com.Posi.Shelf.DAO
{
    internal sealed class AplicativoDAOImpl : DAOImpl<Aplicativo>, IAplicativoDAO
    {
        private IVersaoDAO daoVersao;

        public AplicativoDAOImpl() : base("Aplicativo", "IDAplicativo", FactoryConnection.BuildShelf(Connection.Enums.Modo.XML))
        {
            daoVersao = new VersaoDAOImpl();
        }

        public override Aplicativo parseToDTO(DataRow row)
        {
            Aplicativo aplicativo = new Aplicativo();
            aplicativo.IDAplicativo = row.GetValue("IDAplicativo", default(long));
            aplicativo.Descricao = row.GetValue("Descricao", string.Empty);
            daoVersao.GetListWhere("Aplicativo_ID", aplicativo.IDAplicativo).ForEach(a => aplicativo.Versoes.Add(a));
            return aplicativo;
        }

        public override Dictionary<string, object> ParseToParamenters(Aplicativo t)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("IDAplicativo", t.IDAplicativo);
            dic.Add("Descricao", t.Descricao);
            return dic;
        }

        public override Aplicativo Save(Aplicativo t)
        {
            t = SaveSimple(t, (r, id) => r.IDAplicativo = id, $"INSERT INTO {this.GetTableName()} (Descricao) output INSERTED.IDAplicativo VALUES (@Descricao)", this.ParseToParamenters(t));
            if (t.Versoes != null)
            {
                t.Versoes.Select(s => { return daoVersao.SaveOrUpdate(s); });
            }
            return t;
        }

        public override Aplicativo Update(Aplicativo t)
        {
            t = UpdateSimple(t, $"UPDATE {this.GetTableName()} SET Descricao = @Descricao WHERE {this.GetPKColumnName()} = @IDAplicativo", this.ParseToParamenters(t));
            if (t.Versoes != null)
            {
                t.Versoes.Select(s => { return daoVersao.SaveOrUpdate(s); });
            }
            return t;
        }
    }
}
