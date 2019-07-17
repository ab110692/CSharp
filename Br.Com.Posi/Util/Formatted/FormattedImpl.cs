using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Br.Com.Posi.Util.Formatted
{
    public abstract class FormattedImpl : IFormatted
    {
        public abstract string Formatted(string text);

        public abstract int GetLenght();

        public string OnlyNumber()
        {
            StringBuilder sb = new StringBuilder();

            Regex regex = new Regex("[0-9]");

            MatchCollection matches = regex.Matches(this.Text);

            foreach (Match m in matches)
            {
                sb.Append(m.Value);
            }

            return sb.ToString();
        }

        public string OnlyLetters()
        {
            StringBuilder sb = new StringBuilder();

            Regex regex = new Regex("^\\w*(\\s\\w*)?$");

            MatchCollection matches = regex.Matches(this.Text);

            foreach (Match m in matches)
            {
                sb.Append(m.Value);
            }

            return sb.ToString();
        }

        public string OnlyLettersAndSpecial()
        {
            StringBuilder sb = new StringBuilder();

            Regex regex = new Regex("[^0-9]");

            MatchCollection matches = regex.Matches(this.Text);

            foreach (Match m in matches)
            {
                sb.Append(m.Value);
            }

            return sb.ToString();
        }

        public abstract bool IsValid();

        public string Text { get; set; }
    }
}
