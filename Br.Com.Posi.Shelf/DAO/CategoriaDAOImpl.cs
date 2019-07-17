using Br.Com.Posi.Shelf.Model;
using System.Collections.Generic;
using System.Data;
using System.Collections.ObjectModel;
using Br.Com.Posi.Connection;
using Br.Com.Posi.Connection.Util;

namespace Br.Com.Posi.Shelf.DAO
{
    internal sealed class CategoriaDAOImpl : DAOImpl<Categoria>, ICategoriaDAO
    {

        private ISubCategoriaDAO subCategoriaDAO;

        public CategoriaDAOImpl() : base("Categoria", "IDCategoria", FactoryConnection.BuildShelf(Connection.Enums.Modo.XML))
        {
            subCategoriaDAO = new SubCategoriaDAOImpl();
        }

        public override Categoria parseToDTO(DataRow row)
        {
            Categoria categoria = new Categoria();
            categoria.IDCategoria = row.GetValue("IDCategoria", default(long));
            categoria.Nome = row.GetValue("Nome", string.Empty);
            subCategoriaDAO.GetListWhere("Categoria_ID", categoria.IDCategoria).ForEach(s => categoria.SubCategorias.Add(s));
            return categoria;
        }

        private Categoria parseToDTOWithItem(DataRow row)
        {
            Categoria categoria = new Categoria();
            categoria.IDCategoria = row.GetValue("IDCategoria", default(long));
            categoria.Nome = row.GetValue("Nome", string.Empty);
            subCategoriaDAO.GetListWhere("Categoria_ID", categoria.IDCategoria).ForEach(s => categoria.SubCategorias.Add(s));
            return categoria;
        }

        public override Dictionary<string, object> ParseToParamenters(Categoria t)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("IDCategoria", t.IDCategoria);
            dic.Add("Nome", t.Nome);
            return dic;
        }

        public override Categoria Save(Categoria t)
        {
            Categoria categoria = SaveSimple(t, (r, id) => r.IDCategoria = id, $"INSERT INTO {this.GetTableName()} (Nome) output INSERTED.IDCategoria VALUES (@Nome)", this.ParseToParamenters(t));
            this.SaveUpdateSubCategoria(categoria.SubCategorias);
            return categoria;
        }

        public override Categoria Update(Categoria t)
        {
            Categoria categoria = UpdateSimple(t, $"UPDATE {this.GetTableName()} SET Nome = @Nome WHERE {this.GetPKColumnName()} = @IDCategoria", this.ParseToParamenters(t));
            this.SaveUpdateSubCategoria(categoria.SubCategorias);
            return categoria;
        }

        private void SaveUpdateSubCategoria(ObservableCollection<SubCategoria> list)
        {
            foreach (SubCategoria t in list)
            {
                subCategoriaDAO.SaveOrUpdate(t);
            }
        }

        public List<Categoria> GetCategoriaWithSubCategoriaAndItem()
        {
            using (DataTable dataTable = this.GetDataTable($"select c.* from Categoria c INNER JOIN SubCategoria s ON c.IDCategoria = s.Categoria_ID WHERE s.IDSubCategoria IN (SELECT SubCategoria_ID FROM Item)"))
            {
                List<Categoria> list = new List<Categoria>();
                foreach (DataRow row in dataTable.Rows)
                {
                    list.Add(this.parseToDTOWithItem(row));
                }
                return list;
            }
        }

    }
}
