using Br.Com.Posi.Connection;
using Br.Com.Posi.ControlarEstoque.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Com.Posi.ControlarEstoque.Dao
{
    public class CategoriaDAOImpl : DAOHibernateImpl<Categoria>
    {
        public CategoriaDAOImpl() : base("tblCategoria")
        {
        }
    }
}
