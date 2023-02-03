using System.Windows;
using WpfClient.Views.MainWindow;

namespace WpfClient.Models.Commands.Tilebar
{
    class MinMaxWindowCommand : Command
    {
        private MainWindow _context { get; set; }

        public MinMaxWindowCommand(MainWindow context)
        {
            CmdId = CommandId.MinMaxWindow;
            _context = context;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            _context.WindowState =
                (_context.WindowState == WindowState.Maximized) ? WindowState.Normal : WindowState.Maximized;
        }
    }
}
