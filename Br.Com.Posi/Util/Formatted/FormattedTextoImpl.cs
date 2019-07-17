using System.Text;

namespace Br.Com.Posi.Util.Formatted
{
    sealed class FormattedTextoImpl : FormattedImpl
    {
        public override string Formatted(string text)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(this.OnlyLettersAndSpecial());

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
