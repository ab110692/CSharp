using System.Windows;
using System.Windows.Controls;

namespace Br.Com.Posi.Util
{
    public static class MyNavegate
    {
        public static void Navegate(this Window window,Page content)
        {            
            window.Content = content;
        }

        public static void Navegate(this Page window, Page content)
        {
            window.Content = content;
        }

        public static void Navegate(this Frame window, Page content)
        {
            window.Content = content;
        }
    }
}
