using Br.Com.Posi.Connection.Model;

namespace Br.Com.Posi.Shelf.Model
{
    public class Contrato : IModelo
    {
        public long IDContrato { get; set; }

        public string Nome { get; set; }

        public bool Ativo { get; set; }

        public Cliente Cliente { get; set; }

        public Contrato()
        {
            this.Cliente = new Cliente();
        }

        ~Contrato()
        {
            this.Cliente = null;
        }

        long IModelo.ID
        {
            get
            {
                return IDContrato;
            }
        }
    }
}
