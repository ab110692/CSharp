using System;
using System.Text;

namespace Br.Com.Posi.Util.Formatted
{
    sealed class FormattedNumeroImpl : FormattedImpl
    {
        public override string Formatted(string text)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(this.OnlyNumber());

            return sb.ToString();
        }

        public override int GetLenght()
        {
            return 0;
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
