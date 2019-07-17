using Br.Com.Posi.Util.Formatted;
using System.Windows;
using System.Windows.Controls;
using Br.Com.Posi.MyUI.Enums;

namespace Br.Com.Posi.MyUI
{
    public class MaskedTextBox : TextBox
    {
        public static readonly DependencyProperty IsValidProperty =
            DependencyProperty.Register("IsValid",
                typeof(bool),
                typeof(MaskedTextBox),
                new PropertyMetadata(false));


        public static readonly DependencyProperty MaskProperty =
            DependencyProperty.Register("Mask",
                typeof(TextBoxMasked),
                typeof(MaskedTextBox),
                new PropertyMetadata(TextBoxMasked.INVALIDO, MaskPropertyChanged));



        private FormattedImpl formatted;

        public TextBoxMasked Mask
        {
            get
            {
                return (TextBoxMasked)GetValue(MaskProperty);
            }
            set
            {
                SetValue(MaskProperty, value);
            }
        }

        public bool IsValid
        {
            get
            {
                this.SetValue(IsValidProperty, formatted.IsValid());
                return (bool)this.GetValue(IsValidProperty);
            }
        }

        private static void MaskPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MaskedTextBox textBox = d as MaskedTextBox;
            textBox.Mask = (TextBoxMasked)e.NewValue;
            textBox.formatted = FactoryFormatted.InitFormatted(textBox.Mask);

            textBox.TextChanged -= textBox.Text_TextChanged;
            textBox.TextChanged -= textBox.MaskedTextBox_TextChanged;

            textBox.TextChanged += textBox.Text_TextChanged;
            textBox.TextChanged += textBox.MaskedTextBox_TextChanged;
        }

        public MaskedTextBox()
        {
        }

        ~MaskedTextBox()
        {
        }

        private void MaskedTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            MaskedTextBox mask = (sender as MaskedTextBox);

            if (mask != null && mask.Text.Length > 0)
            {
                mask.TextChanged -= MaskedTextBox_TextChanged;
                mask.Text = this.Formatted(mask.Text);
                mask.CaretIndex = mask.Text.Length;
                mask.TextChanged += MaskedTextBox_TextChanged;
            }
        }

        private void Text_TextChanged(object sender, TextChangedEventArgs e)
        {
            formatted.Text = this.Text;
        }

        private string Formatted(string text)
        {
            return formatted.Formatted(text);
        }
    }
}
