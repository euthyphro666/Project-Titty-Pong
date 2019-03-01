using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.ECS.Components;

namespace Common.Utils
{
    public static class TittyMaths
    {
        public static byte Clamp(int value)
        {
            return (byte)(value > byte.MinValue ? ((value > byte.MaxValue) ? byte.MaxValue : value) : byte.MinValue);
        }

        public static float Clamp(float value, float min, float max)
        {
            return value > min ? ((value > max) ? max : value) : min;
        }
        public static float Max(float a, float b)
        {
            return a > b ? a : b;
        }
        public static float Min(float a, float b)
        {
            return a < b ? a : b;
        }
        public static float Distance(Vector2 a, Vector2 b)
        {
            var xx = a.X - b.X;
            var yy = a.Y - b.Y;
            return (float)Math.Sqrt((double)(xx * xx) + (yy * yy));
        }
        public static float Distance2(Vector2 a, Vector2 b)
        {
            var xx = a.X - b.X;
            var yy = a.Y - b.Y;
            return (xx * xx) + (yy * yy);
        }
        
        
        public static bool Intersects(RigidBodyComponent bodyA, PositionComponent posA, RigidBodyComponent bodyB, PositionComponent posB)
        {
            if (bodyA.IsRect)
                return bodyB.IsRect ? RectToRectIntersect(bodyA, posA, bodyB, posB) : CircleToRectIntersect(bodyB, posB, bodyA, posA);

            return bodyB.IsRect ? CircleToRectIntersect(bodyA, posA, bodyB, posB) : CircleToCircleIntersect(bodyA, posA, bodyB, posB);
        }

        /// <summary>
        /// Assumes equal width and height
        /// </summary>
        /// <param name="bodyA"></param>
        /// <param name="posA"></param>
        /// <param name="bodyB"></param>
        /// <param name="posB"></param>
        /// <returns></returns>
        public static bool CircleToCircleIntersect(RigidBodyComponent bodyA, PositionComponent posA, RigidBodyComponent bodyB, PositionComponent posB)
        {
            var aRadius = bodyA.Width / 2;
            var bRadius = bodyB.Width / 2;

            // (x1-x2)^2 + (y1-y2)^2 <= (r1+r2)^2
            if ((posA.X - posB.X) * (posA.X - posB.X) + (posA.Y - posB.Y) * (posA.Y - posB.Y) <= (aRadius + bRadius) * (aRadius + bRadius))
            {
                
            }
            
            return false;
        }

        public static bool CircleToRectIntersect(RigidBodyComponent circleBodyA, PositionComponent posA, RigidBodyComponent rectBodyB, PositionComponent posB)
        {
            return false;
        }

        public static bool RectToRectIntersect(RigidBodyComponent bodyA, PositionComponent posA, RigidBodyComponent bodyB, PositionComponent posB)
        {
            return false;
        }
    }
}
