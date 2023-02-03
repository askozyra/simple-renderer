using System.Windows;
using WpfClient.Views.MainWindow;

namespace WpfClient.Models.Commands.Tilebar
{
    class MinimizeWindowCommand : Command
    {
        private MainWindow _context { get; set; }

        public MinimizeWindowCommand(MainWindow context)
        {
            CmdId = CommandId.MinimizeWindow;
            _context = context;
        }

        public override bool CanExecute(object parameter)
        {
            return _context.WindowState == WindowState.Normal;
        }

        public override void Execute(object parameter)
        {
            _context.WindowState = WindowState.Minimized;
        }
    }
}
