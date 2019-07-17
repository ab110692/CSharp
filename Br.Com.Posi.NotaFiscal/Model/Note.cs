using Br.Com.Posi.Connection.Model;

namespace Br.Com.Posi.NotaFiscal.Model
{
    public class Note : IModelo
    {
        public string NumeroLoja { get; set; }

        public string NomeLoja { get; set; }

        public string NotaInicial { get; set; }

        public string NotaFinal { get; set; }

        public string UltimaTransferencia { get; set; }

        public string QuantidadeNota { get; set; }

        public string Faltantes { get; set; }

        public string Rede { get; set; }

        public string Estado { get; set; }

        public string Ano { get; set; }

        public string Mes { get; set; }

        public string Fiscal { get; set; }

        public string DataCriado { get; set; }

        public long ID => long.TryParse(this.NumeroLoja, out long t) ? t : default(long);

    }
}
