using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Myra.Graphics2D.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TittyPong.Events;
using TittyPong.Graphics;
using TittyPong.IO;

namespace TittyPong.Core
{
    public class TittyDebugger
    {
        private ContentManager Assets;
        private EventManager Events;

        #region UIFields
        private Desktop UIHost;
        private Grid UIGrid;

        private TextBlock DebugTxt;
        #endregion

        public TittyDebugger(ContentManager assets, EventManager events)
        {
            Assets = assets;
            Events = events;
            InitUI();
            RegisterEvents();
        }

        #region UI
        public void InitUI()
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
            UIGrid.ColumnsProportions.Add(new Grid.Proportion(Grid.ProportionType.Part, 90.0f));
            UIGrid.RowsProportions.Add(new Grid.Proportion(Grid.ProportionType.Part, 1.0f));
            UIGrid.RowsProportions.Add(new Grid.Proportion(Grid.ProportionType.Part, 90.0f));


            DebugTxt = new TextBlock
            {
                Text = "",
                GridPositionX = 0,
                GridPositionY = 0,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            
            UIGrid.Widgets.Add(DebugTxt);
            UIHost.Widgets.Add(UIGrid);
        }
        #endregion

        #region Events
        public void RegisterEvents()
        {
            Events.LoggingEvent += HandleLoggingEvent;
        }

        public void HandleLoggingEvent(object sender, StringEventArgs e)
        {
            DebugTxt.Text += e.Data + "\n";
        }
        #endregion

        public void Render(GameTime delta, ScreenManager screen)
        {
            UIHost.Bounds = new Rectangle(0, 0, (int)screen.Width(), (int)screen.Height());
            UIHost.Render();
        }
    }
}
