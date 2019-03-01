using Common.ECS.Contracts;
using Common.ECS.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.ECS.SystemEvents;
using Common.ECS.Components;
using Microsoft.Xna.Framework;
using Common.Utils;

namespace Common.ECS.Systems
{
    public class MovementSystem : ISystem
    {
        public uint Priority { get; set; }

        private readonly ISystemContext SystemContext;
        private List<MovementNode> Targets;
        private List<PlayerNode> PlayerTargets;

        private IEventManager Events;

        public MovementSystem(ISystemContext systemContext)
        {
            SystemContext = systemContext;
            Events = SystemContext.Events;
            Targets = new List<MovementNode>();
            PlayerTargets = new List<PlayerNode>();

            Events.InputEvent += OnInputEvent;
            Events.EntityAddedEvent += OnEntityAddedEvent;
            Events.CollisionEvent += OnCollisionEvent;
        }

        private void OnCollisionEvent(object sender, CollisionEventArgs e)
        {
            if (e.Node1.RigidBody.IsDynamic)
            {
                Maths.Reverse(e.Node1.Velocity);
                Maths.Apply(e.Node1.Velocity, e.Node1.Position);
            }
            if (e.Node2.RigidBody.IsDynamic)
            {
                Maths.Reverse(e.Node2.Velocity);
                Maths.Apply(e.Node2.Velocity, e.Node2.Position);
            }
        }

        private void OnEntityAddedEvent(object sender, EntityAddedEventArgs args)
        {
            if (PlayerNode.TryCreate(args.Target, out var playerNode))
                PlayerTargets.Add(playerNode);
            else if (MovementNode.TryCreate(args.Target, out var moveNode))
                Targets.Add(moveNode);
        }

        private void OnInputEvent(object sender, InputEventArgs e)
        {
            foreach(var target in PlayerTargets)
            {
                if(target.Player.Number == e.Player)
                {
                    target.Velocity.Y = e.Input;
                }
            }
        }

        public void Update()
        {
            foreach (var player in PlayerTargets)
            {
                if (player.Velocity.X == 0 && player.Velocity.Y == 0)
                    continue;
                player.Position.X += player.Velocity.X;
                player.Position.Y += player.Velocity.Y;
            }
            foreach (var target in Targets)
            {
                if (target.Velocity.X == 0 && target.Velocity.Y == 0)
                    continue;
                target.Position.X += target.Velocity.X;
                target.Position.Y += target.Velocity.Y;
            }
        }
    }
}