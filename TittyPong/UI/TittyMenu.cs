using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Myra;
using Myra.Graphics2D.UI;
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
        private TextBlock AddressTxt;
        private TextField AddressFld;
        private Button ConnectBtn;

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
            UIGrid.RowsProportions.Add(new Grid.Proportion(Grid.ProportionType.Part, 8.0f));
            UIGrid.RowsProportions.Add(new Grid.Proportion(Grid.ProportionType.Part, 1.0f));

            TitleTxt = new TextBlock
            {
                Text = "( . ) Titty Pong ( . )",
                GridPositionX = 1,
                GridPositionY = 1,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            AddressTxt = new TextBlock
            {
                Text = "Address",
                GridPositionX = 1,
                GridPositionY = 2,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            AddressFld = new TextField
            {
                Text = "192.168.1.223",
                GridPositionX = 1,
                GridPositionY = 3,
                Width = 256,
                Height = 32,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            ConnectBtn = new Button
            {
                Text = "Connect",
                GridPositionX = 1,
                GridPositionY = 4,
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
                GridPositionY = 5,
                Width = 512,
                Height = 128,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            UIGrid.Widgets.Add(TitleTxt);
            UIGrid.Widgets.Add(AddressTxt);
            UIGrid.Widgets.Add(AddressFld);
            UIGrid.Widgets.Add(ConnectBtn);
            UIGrid.Widgets.Add(ClientConnections);
            UIHost.Widgets.Add(UIGrid);
        }
        
        public void HandleClientListReceived(object sender, ClientListReceivedEventArgs ev)
        {
            var clients = ev.Clients;
            foreach(var client in clients)
            {
                ClientConnections.Items.Add(new ListItem(client));
            }
            
        }
        #endregion

        #region Events
        private void RegisterEvents()
        {
            ConnectBtn.Down += OnConnectionButton;

            events.ClientListReceivedEvent += HandleClientListReceived;
        }


        private void OnConnectionButton(object sender, EventArgs e)
        {
            events.OnConnectionInfoEvent(this, new StringEventArgs(AddressFld.Text));
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
