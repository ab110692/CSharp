using Br.Com.Posi.Connection.Model;

namespace Br.Com.Posi.Shelf.Model
{
    public class Item : IModelo
    {

        public long IDItem { get; set; }

        public string Nome { get; set; }

        public SubCategoria SubCategoria { get; set; }

        public long ID
        {
            get
            {
                return IDItem;
            }
        }

        public override string ToString()
        {
            return this.Nome;
        }

        public Item()
        {
        }

        ~Item()
        {
        }
    }
}