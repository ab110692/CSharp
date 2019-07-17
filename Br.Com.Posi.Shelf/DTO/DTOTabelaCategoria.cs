using Br.Com.Posi.Shelf.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Com.Posi.Shelf.DTO
{
    public class DTOTabelaCategoria
    {
        public Categoria Categoria { get; set; }
        public SubCategoria SubCategoria { get; set; }
        public Item Item { get; set; }
        public Aplicativo Aplicativo { get; set; }
        public Versao Versao { get; set; }
    }
}
