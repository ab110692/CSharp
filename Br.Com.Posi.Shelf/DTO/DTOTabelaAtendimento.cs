using Br.Com.Posi.Shelf.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Com.Posi.Shelf.DTO
{
    public class DTOTabelaAtendimento
    {

        public long Protocolo { get; set; }

        public DateTime Data { get; set; }

        public string Atendente { get; set; }

        public string NumeroCliente { get; set; }

        public string Cliente { get; set; }

        public string Contato { get; set; }

        public string Problema { get; set; }
    }
}
