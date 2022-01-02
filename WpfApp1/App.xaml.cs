using System.Windows;

namespace WpfApp1
{
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            CalculatorView view = new CalculatorView
            {
                DataContext = new CalculatorViewModel(new CalculatorModel())
            };
            view.Show();
        }
    }
}
