﻿using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using WpfClient.Models.Commands;
using WpfClient.Models.Commands.Tilebar;
using WpfClient.Views.MainWindow;

namespace WpfClient.Models.Managers
{
    class CommandManager
    {
        private MainWindow _context;

        public Dictionary<CommandId, Command> CommandsList { get; private set; }

        public CommandManager(MainWindow context)
        {
            _context = context;

            CommandsList = new Dictionary<CommandId, Command>();

            InitCommands();
            BindCommands();
        }

        private void InitCommands()
        {
            CommandsList[CommandId.CloseWindow] = new CloseWindowCommand(_context);
            CommandsList[CommandId.MinMaxWindow] = new MinMaxWindowCommand(_context);
            CommandsList[CommandId.MinimizeWindow] = new MinimizeWindowCommand(_context);
        }

        private void BindCommands()
        {
            BindCommand(_context.MinimizeWindow, CommandId.MinimizeWindow);
            BindCommand(_context.MinMaxWindow, CommandId.MinMaxWindow);
            BindCommand(_context.CloseWindow, CommandId.CloseWindow);
        }

        private void BindCommand(ButtonBase source, CommandId commandId)
        {
            source.DataContext = this;
            source.Command = CommandsList[commandId];
        }
    }
}
