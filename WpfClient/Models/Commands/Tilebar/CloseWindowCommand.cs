using WpfClient.Views.MainWindow;

namespace WpfClient.Models.Commands.Tilebar
{
    class CloseWindowCommand : Command
    {
        private MainWindow _context { get; set; }

        public CloseWindowCommand(MainWindow context)
        {
            CmdId = CommandId.CloseWindow;
            _context = context;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            _context.Close();
        }
    }
}
