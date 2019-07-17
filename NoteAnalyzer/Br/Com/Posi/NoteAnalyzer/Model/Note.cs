using Br.Com.Posi.NoteAnalyzer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteAnalyzer.Br.Com.Posi.NoteAnalyzer.Model
{
    public class Note : IModel
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

        public int GetID()
        {
            throw new NotImplementedException();
        }
    }
}
