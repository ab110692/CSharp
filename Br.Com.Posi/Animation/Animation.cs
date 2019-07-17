using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Br.Com.Posi.Animation
{
    public class Animation
    {
        public static void FadeIn(UIElement element, String text, SolidColorBrush color)
        {
            if (element is Label)
            {
                ((Label)element).Foreground = color;
                ((Label)element).Content = text;
            }
            DoubleAnimation animation = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(4));
            element.BeginAnimation(UIElement.OpacityProperty, animation);
        }

        public static void FadeOut  (UIElement element)
        {
            DoubleAnimation animation = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(4));
            element.BeginAnimation(UIElement.OpacityProperty, animation);
        }

        public static void FadeIn(UIElement element)
        {
            DoubleAnimation animation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(1.5));
            element.BeginAnimation(UIElement.OpacityProperty, animation);
        }
    }
}
