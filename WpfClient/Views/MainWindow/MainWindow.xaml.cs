using System.Windows;
using Tools.Helpers;

namespace WpfClient.Views.MainWindow
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            PathHelper.BuildAbsolutePath(string.Empty);

            this.InitializeComponent();
        }
    }
}
