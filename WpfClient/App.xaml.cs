using System.Windows;
using WpfClient.ViewModels.MainWindow;
using WpfClient.Views.MainWindow;

namespace WpfClient
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainWindow window = new MainWindow();
            MainWindowViewModel VM = new MainWindowViewModel(window);
            window.DataContext = VM;
            window.Show();
        }
    }
}
