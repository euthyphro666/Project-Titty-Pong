using Common.ECS.Components;
using Common.ECS.Contracts;
using Common.ECS.Nodes;
using Common.ECS.SystemEvents;
using Common.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ECS.Systems
{
    public class RenderSystem : ISystem
    {
        /// <summary>
        /// Time step in ticks
        /// </summary>
        public static float PHYSICS_TIME_STEP = 16_666f; //TODO : Put this in the ambient context

        public uint Priority { get; set; }
        private readonly ISystemContext SystemContext;

        private List<RenderNode> Targets { get; set; }
        private List<LerpableRenderNode> LerpTargets { get; set; }

        private List<DynamicSnapshotNode> Snapshot { get; set; }
        /// <summary>
        /// Timestamps in ticks
        /// </summary>
        private long SnapshotTime;
        private long LastTime;

        private readonly Screen Screen;
        private IEventManager Events;
        
        public RenderSystem(ISystemContext systemContext, Screen screen)
        {
            SystemContext = systemContext;
            Events = SystemContext.Events;
            Targets = new List<RenderNode>();
            LerpTargets = new List<LerpableRenderNode>();
            Screen = screen;

            Events.EntityAddedEvent += OnEntityAddedEvent;
            Events.SnapshotEvent += OnSnapshotEvent;
        }

        /// <summary>
        /// Should fire once every PHYSICS_TIME_STEP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSnapshotEvent(object sender, SnapshotEventArgs e)
        {
            Snapshot = e.Snapshot;
            SnapshotTime = LastTime;
        }

        //TODO : Either make two types of systems or have all systems use Update(ulong delta)
        public void Update(long dt)
        {
            LastTime += dt;
            Screen.Start();
            //Render all non-lerpable render nodes.
            foreach(var target in Targets)
            {
                Screen.Render(
                    target.Display.Sprite, 
                    target.Position.X, 
                    target.Position.Y,
                    target.RigidBody.Width,
                    target.RigidBody.Height);
            }
            //If we even have a snapshot and it's fairly recent (within 100 milliseconds) then lerp.
            var delta = LastTime - SnapshotTime;
            if (Snapshot != null && delta < PHYSICS_TIME_STEP * 6)
            {
                foreach(var newTarget in LerpTargets)
                {
                    var oldTarget = Snapshot.Find(t => t.NetworkIdentity.Id == newTarget.NetId.Id);

                    Screen.Render(
                        newTarget.Display.Sprite,
                        ((1f - (delta / PHYSICS_TIME_STEP)) * oldTarget.Position.X) + 
                        ((delta / PHYSICS_TIME_STEP) * newTarget.Position.X), //Lerped X
                        ((1f - (delta / PHYSICS_TIME_STEP)) * oldTarget.Position.Y) +
                        ((delta / PHYSICS_TIME_STEP) * newTarget.Position.Y), //Lerped Y
                        newTarget.RigidBody.Width,
                        newTarget.RigidBody.Height);
                }
            }
            //Else render lerpable targets as normal.
            else
            {
                foreach (var target in LerpTargets)
                {
                    Screen.Render(
                        target.Display.Sprite,
                        target.Position.X,
                        target.Position.Y,
                        target.RigidBody.Width,
                        target.RigidBody.Height);
                }
            }
            Screen.Stop();
        }

        public void OnEntityAddedEvent(object sender, EntityAddedEventArgs args)
        {
            if (RenderNode.TryCreate(args.Target, out var node))
                Targets.Add(node);
            else if (LerpableRenderNode.TryCreate(args.Target, out var lerpNode))
                LerpTargets.Add(lerpNode);
        }

    }
}
