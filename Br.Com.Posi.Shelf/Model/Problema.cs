using Br.Com.Posi.Connection.Model;

namespace Br.Com.Posi.Shelf.Model
{
    public class Problema : IModelo
    {

        public long IDProblema { get; set; }

        public Categoria Categoria { get; set; }

        public SubCategoria SubCategoria { get; set; }

        public Item Item { get; set; }

        public Aplicativo Aplicativo { get; set; }

        public Versao Versao { get; set; }

        public Atendimento Atendimento { get; set; }

        public long ID
        {
            get
            {
                return IDProblema;
            }
        }

        public Problema()
        {
        }

        ~Problema()
        {
        }
    }
}
