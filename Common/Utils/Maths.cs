using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.ECS.Components;

namespace Common.Utils
{
    public static partial class Maths
    {

        public static void Reverse(VelocityComponent velocity)
        {
            velocity.X *= -1;
            velocity.Y *= -1;
        }

        public static void Apply(VelocityComponent velocity, PositionComponent position)
        {
            position.X += velocity.X;
            position.Y += velocity.Y;
        }

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

        public static float Distance2(
            float x1, float y1,
            float x2, float y2)
        {
            var dx = x1 - x2;
            var dy = y1 - y2;
            return (dx * dx) + (dy * dy);
        }

        //public static void CalculateRebound(RigidBodyComponent body, PositionComponent pos, ref Vector2 velocity)
        //{
        //    //var magnitude = velocity.Length();
        //    //velocity.X = body.X - pos.X;
        //    //velocity.Y = body. - pos.Y;
        //    //velocity.Normalize();
        //    //velocity *= magnitude;
        //}
    }
}
