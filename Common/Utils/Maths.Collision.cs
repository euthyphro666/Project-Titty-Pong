using Common.ECS.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utils
{
    public static partial class Maths
    {
        /// <summary>
        /// Determines if two entities are colliding by taking there rigidbodies and positions.
        /// </summary>
        /// <param name="b1">Body of entity one.</param>
        /// <param name="b2">Body of entity two.</param>
        /// <param name="p1">Position of entity one.</param>
        /// <param name="p2">Position of entity two.</param>
        /// <returns>Whether the entities are collided.</returns>
        public static bool Intersects(
            RigidBodyComponent b1, RigidBodyComponent b2,
            PositionComponent p1, PositionComponent p2)
        {
            if (b1.IsRect)
                return b2.IsRect ?
                    RectangleIntersectsRectangle(
                        p1.X - (b1.Width / 2), p1.Y - (b1.Height / 2),
                        p1.X + (b1.Width / 2), p1.Y + (b1.Height / 2),
                        p2.X - (b2.Width / 2), p2.Y - (b2.Height / 2),
                        p2.X + (b2.Width / 2), p2.Y + (b2.Height / 2)) :
                    CircleIntersectsRectangle(
                        p2.X, p2.Y, b2.Width / 2,
                        p1.X, p1.Y, b1.Width, b1.Height);
            return b2.IsRect ?
                    CircleIntersectsRectangle(
                        p1.X, p1.Y, b1.Width / 2,
                        p2.X, p2.Y, b2.Width, b2.Height) :
                    CircleIntersectsCircle(
                        p1.X, p1.Y, b1.Width / 2,
                        p2.X, p2.Y, b2.Width / 2);
        }

        /// <summary>
        /// Determines if two circles are intersecting.
        /// </summary>
        /// <param name="x1">Center X position of circle one.</param>
        /// <param name="y1">Center Y position of circle one.</param>
        /// <param name="r1">Radius of cirlce one.</param>
        /// <param name="x2">Center X position of circle two.</param>
        /// <param name="y2">Center Y position of circle two.</param>
        /// <param name="r2">Radius of circle two.</param>
        /// <returns>Whether the circles are intersecting.</returns>
        public static bool CircleIntersectsCircle(
            float x1, float y1, float r1,
            float x2, float y2, float r2)
        {
            var cd = Distance2(x1, y1, x2, y2);
            var rd = (r1 * r2) + (2 * r1 * r2) + (r2 * r2);
            return cd <= rd;
        }

        /// <summary>
        /// Determines if two rectangles are intersecting.
        /// </summary>
        /// <param name="x1">Corner one X position of rectangle one.</param>
        /// <param name="y1">Corner one Y position of rectangle one.</param>
        /// <param name="x2">Corner two X position of rectangle one.</param>
        /// <param name="y2">Corner two Y position of rectangle one.</param>
        /// <param name="u1">Corner one X position of rectangle two.</param>
        /// <param name="v1">Corner one Y position of rectangle two.</param>
        /// <param name="u2">Corner two X position of rectangle two.</param>
        /// <param name="v2">Corner two Y position of rectangle two.</param>
        /// <returns>Whether the rectangles are intersecting.</returns>
        public static bool RectangleIntersectsRectangle(
            float x1, float y1, float x2, float y2,
            float u1, float v1, float u2, float v2)
        {
            return !(u1 >= x2 ||
                     u2 <= x1 ||
                     v1 >= y2 ||
                     v2 <= y1);
        }

        /// <summary>
        /// Determines if a circle and rectangle are intersecting.
        /// </summary>
        /// <param name="cx">Center X position of circle.</param>
        /// <param name="cy">Center Y position of circle.</param>
        /// <param name="r">Radius of circle.</param>
        /// <param name="x">Center X position of rectangle.</param>
        /// <param name="y">Center Y position of rectangle.</param>
        /// <param name="w">Half the width of the rectangle.</param>
        /// <param name="h">Half the height of the rectangle.</param>
        /// <returns>Whether the circle and rectangle are intersecting.</returns>
        public static bool CircleIntersectsRectangle(
            float cx, float cy, float r,
            float x, float y,
            float w, float h)
        {
            return PointIntersectsRectangle(cx, cy, x - w, y - h, x + w, y + h) ||
                   LineIntersectsCircle(cx, cy, r, x - w, y - h, x + w, y - h) ||
                   LineIntersectsCircle(cx, cy, r, x - w, y + h, x + w, y + h) ||
                   LineIntersectsCircle(cx, cy, r, x - w, y - h, x - w, y + h) ||
                   LineIntersectsCircle(cx, cy, r, x + w, y - h, x + w, y + h);
        }

        public static bool LineIntersectsCircle(
            float cx, float cy, float r,
            float x1, float y1,
            float x2, float y2)
        {
            var dx = x2 - x1;
            var dy = y2 - y1;
            var dr = Math.Sqrt((dx * dx) + (dy * dy));
            var d = (x1 * y2) - (x2 * y1);
            return ((r * r * dr * dr) - (d * d)) < 0;
        }

        public static bool PointIntersectsRectangle(
            float px, float py,
            float rx1, float ry1,
            float rx2, float ry2)
        {
            return (px >= rx1 &&
                    px <= rx2 &&
                    py >= ry1 &&
                    py <= ry2);
        }
    }
}
