using Br.Com.Posi.Connection.Model;
using NHibernate.Mapping.Attributes;

namespace Br.Com.Posi.ControlarEstoque.Model
{
    [Class(Table = "tblCategoria")]
    public class Categoria
    {
        [Id(Name = "Id", Column = "Id"), Generator(1, Class = "identity")]
        public long ID { get; set; }

        [Property(Column = "Nome")]
        public string Nome { get; set; }
    }
}
