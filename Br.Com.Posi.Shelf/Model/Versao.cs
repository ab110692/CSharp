using System;
using Br.Com.Posi.Connection.Model;

namespace Br.Com.Posi.Shelf.Model
{
    public class Versao : IModelo
    {

        public long IDVersao { get; set; }

        public Aplicativo Aplicativo { get; set; }

        public string VersaoSistema { get; set; }

        public long ID
        {
            get
            {
                return IDVersao;
            }
        }

        public Versao()
        {
            Aplicativo = new Aplicativo();
        }

        ~Versao()
        {
            Aplicativo = null;
        }

        public override string ToString()
        {
            return this.VersaoSistema;
        }

        internal void ForEach(Func<object, object> p)
        {
            throw new NotImplementedException();
        }
    }
}