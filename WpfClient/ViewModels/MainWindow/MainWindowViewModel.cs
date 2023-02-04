using OpenGLCore;
using System;
using System.ComponentModel;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using AppWindow = WpfClient.Views.MainWindow.MainWindow;
using CommandManager = WpfClient.Models.Managers.CommandManager;

namespace WpfClient.ViewModels.MainWindow
{
    public class MainWindowViewModel
    {
        private CommandManager _commandManager { get; set; }
        private AppWindow _context { get; set; }
        private WindowsFormsHost _winFormsHost;
        private RenderingControl _renderingContext { get; set; }

        public MainWindowViewModel(AppWindow context)
        {
            _context = context;
            _commandManager = new CommandManager(_context);

            AttachContextEvents();

            InitWinFormsHost();
        }

        private void AttachContextEvents()
        {
            _context.ContentRendered += ContextRendered;
            _context.Closing += Context_Closing;
            _context.MouseDown += TitleBar_MouseDown;
            _context.MouseDoubleClick += Context_MouseDoubleClick;
        }

        private void Context_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                _context.MinMaxWindow.Command.Execute(null);
        }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                _context.DragMove();
        }

        private void Context_Closing(object sender, CancelEventArgs e)
        {
            _renderingContext.Dispose();
        }

        private void ContextRendered(object sender, EventArgs e)
        {
            _renderingContext.StartLoop();
        }

        private void InitWinFormsHost()
        {
            _winFormsHost =
                new WindowsFormsHost();

            _renderingContext =
                new RenderingControl();

            _winFormsHost.Child = _renderingContext;

            _context.OpenGLRenderingControl.Children.Add(_winFormsHost);
        }
    }
}
