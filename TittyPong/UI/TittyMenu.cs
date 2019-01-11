using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Myra;
using Myra.Graphics2D.UI;
using Myra.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TittyPong.Contracts;
using TittyPong.Core;
using TittyPong.Events;
using TittyPong.Graphics;
using TittyPong.IO;

namespace TittyPong.UI
{
    public class TittyMenu : IManager
    {

        private ContentManager assets;
        private EventManager events;

        #region UIFields
        private Desktop UIHost;
        private Grid UIGrid;

        private TextBlock TitleTxt;
        private TextBlock DisplayNameTxt;
        private TextField DisplayNameFld;
        private TextBlock AddressTxt;
        private TextField AddressFld;
        private Button ConnectBtn;

        private Dialog GameRequestDlg;

        private ListBox ClientConnections;
        #endregion

        public TittyMenu(ContentManager ass, EventManager ev)
        {
            assets = ass;
            events = ev;
            InitUI();
            RegisterEvents();
        }

        #region UI
        private void InitUI()
        {
            UIHost = new Desktop();
            UIGrid = new Grid
            {
                RowSpacing = 15,
                ColumnSpacing = 15,
                PaddingLeft = 100,
                PaddingTop = 100
            };
            UIGrid.ColumnsProportions.Add(new Grid.Proportion(Grid.ProportionType.Part, 1.0f));
            UIGrid.ColumnsProportions.Add(new Grid.Proportion(Grid.ProportionType.Part, 28.0f));
            UIGrid.ColumnsProportions.Add(new Grid.Proportion(Grid.ProportionType.Part, 1.0f));
            UIGrid.RowsProportions.Add(new Grid.Proportion(Grid.ProportionType.Part, 1.0f));
            UIGrid.RowsProportions.Add(new Grid.Proportion(Grid.ProportionType.Part, 10.0f));
            UIGrid.RowsProportions.Add(new Grid.Proportion(Grid.ProportionType.Part, 2.0f));
            UIGrid.RowsProportions.Add(new Grid.Proportion(Grid.ProportionType.Part, 2.0f));
            UIGrid.RowsProportions.Add(new Grid.Proportion(Grid.ProportionType.Part, 2.0f));
            UIGrid.RowsProportions.Add(new Grid.Proportion(Grid.ProportionType.Part, 2.0f));
            UIGrid.RowsProportions.Add(new Grid.Proportion(Grid.ProportionType.Part, 2.0f));
            UIGrid.RowsProportions.Add(new Grid.Proportion(Grid.ProportionType.Part, 8.0f));
            UIGrid.RowsProportions.Add(new Grid.Proportion(Grid.ProportionType.Part, 1.0f));

            TitleTxt = new TextBlock
            {
                Text = "( . ) Titty Pong ( . )",
                GridPositionX = 1,
                GridPositionY = 1,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            DisplayNameTxt = new TextBlock
            {
                Text = "Display Name",
                GridPositionX = 1,
                GridPositionY = 2,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            DisplayNameFld = new TextField
            {
                Text = "",
                GridPositionX = 1,
                GridPositionY = 3,
                Width = 256,
                Height = 32,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            AddressTxt = new TextBlock
            {
                Text = "Address",
                GridPositionX = 1,
                GridPositionY = 4,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            AddressFld = new TextField
            {
                Text = "192.168.1.223",
                GridPositionX = 1,
                GridPositionY = 5,
                Width = 256,
                Height = 32,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            ConnectBtn = new Button
            {
                Text = "Connect",
                GridPositionX = 1,
                GridPositionY = 6,
                Width = 128,
                Height = 24,
                ContentHorizontalAlignment = HorizontalAlignment.Center,
                ContentVerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            ClientConnections = new ListBox
            {
                Visible = true,
                GridPositionX = 1,
                GridPositionY = 7,
                Width = 512,
                Height = 128,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            GameRequestDlg = new Dialog
            {
                Title = "Accept Game?",
                GridPositionX = 1,
                GridPositionY = 5
            };


            UIGrid.Widgets.Add(TitleTxt);
            UIGrid.Widgets.Add(DisplayNameTxt);
            UIGrid.Widgets.Add(DisplayNameFld);
            UIGrid.Widgets.Add(AddressTxt);
            UIGrid.Widgets.Add(AddressFld);
            UIGrid.Widgets.Add(ConnectBtn);
            UIGrid.Widgets.Add(ClientConnections);
            UIHost.Widgets.Add(UIGrid);

            //HandleClientRequestGame(null, new ConnectionInfoEventArgs("Some dude", "-----"));
        }
        #endregion

        #region Events
        public void HandleClientRequestGame(object sender, ConnectionInfoEventArgs ev)
        {
            //Another client has challenged this client, show the dialog box with the prompt.
            GameRequestDlg.Content = new TextBlock
            {
                Text = $"{ev.DisplayName} has challenged you to a match. Will you accept or are you a bitch?"
            };
            //Handles the dialog response.
            GameRequestDlg.Closed += (s, e) => 
            {
                if (GameRequestDlg.Result)
                {
                    //Accepts the request and sends the result to the server
                }
            };
            GameRequestDlg.ShowModal(UIHost);
        }

        /// <summary>
        /// The server has sent a list of connected clients. The connected clients list needs to be populated.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ev"></param>
        public void HandleClientListReceived(object sender, ClientListReceivedEventArgs ev)
        {
            ClientConnections.Items.Clear();
            var clients = ev.ClientMacToDisplayDictionary;
            foreach (var client in clients)
            {
                ClientConnections.Items.Add(new ListItem(client.Value)
                {
                    Tag = client.Key
                });
            }

        }

        /// <summary>
        /// The user has double clicked a client in the client connections list, the ui then triggers an event starting
        /// the game request sequence.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleClientSelectionEvent(object sender, GenericEventArgs<MouseButtons> e)
        {
            var selectedClientId = ClientConnections.SelectedItem.Tag.ToString();
            var selectedClientDisplay = ClientConnections.SelectedItem.Text;
            events.OnConnectToClientRequestEvent(this, new ConnectionInfoEventArgs(selectedClientDisplay, selectedClientId));

        }

        /// <summary>
        /// The user has clicked the connect button and so the connection info is sent and the client connects to the server.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleConnectionButton(object sender, EventArgs e)
        {
            var args = new ConnectionInfoEventArgs(DisplayNameFld.Text ?? "NOBODY", AddressFld.Text ?? "");
            events.OnConnectionInfoEvent(this, args);
        }

        private void RegisterEvents()
        {
            ConnectBtn.Down += HandleConnectionButton;
            ClientConnections.MouseUp += HandleClientSelectionEvent;

            events.ClientListReceivedEvent += HandleClientListReceived;
        }
        #endregion

        public void Update(GameTime delta, InputManager input)
        {
        }

        public void Render(GameTime delta, ScreenManager screen)
        {
            UIHost.Bounds = new Rectangle(0, 0, (int)screen.Width(), (int)screen.Height());
            UIHost.Render();
        }

        
        
    }
}
