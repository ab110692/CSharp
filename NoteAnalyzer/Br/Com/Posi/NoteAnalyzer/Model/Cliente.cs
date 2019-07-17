using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Com.Posi.NoteAnalyzer.Model
{
    public class Cliente : IModel
    {
        public String NumeroDoCliente { get; set; }
        public String NomeDoCliente { get; set; }
        public String RazaoSocial { get; set; }
        public String CPFCNPJ { get; set; }
        public String InscEstRG { get; set; }
        public String Telefone { get; set; }
        public String Fax { get; set; }
        public String Endereco { get; set; }
        public String Numero { get; set; }
        public String Complemento { get; set; }
        public String Bairro { get; set; }
        public String Cidade { get; set; }
        public String Estado { get; set; }
        public String CEP { get; set; }
        public String Email { get; set; }
        public bool Contrato { get; set; }

        public int GetID()
        {
            return Convert.ToInt32(NumeroDoCliente);
        }
    }
}
