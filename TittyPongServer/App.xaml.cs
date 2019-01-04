using System.Windows;

namespace TittyPongServer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private Master _master;

        public static Events Events;

        public App()
        {
            Events = new Events();
            
            var mainWindow = new MainWindow();
            Current.MainWindow = mainWindow;
            mainWindow.Show();
            
            Events.GuiLogMessageEvent += mainWindow.EventsOnGuiLogMessageEvent;
            
            _master = new Master();
        }
    }
}