using System.Windows;

namespace TittyPongServer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private Master Master;

        public readonly Events Events;

        public App()
        {
            Events = new Events();
            
            var mainWindow = new MainWindow();
            Current.MainWindow = mainWindow;
            mainWindow.Show();
            
            Events.GuiLogMessageEvent += mainWindow.OnGuiLogMessageEvent;
            
            Master = new Master(Events);
        }
    }
}