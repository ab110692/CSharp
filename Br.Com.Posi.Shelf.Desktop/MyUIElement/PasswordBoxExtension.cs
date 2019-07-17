using System.Windows;
using System.Windows.Controls;

namespace Br.Com.Posi.Shelf.Desktop.MyUIElement
{
    public static class PasswordBoxExtension
    {
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.RegisterAttached("Password",
                typeof(string), typeof(PasswordBoxExtension),
                new FrameworkPropertyMetadata(string.Empty, OnPasswordPropertyChanged));

        public static readonly DependencyProperty AttachProperty =
            DependencyProperty.RegisterAttached("Attach",
                typeof(bool), typeof(PasswordBoxExtension), new PropertyMetadata(false, Attach));

        public static void SetAttach(DependencyObject dp, bool value)
        {
            dp.SetValue(AttachProperty, value);
        }

        public static bool GetAttach(DependencyObject dp)
        {
            return (bool)dp.GetValue(AttachProperty);
        }

        public static void SetPassword(DependencyObject dp, string value)
        {
            dp.SetValue(PasswordProperty, value);
        }

        private static void OnPasswordPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            PasswordBox pass = sender as PasswordBox;
            pass.PasswordChanged -= PasswordChanged;
            pass.PasswordChanged += PasswordChanged;
        }

        private static void Attach(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PasswordBox pass = d as PasswordBox;

            if (pass == null)
            {
                return;
            }

            pass.PasswordChanged -= PasswordChanged;
            pass.PasswordChanged += PasswordChanged;
        }

        private static void PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox pass = sender as PasswordBox;
            SetPassword(pass, pass.Password);
        }
    }
}
