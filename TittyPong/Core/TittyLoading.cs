using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Myra.Graphics2D.UI;
using TittyPong.Contracts;
using TittyPong.Events;
using TittyPong.Graphics;
using TittyPong.IO;

namespace TittyPong.Core
{
    public class TittyLoading : IManager
    {

        private ContentManager Assets;
        private EventManager Events;

        #region UIFields
        private Desktop UIHost;
        private Grid UIGrid;

        private TextBlock TitleTxt;
        private TextBlock LoadingTxt;
        private Texture2D LoadingImg;
        private Rectangle LoadingBdy;
        private int LoadingImgCounter;
        private int LoadingImgTimer;
        private int LoadingImgTime;
        private int LoadingImgMax;
        #endregion

        public TittyLoading(ContentManager assets, EventManager events)
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
            UIGrid.ColumnsProportions.Add(new Grid.Proportion(Grid.ProportionType.Part, 28.0f));
            UIGrid.ColumnsProportions.Add(new Grid.Proportion(Grid.ProportionType.Part, 1.0f));
            UIGrid.RowsProportions.Add(new Grid.Proportion(Grid.ProportionType.Part, 1.0f));
            UIGrid.RowsProportions.Add(new Grid.Proportion(Grid.ProportionType.Part, 10.0f));
            UIGrid.RowsProportions.Add(new Grid.Proportion(Grid.ProportionType.Part, 1.0f));
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
            LoadingTxt = new TextBlock
            {
                Text = "Loading",
                GridPositionX = 1,
                GridPositionY = 3,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            LoadingImgCounter = 1;
            LoadingImgMax = 28;
            LoadingImgTimer = 100;
            LoadingImg = Assets.Load<Texture2D>($"Loading/frame ({LoadingImgCounter})");
            LoadingBdy = new Rectangle(810 - 150, 540 - 150, 300, 150);


            UIGrid.Widgets.Add(TitleTxt);
            UIGrid.Widgets.Add(LoadingTxt);
            UIHost.Widgets.Add(UIGrid);
        }
        #endregion

        #region Events
        public void RegisterEvents()
        {

        }
        #endregion

        public void Update(GameTime delta, InputManager input)
        {
            LoadingImgTime += delta.ElapsedGameTime.Milliseconds;
            if(LoadingImgTime >= LoadingImgTimer)
            {
                LoadingImgTime = 0;
                LoadingImgCounter++;
                if (LoadingImgCounter > 28)
                    LoadingImgCounter = 1;
                LoadingImg = Assets.Load<Texture2D>($"Loading/frame ({LoadingImgCounter})");
            }
        }

        public void Render(GameTime delta, ScreenManager screen)
        {
            UIHost.Bounds = new Rectangle(0, 0, (int)screen.Width(), (int)screen.Height());
            UIHost.Render();
            screen.Render(LoadingImg, LoadingBdy);
        }
    }
}
