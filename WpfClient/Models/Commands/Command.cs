using System;
using System.Windows.Input;

namespace WpfClient.Models.Commands
{
    abstract class Command : ICommand
    {
        public CommandId CmdId { get; protected set; }
        public bool IsUndoable { get; set; }

        public event EventHandler CanExecuteChanged;

        public abstract bool CanExecute(object parameter);

        public abstract void Execute(object parameter);
    }
}
