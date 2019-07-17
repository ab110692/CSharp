using System;
using System.Text.RegularExpressions;

namespace Br.Com.Posi.Util.Formatted
{
    sealed class FormattedEmailImpl : FormattedImpl
    {
        public override string Formatted(string text)
        {
            return text;
        }

        public override int GetLenght()
        {
            return 0;
        }

        public override bool IsValid()
        {
            Regex rg = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");

            return rg.IsMatch(this.Text);
        }
    }
}
