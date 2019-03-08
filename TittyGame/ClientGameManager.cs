using Common.ECS;
using Common.ECS.Components;
using Common.ECS.Contracts;
using Common.ECS.SystemEvents;
using Common.ECS.Systems;
using Common.Graphics;
using Common.Networking;
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
            var client = new NetworkClient();
            
            GameEngine = new Engine(SystemContext);
            GameEngine.AddSystem(new InputSystem(SystemContext), 1, false)
                      .AddSystem(new NetworkSystem(SystemContext, client), 4, false) // TODO make this the snapshot system
                      .AddSystem(new CollisionSystem(SystemContext), 3, false)
                      .AddSystem(new MovementSystem(SystemContext), 2,false)
                      .AddSystem(new RenderSystem(SystemContext, Screen),1, true);

            
            GameEngine.AddEntity(new Entity("Paddle One")
                                    .Add(new DisplayComponent
                                    {
                                        Sprite = Content.Load<Texture2D>("Paddle")
                                    })
                                    .Add(new PositionComponent
                                    {
                                        X = 116,
                                        Y = 540
                                    })
                                    .Add(new VelocityComponent())
                                    .Add(new RigidBodyComponent
                                    {
                                        Width = 32,
                                        Height = 128,
                                        IsDynamic = true,
                                        IsKinematic = true,
                                        IsRect = true
                                    })
                                    .Add(new PlayerComponent
                                    {
                                        Number = PlayerNumber.One
                                    })
                                    .Add(new NetworkIdentityComponent("Paddle One"){})
                                )
                      .AddEntity(new Entity("Paddle Two")
                                    .Add(new DisplayComponent
                                    {
                                        Sprite = Content.Load<Texture2D>("Paddle")
                                    })
                                    .Add(new PositionComponent
                                    {
                                        X = 1804,
                                        Y = 540
                                    })
                                    .Add(new VelocityComponent())
                                    .Add(new RigidBodyComponent
                                    {
                                        Width = 32,
                                        Height = 128,
                                        IsDynamic = true,
                                        IsKinematic = true,
                                        IsRect = true
                                    })
                                    .Add(new PlayerComponent
                                    {
                                        Number = PlayerNumber.Two
                                    })
                                    .Add(new NetworkIdentityComponent("Paddle Two"){})
                                )
                      .AddEntity(new Entity("Ball")
                                    .Add(new DisplayComponent
                                    {
                                        Sprite = Content.Load<Texture2D>("Ball")
                                    })
                                    .Add(new PositionComponent
                                    {
                                        X = 1920 / 2,
                                        Y = 540
                                    })
                                    .Add(new VelocityComponent
                                    {
                                        X = -4,
                                        Y = 0
                                    })
                                    .Add(new RigidBodyComponent
                                    {
                                        Width = 32,
                                        Height = 32,
                                        IsDynamic = true,
                                        IsKinematic = false,
                                        IsRect = false
                                    })
                                    .Add(new NetworkIdentityComponent("Ball"){})
                                )
                        .AddEntity(new Entity("North-Wall")
                                    .Add(new PositionComponent
                                    {
                                        X = 1920 / 2,
                                        Y = 0 - 128
                                    })
                                    .Add(new VelocityComponent())
                                    .Add(new RigidBodyComponent
                                    {
                                        Width = 1920,
                                        Height = 256,
                                        IsDynamic = false,
                                        IsKinematic = true,
                                        IsRect = true
                                    })
                        )
                        .AddEntity(new Entity("South-Wall")
                                    .Add(new PositionComponent
                                    {
                                        X = 1920 / 2,
                                        Y = 1080 + 128
                                    })
                                    .Add(new VelocityComponent())
                                    .Add(new RigidBodyComponent
                                    {
                                        Width = 1920,
                                        Height = 256,
                                        IsDynamic = false,
                                        IsKinematic = true,
                                        IsRect = true
                                    })
                        )
                        .AddEntity(new Entity("West-Wall")
                                    .Add(new PositionComponent
                                    {
                                        X = 0 - 128,
                                        Y = 1080 / 2
                                    })
                                    .Add(new VelocityComponent())
                                    .Add(new RigidBodyComponent
                                    {
                                        Width = 256,
                                        Height = 1080,
                                        IsDynamic = false,
                                        IsKinematic = true,
                                        IsRect = true
                                    })
                        )
                        .AddEntity(new Entity("East-Wall")
                                    .Add(new PositionComponent
                                    {
                                        X = 1920 + 128,
                                        Y = 1080 / 2
                                    })
                                    .Add(new VelocityComponent())
                                    .Add(new RigidBodyComponent
                                    {
                                        Width = 256,
                                        Height = 1080,
                                        IsDynamic = false,
                                        IsKinematic = true,
                                        IsRect = true
                                    })
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
