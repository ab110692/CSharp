using System;
using System.Text;

namespace Br.Com.Posi.Util.Formatted
{
    sealed class FormattedPISImpl : FormattedImpl
    {

        int PIS_LENGHT = 14;

        public override string Formatted(string text)
        {
            if (text.Length > PIS_LENGHT)
            {
                return text.Substring(0, PIS_LENGHT);
            }

            StringBuilder sb = new StringBuilder();

            sb.Append(this.OnlyNumber());

            if (text.Length > 3) sb.Insert(3, ".");
            if (text.Length > 9) sb.Insert(9, ".");
            if (text.Length > 12) sb.Insert(12, "-");

            return sb.ToString();
        }

        public override int GetLenght()
        {
            return PIS_LENGHT;
        }

        public override bool IsValid()
        {
            return ValidPis();
        }

        private bool ValidPis()
        {
            string pis = this.Text;
            int[] multiplicador = new int[10] { 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;

            if (pis.Trim().Length != 14)
                return false;

            pis = pis.Trim();
            pis = pis.Replace("-", "").Replace(".", "").PadLeft(11, '0');


            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(pis[i].ToString()) * multiplicador[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            return pis.EndsWith(resto.ToString());
        }
    }
}
