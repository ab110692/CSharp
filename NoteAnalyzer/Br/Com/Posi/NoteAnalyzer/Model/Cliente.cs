using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Com.Posi.NoteAnalyzer.Model
{
    public class Cliente : IModel, IEqualityComparer<Cliente>, IComparable<Cliente>, IComparer<Cliente>
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

        public int Compare(Cliente x, Cliente y)
        {
            if (x == null || y == null)
            {
                return 0;
            }
            return x.GetID() == y.GetID() ? 1:0;
        }

        public int CompareTo(Cliente other)
        {
            if (other == null)
            {
                return 0;
            }
            return this.GetID() == other.GetID() ? 1 : 0;
        }

        public bool Equals(Cliente x, Cliente y)
        {
            if (x == null || y == null)
            {
                return false;
            }
            return x.GetID() == y.GetID();
        }

        public int GetHashCode(Cliente obj)
        {
       return obj.GetHashCode();       
        }

        public int GetID()
        {
            return Convert.ToInt32(NumeroDoCliente);
        }
    }
}
