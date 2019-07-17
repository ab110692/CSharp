using System;
using System.Text;

namespace Br.Com.Posi.Util.Formatted
{
    sealed class FormattedCPFImpl : FormattedImpl
    {
        int CPF_LENGHT = 15;
        int CNPJ_LENGHT = 18;

        public override string Formatted(string text)
        {
            if (text.Length > CNPJ_LENGHT)
            {
                return text.Substring(0, CNPJ_LENGHT);
            }

            StringBuilder sb = new StringBuilder();

            sb.Append(this.OnlyNumber());

            if (text.Length <= CPF_LENGHT)
            {
                if (text.Length > 3 && sb.Length >= 3) sb.Insert(3, ".");
                if (text.Length > 7 && sb.Length >= 7) sb.Insert(7, ".");
                if (text.Length > 11 && sb.Length >= 11) sb.Insert(11, "-");
            }
            else
            {
                if (text.Length > 2 && sb.Length >= 2) sb.Insert(2, ".");
                if (text.Length > 6 && sb.Length >= 6) sb.Insert(6, ".");
                if (text.Length > 10 && sb.Length >= 10) sb.Insert(10, "/");
                if (text.Length > 15 && sb.Length >= 15) sb.Insert(15, "-");
            }

            return sb.ToString();
        }

        public override int GetLenght()
        {
            return this.Text.Length <= CPF_LENGHT ? CPF_LENGHT : CNPJ_LENGHT;
        }

        public override bool IsValid()
        {
            if (GetLenght() == CNPJ_LENGHT)
            {
                return ValidCNPJ();
            }
            else
            {
                return ValidCPF();
            }
        }

        private bool ValidCPF()
        {
            string cpf = this.Text;
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
                return false;

            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();

            tempCpf = tempCpf + digito;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }

        private bool ValidCNPJ()
        {
            string cnpj = this.Text;
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;

            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            if (cnpj.Length != 14)
                return false;

            tempCnpj = cnpj.Substring(0, 12);

            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();

            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cnpj.EndsWith(digito);
        }
    }
}
