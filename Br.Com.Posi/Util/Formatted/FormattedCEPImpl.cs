using System.Text;

namespace Br.Com.Posi.Util.Formatted
{
    sealed class FormattedCEPImpl : FormattedImpl
    {

        int CEP_LENGHT = 9;

        public override string Formatted(string text)
        {
            if (text.Length > CEP_LENGHT)
            {
                return text.Substring(0, CEP_LENGHT);
            }

            StringBuilder sb = new StringBuilder();

            sb.Append(this.OnlyNumber());

            if (text.Length > 5) sb.Insert(5, "-");

            return sb.ToString();
        }

        public override int GetLenght()
        {
            return CEP_LENGHT;
        }

        public override bool IsValid()
        {
            return GetLenght() == this.Text.Length || GetLenght() == 0;
        }
    }
}
