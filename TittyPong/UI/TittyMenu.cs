using Microsoft.Xna.Framework;
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

namespace TittyPong.UI
{
    public class TittyMenu : IManager
    {

        private Desktop UIHost;
        private Grid UIGrid;

        private TextBlock TitleTxt;
        private TextBlock AddressTxt;
        private TextField AddressFld;
        private Button ConnectBtn;

        public TittyMenu()
        {
            InitUI();
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
            UIGrid.ColumnsProportions.Add(new Grid.Proportion(Grid.ProportionType.Auto));
            UIGrid.RowsProportions.Add(new Grid.Proportion(Grid.ProportionType.Auto));
            UIGrid.RowsProportions.Add(new Grid.Proportion(Grid.ProportionType.Auto));
            UIGrid.RowsProportions.Add(new Grid.Proportion(Grid.ProportionType.Auto));
            UIGrid.RowsProportions.Add(new Grid.Proportion(Grid.ProportionType.Auto));

            TitleTxt = new TextBlock
            {
                Text = "Titty Pong ( . )( . )",
                GridPositionX = 0,
                GridPositionY = 0,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            AddressTxt = new TextBlock
            {
                Text = "Address",
                GridPositionX = 0,
                GridPositionY = 1,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            AddressFld = new TextField
            {
                GridPositionX = 0,
                GridPositionY = 2,
                Width = 512,
                Height = 64,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            ConnectBtn = new Button
            {
                Text = "Connect",
                GridPositionX = 0,
                GridPositionY = 3,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            ConnectBtn.Down += OnConnectionButton;

            UIGrid.Widgets.Add(TitleTxt);
            UIGrid.Widgets.Add(AddressTxt);
            UIGrid.Widgets.Add(AddressFld);
            UIGrid.Widgets.Add(ConnectBtn);
            UIHost.Widgets.Add(UIGrid);
        }
        #endregion

        #region Events
        private void OnConnectionButton(object sender, EventArgs e)
        {
            Master.EM.OnConnectionInfoEvent(this, new StringEventArgs(AddressFld.Text));
        }
        #endregion

        public void Update()
        {
        }

        public void Render()
        {
            UIHost.Bounds = new Rectangle(0, 0, (int)Master.SM.Width(), (int)Master.SM.Height());
            UIHost.Render();
        }

    }
}
