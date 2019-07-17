using Br.Com.Posi.Connection.Model;

namespace Br.Com.Posi.Shelf.Model
{
    public class MSWindows : IModelo
    {

        public long IDWindows { get; set; }

        public string Nome { get; set; }

        long IModelo.ID
        {
            get
            {
                return IDWindows;
            }
        }
        public override string ToString()
        {
            return this.Nome;
        }
    }
}
