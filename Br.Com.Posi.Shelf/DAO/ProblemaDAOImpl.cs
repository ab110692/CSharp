using Br.Com.Posi.Shelf.Model;
using System.Collections.Generic;
using System.Data;
using Br.Com.Posi.Connection;
using Br.Com.Posi.Connection.Util;

namespace Br.Com.Posi.Shelf.DAO
{
    internal sealed class ProblemaDAOImpl : DAOImpl<Problema>, IProblemaDAO
    {

        private IAplicativoDAO daoAplicativo;
        private IVersaoDAO daoVersao;
        private ICategoriaDAO daoCategoria;
        private ISubCategoriaDAO daoSubCategoria;
        private IItemDAO daoItem;

        public ProblemaDAOImpl() : base("Problema", "IDProblema", FactoryConnection.BuildShelf(Connection.Enums.Modo.XML))
        {
            daoAplicativo = DAOFactory.InitAplicativoDAO();
            daoVersao = DAOFactory.InitVersaoDAO();
            daoCategoria = DAOFactory.InitCategoriaDAO();
            daoSubCategoria = DAOFactory.InitSubCategoriaDAO();
            daoItem = DAOFactory.InitItemDAO();
        }

        public override Problema parseToDTO(DataRow row)
        {
            Problema problema = new Problema();
            problema.IDProblema = row.GetValue("IDProblema", default(long));
            problema.Categoria = daoCategoria.GetByPK(row.GetValue("Categoria_ID", default(long)));
            problema.SubCategoria = daoSubCategoria.GetByPK(row.GetValue("SubCategoria_ID", default(long)));
            problema.Item = daoItem.GetByPK(row.GetValue("Item_ID", default(long)));
            problema.Aplicativo = daoAplicativo.GetByPK(row.GetValue("Aplicativo_ID", default(long)));
            problema.Versao = daoVersao.GetByPK(row.GetValue("Versao_ID", default(long)));
            return problema;
        }

        public override Dictionary<string, object> ParseToParamenters(Problema t)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("IDProblema", t.IDProblema);
            dic.Add("Categoria_ID", t.Categoria.IDCategoria);
            dic.Add("SubCategoria_ID", t.SubCategoria.IDSubCategoria);
            dic.Add("Item_ID", t.Item.IDItem);
            dic.Add("Aplicativo_ID", t.Aplicativo.IDAplicativo);
            dic.Add("Versao_ID", t.Versao.IDVersao);
            dic.Add("Atendimento_ID", t.Atendimento.IDAtendimento);
            return dic;
        }

        public override Problema Save(Problema t)
        {
            t = SaveSimple(t, (p, id) => p.IDProblema = id, $"INSERT INTO {this.GetTableName()} (Categoria_ID,SubCategoria_ID,Item_ID,Aplicativo_ID,Versao_ID,Atendimento_ID) output inserted.IDProblema VALUES (@Categoria_ID,@SubCategoria_ID,@Item_ID,@Aplicativo_ID,@Versao_ID,@Atendimento_ID)", ParseToParamenters(t));
            
            return t;
        }

        public override Problema Update(Problema t)
        {
            t = UpdateSimple(t, $"UPDATE {this.GetTableName()} SET Categoria_ID = @Categoria_ID, SubCategoria_ID = @SubCategoria_ID, Item_ID = @Item_ID, Aplicativo_ID = @Aplicativo_ID, Versao_ID = @Versao_ID, Atendimento_ID = @Atendimento_ID WHERE {this.GetPKColumnName()} = @IDProblema", ParseToParamenters(t));
            
            return t;
        }
    }
}

