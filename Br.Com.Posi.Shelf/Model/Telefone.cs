using Br.Com.Posi.Connection.Model;

namespace Br.Com.Posi.Shelf.Model
{
    public class Telefone : IModelo
    {
        public long IDTelefone { get; set; }

        public string Numero { get; set; }

        public string Nome { get; set; }

        public string Cargo { get; set; }

        public Cliente Cliente { get; set; }

        public Telefone()
        {
            this.Cliente = new Cliente();
        }

        ~Telefone()
        {
            this.Cliente = null;
        }

        long IModelo.ID
        {
            get
            {
                return IDTelefone;
            }
        }
    }
}
