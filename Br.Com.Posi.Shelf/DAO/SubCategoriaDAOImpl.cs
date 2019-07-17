using Br.Com.Posi.Shelf.Model;
using System.Collections.Generic;
using System.Data;
using System.Collections.ObjectModel;
using Br.Com.Posi.Connection;
using Br.Com.Posi.Connection.Util;

namespace Br.Com.Posi.Shelf.DAO
{
    internal sealed class SubCategoriaDAOImpl : DAOImpl<SubCategoria>, ISubCategoriaDAO
    {

        private IItemDAO itemDAO;

        public SubCategoriaDAOImpl() : base("SubCategoria", "IDSubCategoria", FactoryConnection.BuildShelf(Connection.Enums.Modo.XML))
        {
            itemDAO = new ItemDAOImpl();
        }

        public override SubCategoria parseToDTO(DataRow row)
        {
            SubCategoria subCategoria = new SubCategoria();
            subCategoria.IDSubCategoria = row.GetValue("IDSubCategoria", default(long));
            subCategoria.Nome = row.GetValue("Nome", string.Empty);
            itemDAO.GetListWhere("SubCategoria_ID", subCategoria.IDSubCategoria).ForEach(i => subCategoria.Items.Add(i));
            return subCategoria;
        }

        public override Dictionary<string, object> ParseToParamenters(SubCategoria t)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("IDSubCategoria", t.IDSubCategoria);
            dic.Add("Nome", t.Nome);
            dic.Add("Categoria_ID", t.Categoria.IDCategoria);
            return dic;
        }

        public override SubCategoria Save(SubCategoria t)
        {
            SubCategoria subCategoria = SaveSimple(t, (r, id) => r.IDSubCategoria = id, $"INSERT INTO {this.GetTableName()} (Nome,Categoria_ID) output INSERTED.IDSubCategoria VALUES (@Nome,@Categoria_ID)", this.ParseToParamenters(t));
            this.SaveUpdateItem(subCategoria.Items);
            return subCategoria;
        }

        public override SubCategoria Update(SubCategoria t)
        {
            SubCategoria subCategoria = UpdateSimple(t, $"UPDATE {this.GetTableName()} SET Nome = @Nome, Categoria_ID = @Categoria_ID WHERE {this.GetPKColumnName()} = @IDSubCategoria", this.ParseToParamenters(t));
            this.SaveUpdateItem(subCategoria.Items);
            return subCategoria;
        }

        private void SaveUpdateItem(ObservableCollection<Item> list)
        {
            foreach (Item t in list)
            {
                itemDAO.SaveOrUpdate(t);
            }
        }

        public List<SubCategoria> GetSubCategoriaByCategoriaWithItem(Categoria categoria)
        {
            using (DataTable dataTable = this.GetDataTable($"select s.* from SubCategoria s WHERE s.IDSubCategoria in (SELECT SubCategoria_ID FROM Item) AND s.Categoria_ID = {categoria.IDCategoria}"))
            {
                List<SubCategoria> list = new List<SubCategoria>();
                foreach (DataRow row in dataTable.Rows)
                {
                    list.Add(this.parseToDTO(row));
                }
                return list;
            }
        }
    }
}
