using System;
using System.Linq;
using System.Windows;

namespace TittyPongServer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        public void OnGuiLogMessageEvent(object sender, GuiLogMessageEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(new Action(() => { logTextBox.Text += e.Message + "\n";}));
        }
    }
}