using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Com.Posi.NoteAnalyzer.DataGrid.Model
{
    public class NotePerState
    {
        public String numeroLoja { get; set; }

        public String nomeLoja { get; set; }

        public String notaInicial { get; set; }

        public String notaFinal { get; set; }

        public String dataCriado { get; set; }

        public int quantidadeTotal { get; set; }

        public int faltantes { get; set; }

        public List<int> numerosFaltantes { get; set; }

        public List<int> numerosInutilizado { get; set; }

        public List<int> numerosCancelado { get; set; }
    }
}
