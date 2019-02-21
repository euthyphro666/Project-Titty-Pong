using Common.ECS;
using Common.ECS.Components;
using Common.ECS.Contracts;
using Common.ECS.SystemEvents;
using Common.ECS.Systems;
using Common.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TittyGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Master : Game
    {
        private Engine GameEngine;
        private Screen Screen;
        private ISystemContext SystemContext;

        public Master()
        {
            Content.RootDirectory = "Content";

            //This must be created in the constructor of the monogame game root.
            Screen = new Screen(this);
            SystemContext = new SystemContext(1920, 1080);
        }
        
        protected override void Initialize()
        {
            base.Initialize();
            IsMouseVisible = true;
            Screen.Init();
            LoadEngine();
        }

        private void LoadEngine()
        {
            GameEngine = new Engine(SystemContext);
            GameEngine.AddSystem(new InputSystem(SystemContext), false)
                      .AddSystem(new CollisionSystem(SystemContext), false)
                      .AddSystem(new MovementSystem(SystemContext), false)
                      .AddSystem(new RenderSystem(SystemContext, Screen), true);

            GameEngine.AddEntity(new Entity()
                                    .Add(new DisplayComponent
                                    {
                                        Sprite = Content.Load<Texture2D>("Paddle")
                                    })
                                    .Add(new PositionComponent
                                    {
                                        X = 100,
                                        Y = 100
                                    })
                                    .Add(new VelocityComponent())
                                    .Add(new RigidBodyComponent
                                    {
                                        Width = 64,
                                        Height = 256
                                    })
                                )
                      .AddEntity(new Entity()
                                    .Add(new DisplayComponent
                                    {
                                        Sprite = Content.Load<Texture2D>("Paddle")
                                    })
                                    .Add(new PositionComponent())
                                    .Add(new VelocityComponent())
                                    .Add(new RigidBodyComponent())
                                )
                      .AddEntity(new Entity()
                                    .Add(new DisplayComponent
                                    {
                                        Sprite = Content.Load<Texture2D>("Ball")
                                    })
                                    .Add(new PositionComponent())
                                    .Add(new VelocityComponent())
                                    .Add(new RigidBodyComponent())
                                );
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        protected override void Update(GameTime delta)
        {
            base.Update(delta);
            GameEngine.Update();
        }

        protected override void Draw(GameTime delta)
        {
            base.Draw(delta);
            GameEngine.Render();
        }

    }
}
