using Br.Com.Posi.Connection.Model;

namespace Br.Com.Posi.Shelf.Model
{
    public class AntiVirus : IModelo
    {
        public long IDAntiVirus { get; set; }

        public string Nome { get; set; }

        long IModelo.ID
        {
            get
            {
                return IDAntiVirus;
            }
        }

        public override string ToString()
        {
            return this.Nome;
        }
    }
}
