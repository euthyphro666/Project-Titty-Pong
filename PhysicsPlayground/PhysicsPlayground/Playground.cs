using Common.ECS;
using Common.ECS.Components;
using Common.ECS.Contracts;
using Common.ECS.SystemEvents;
using Common.ECS.Systems;
using Common.Graphics;
using Common.Networking;
using Common.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace PhysicsPlayground
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Playground : Game
    {
        private List<Entity> Entities;
        private Screen Screen;
        private ISystemContext SystemContext;

        public Playground()
        {
            Content.RootDirectory = "Content";

            //This must be created in the constructor of the monogame game root.
            Screen = new Screen(this);
            Entities = new List<Entity>();
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
            Entities.Add((new Entity("Paddle One")
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
                        Width = 64,
                        Height = 256,
                        IsDynamic = true,
                        IsKinematic = true,
                        IsRect = true
                    })
                    .Add(new PlayerComponent
                    {
                        Number = PlayerNumber.Two
                    })
                    .Add(new NetworkIdentityComponent("Paddle One") { })
                ));
            Entities.Add((new Entity("Paddle Two")
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
                    Width = 64,
                    Height = 256,
                    IsDynamic = true,
                    IsKinematic = true,
                    IsRect = true
                })
                .Add(new PlayerComponent
                {
                    Number = PlayerNumber.Two
                })
                .Add(new NetworkIdentityComponent("Paddle Two") { })
            ));
            Entities.Add(new Entity("Ball")
                .Add(new DisplayComponent
                {
                    Sprite = Content.Load<Texture2D>("Ball")
                })
                .Add(new PositionComponent
                {
                    X = 1920 / 2,
                    Y = 540
                })
                .Add(new VelocityComponent())
                .Add(new RigidBodyComponent
                {
                    Width = 128,
                    Height = 128,
                    IsDynamic = true,
                    IsKinematic = false,
                    IsRect = false
                })
                .Add(new NetworkIdentityComponent("Ball"))
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

            ColoredEntities.Clear();
            var mouse = Mouse.GetState();
            foreach (var e1 in Entities)
            {
                if (e1.TryGetComponent(typeof(IdentityComponent), out var identity))
                {
                    var i = identity as IdentityComponent;
                    if (i.Name == "Paddle One")
                    {
                        if (e1.TryGetComponent(typeof(PositionComponent), out var pos) &&
                            e1.TryGetComponent(typeof(RigidBodyComponent), out var bod))
                        {
                            var p1 = pos as PositionComponent;
                            var b1 = bod as RigidBodyComponent;

                            p1.X = mouse.X;
                            p1.Y = mouse.Y;
                            foreach (var e2 in Entities)
                            {
                                if (e1 == e2)
                                    continue;
                                if (e2.TryGetComponent(typeof(PositionComponent), out pos) &&
                                    e2.TryGetComponent(typeof(RigidBodyComponent), out bod) &&
                                    e2.TryGetComponent(typeof(IdentityComponent), out identity))
                                {
                                    var p2 = pos as PositionComponent;
                                    var b2 = bod as RigidBodyComponent;
                                    var i2 = identity as IdentityComponent;
                                    if (Maths.Intersects(b1, b2, p1, p2))
                                    {
                                        ColoredEntities.Add(i.Name);
                                        ColoredEntities.Add(i2.Name);
                                    }
                                }
                            }
                        }   
                    }
                }
            }
        }

        static List<string> ColoredEntities = new List<string>();

        protected override void Draw(GameTime delta)
        {
            base.Draw(delta);
            Screen.Start();
            foreach (var entity in Entities)
            {
                if(entity.TryGetComponent(typeof(DisplayComponent), out var display))
                {
                    if (entity.TryGetComponent(typeof(PositionComponent), out var pos))
                    {
                        if (entity.TryGetComponent(typeof(RigidBodyComponent), out var bod))
                        {
                            if (entity.TryGetComponent(typeof(IdentityComponent), out var id))
                            {
                                var d = display as DisplayComponent;
                                var p = pos as PositionComponent;
                                var b = bod as RigidBodyComponent;
                                var i = id as IdentityComponent;
                                if (ColoredEntities.Contains(i.Name))
                                    Screen.Render(d.Sprite, p.X, p.Y, b.Width, b.Height, Color.Red);
                                else
                                    Screen.Render(d.Sprite, p.X, p.Y, b.Width, b.Height);
                            }
                        }
                    }

                }
            }
            Screen.Stop();
        }

    }
}
