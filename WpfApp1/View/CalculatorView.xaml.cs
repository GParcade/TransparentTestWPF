using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfApp1
{
    public partial class CalculatorView : Window
    {
        static SolidColorBrush unactive_bg = new SolidColorBrush { Color = Colors.LightGray };
        static SolidColorBrush active_bg = new SolidColorBrush { Opacity = 0.9, Color = Colors.LightGray };
        public CalculatorView()
        {
            InitializeComponent();
        }
        private void WindowsMain_Deactivated(object sender, EventArgs e)
        {
            WindowsMain.Background = unactive_bg;
        }
        private void WindowsMain_Activated(object sender, EventArgs e)
        {
            WindowsMain.Background = active_bg;
        }
        private void WindowsMain_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            AutoFontSizeValue.FontSize = WindowsMain.ActualHeight - (WindowsMain.ActualHeight / 1.07);
            AutoFontSizeAlgo.FontSize = WindowsMain.ActualHeight - (WindowsMain.ActualHeight / 1.05);
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void Close(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
        private void MaxMin(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }
        private void Hide(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
