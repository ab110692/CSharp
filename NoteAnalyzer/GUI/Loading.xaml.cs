using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace NoteAnalyzer.GUI
{
    /// <summary>
    /// Interaction logic for Loading.xaml
    /// </summary>
    public partial class Loading : UserControl
    {

        private readonly DispatcherTimer animationTimer;

        public Loading()
        {
            InitializeComponent();

            animationTimer = new DispatcherTimer(DispatcherPriority.ContextIdle, Dispatcher);
            animationTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
        }

        private void Start()
        {
            //Mouse.OverrideCursor = Cursors.Wait;
            animationTimer.Tick += AnimationTimer_Tick;
            animationTimer.Start();
        }

        private void Stop()
        {
            animationTimer.Stop();
            //Mouse.OverrideCursor = Cursors.Arrow;
            animationTimer.Tick -= AnimationTimer_Tick;
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            SpinnerRotate.Angle = (SpinnerRotate.Angle + 36) % 360;
        }

        private void SetPosition(Ellipse ellipse, double offset, double posOffSet, double step)
        {
            ellipse.SetValue(Canvas.LeftProperty, 50.0 + Math.Sin(offset + posOffSet * step) * 50.0);
            ellipse.SetValue(Canvas.TopProperty, 50.0 + Math.Cos(offset + posOffSet * step) * 50.0);
        }

        private void Canvas_Loaded(object sender, RoutedEventArgs e)
        {
            const double offset = Math.PI;
            const double step = Math.PI * 2 / 10.0;

            SetPosition(C0, offset, 0.0, step);
            SetPosition(C1, offset, 1.0, step);
            SetPosition(C2, offset, 2.0, step);
            SetPosition(C3, offset, 3.0, step);
            SetPosition(C4, offset, 4.0, step);
            SetPosition(C5, offset, 5.0, step);
            SetPosition(C6, offset, 6.0, step);
            SetPosition(C7, offset, 7.0, step);
            SetPosition(C8, offset, 8.0, step);
        }

        private void Canvas_Unloaded(object sender, RoutedEventArgs e)
        {
            Stop();
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            bool IisVisible = (bool)e.NewValue;
            if (IsVisible)
            {
                Start();
            }else
            {
                Stop();
            }
        }
    }
}
