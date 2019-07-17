using Br.Com.Posi.Connection.Model;
using System;

namespace Br.Com.Posi.Shelf.Model
{
    public class Computador : IModelo
    {

        public long IDComputador { get; set; }

        public Cliente Cliente { get; set; }

        public string Nome { get; set; }

        public MSWindows MSWindows { get; set; }

        public string LicencaWindows { get; set; }

        public AntiVirus AntiVirus { get; set; }

        public string LicencaAntiVirus { get; set; }

        public string MacAddress { get; set; }

        public DateTime DataAquisicaoAntiVirus { get; set; }

        public DateTime DataTerminoAntiVirus { get; set; }

        public Computador()
        {
            this.Cliente = new Cliente();
            this.MSWindows = new MSWindows();
            this.AntiVirus = new AntiVirus();
        }

        ~Computador()
        {
            this.Cliente = null;
            this.MSWindows = null;
            this.AntiVirus = null;
        }

        long IModelo.ID
        {
            get
            {
                return IDComputador;
            }
        }
    }
}
