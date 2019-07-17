using Br.Com.Posi.Connection.Model;
using System;

namespace Br.Com.Posi.NotaFiscal.Model
{
    public class Cliente : IModelo
    {
        public string NumeroDoCliente { get; set; }

        public string NomeDoCliente { get; set; }

        public string RazaoSocial { get; set; }

        public string CPFCNPJ { get; set; }

        public string InscEstRG { get; set; }

        public string Telefone { get; set; }

        public string Fax { get; set; }

        public string Endereco { get; set; }

        public string Numero { get; set; }

        public string Complemento { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }

        public string CEP { get; set; }

        public string Email { get; set; }

        public bool Contrato { get; set; }

        public long ID => long.TryParse(this.NumeroDoCliente, out long t) ? t : default(long);

    }
}
