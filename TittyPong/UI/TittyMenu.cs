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

namespace TittyPong.UI
{
    public class TittyMenu : IManager
    {

        private Desktop UIHost;
        private Grid UIGrid;

        private Button ConnectBtn;
        private TextField ConnectFld;

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
                RowSpacing = 8,
                ColumnSpacing = 8
            };
            UIGrid.ColumnsProportions.Add(new Grid.Proportion(Grid.ProportionType.Auto));
            UIGrid.ColumnsProportions.Add(new Grid.Proportion(Grid.ProportionType.Auto));
            UIGrid.RowsProportions.Add(new Grid.Proportion(Grid.ProportionType.Auto));
            UIGrid.RowsProportions.Add(new Grid.Proportion(Grid.ProportionType.Auto));
            
            var ConnectBtn = new Button
            {
                GridPositionX = 1,
                GridPositionY = 0,
                Text = "Connect"
            };
            ConnectBtn.Down += (s, a) =>
            {
                var messageBox = Dialog.CreateMessageBox("Connecting", "Connecting...!");
                messageBox.ShowModal(UIHost);
            };
            ConnectFld = new TextField
            {
                GridPositionX = 2,
                GridPositionY = 0,
                Text = "Port"
            };
            

            UIGrid.Widgets.Add(ConnectBtn);
            UIGrid.Widgets.Add(ConnectFld);
            UIHost.Widgets.Add(UIGrid);
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
