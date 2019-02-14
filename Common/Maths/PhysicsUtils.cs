using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Maths
{
    public static class PhysicsUtils
    {
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
    }
}
