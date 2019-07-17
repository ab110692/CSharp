using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Br.Com.Posi.Util
{
    public static class BallonDialog
    {
        private static AnimationClock animationClock;

        private static DoubleAnimation fadeIn;
        private static DoubleAnimation wait;
        private static DoubleAnimation fadeOut;
        private static Window window;
        private static Thread t;

        public static void Show(string msg, string title)
        {
            ShowIn(msg,title);
        }

        public static void Show(string msg, string title, bool focus)
        {
            ShowIn(msg, title);
        }

        private static void ShowIn(string msg, string title)
        {
            if (t == null)
            {
                t = new Thread(() => CreateDialog(msg, title));
                t.SetApartmentState(ApartmentState.STA);
            }

            if (!t.IsAlive)
            {
                if (t.ThreadState == ThreadState.Stopped)
                {
                    t.DisableComObjectEagerCleanup();
                    t = null;
                    t = new Thread(() => CreateDialog(msg, title));
                    t.SetApartmentState(ApartmentState.STA);
                }
                t.Start();
            }
        }


        //[STAThread]
        private static void CreateDialog(string msg, string title)
        {
            window = new Window();
            window.ShowActivated = false;
            window.WindowStyle = WindowStyle.None;
            window.Width = 300;
            window.Height = 150;
            window.Left = -3;
            window.Top = 1;
            window.Topmost = true;
            window.Background = new SolidColorBrush(Colors.DeepSkyBlue);
            window.AllowsTransparency = true;


            var stackPanel = new StackPanel { Orientation = Orientation.Vertical };
            stackPanel.Children.Add(new TextBlock { Text = title, HorizontalAlignment = HorizontalAlignment.Center, FontWeight = FontWeights.Bold, FontSize = 18 });
            stackPanel.Children.Add(
                new ScrollViewer
                {
                    HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled,
                    VerticalScrollBarVisibility = ScrollBarVisibility.Hidden,
                    Height = 120,
                    Content = new TextBlock
                    {
                        Text = msg,
                        TextWrapping = TextWrapping.Wrap,
                        FontSize = 15.5,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    }
                });

            window.Content = stackPanel;

            fadeIn = new DoubleAnimation();
            wait = new DoubleAnimation();
            fadeOut = new DoubleAnimation();

            window.MouseEnter += Window_MouseEnter;
            window.MouseLeave += Window_MouseLeave;

            CreateAnimationIn(window);
        }

        private static void Window_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            animationClock.Controller.Pause();
        }

        private static void Window_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            animationClock.Controller.Resume();
        }

        private static void CreateAnimationIn(Window window)
        {
            fadeIn.Completed += FadeIn_Completed;
            fadeIn.From = -300;
            fadeIn.To = 2;
            fadeIn.Duration = new Duration(TimeSpan.FromSeconds(0.8));
            animationClock = fadeIn.CreateClock();
            window.ApplyAnimationClock(Window.LeftProperty, animationClock);

            window.ShowDialog();
        }

        private static void FadeIn_Completed(object sender, EventArgs e)
        {
            wait.Completed += Wait_Completed;
            wait.From = 2;
            wait.To = 2;
            wait.Duration = new Duration(TimeSpan.FromSeconds(2.8));
            animationClock = wait.CreateClock();
            window.ApplyAnimationClock(Window.LeftProperty, animationClock);
        }

        private static void Wait_Completed(object sender, EventArgs e)
        {
            fadeOut.Completed += FadeOut_Completed;
            fadeOut.From = 2;
            fadeOut.To = -300;
            fadeOut.Duration = new Duration(TimeSpan.FromSeconds(0.8));
            animationClock = fadeOut.CreateClock();
            window.ApplyAnimationClock(Window.LeftProperty, animationClock);
        }

        private static void FadeOut_Completed(object sender, EventArgs e)
        {
            window.Close();
        }
    }
}
