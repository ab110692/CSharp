using System;
using System.Text;

namespace Br.Com.Posi.Util.Formatted
{
    sealed class FormattedTelefoneImpl : FormattedImpl
    {

        int TELEFONE_LENGHT = 14;
        int CELULAR_LENGHT = 15;

        public override string Formatted(string text)
        {
            if (text.Length > CELULAR_LENGHT)
            {
                return text.Substring(0, CELULAR_LENGHT);
            }

            StringBuilder sb = new StringBuilder();

            sb.Append(this.OnlyNumber());

            if (text.Length < TELEFONE_LENGHT)
            {
                if (text.Length > 0) sb.Insert(0, "(");
                if (text.Length > 3) sb.Insert(3, ")");
                if (text.Length > 3) sb.Insert(4, " ");
                if (text.Length > 9) sb.Insert(9, "-");
            }
            else
            {
                if (text.Length > 0) sb.Insert(0, "(");
                if (text.Length > 3) sb.Insert(3, ")");
                if (text.Length > 3) sb.Insert(4, " ");
                if (text.Length > 10) sb.Insert(10, "-");
            }

            return sb.ToString();
        }

        public override int GetLenght()
        {
            return this.Text.Length <= TELEFONE_LENGHT ? TELEFONE_LENGHT : CELULAR_LENGHT;
        }

        public override bool IsValid()
        {
            if (this.Text.Length == TELEFONE_LENGHT)
            {
                return true;
            }
            else if (this.Text.Length == CELULAR_LENGHT)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
