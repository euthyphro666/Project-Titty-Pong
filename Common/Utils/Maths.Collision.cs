using Common.ECS.Components;
using Microsoft.Xna.Framework;
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
        /// <param name="body1">Body of entity one.</param>
        /// <param name="body2">Body of entity two.</param>
        /// <param name="pos1">Position of entity one.</param>
        /// <param name="pos2">Position of entity two.</param>
        /// <returns>Whether the entities are collided.</returns>
        public static bool Intersects(
            RigidBodyComponent body1, RigidBodyComponent body2,
            PositionComponent pos1, PositionComponent pos2)
        {
            if (body1.IsRect)
                return body2.IsRect ?
                    RectangleIntersectsRectangle(
                        pos1.X - (body1.Width / 2), pos1.Y - (body1.Height / 2),
                        pos1.X + (body1.Width / 2), pos1.Y + (body1.Height / 2),
                        pos2.X - (body2.Width / 2), pos2.Y - (body2.Height / 2),
                        pos2.X + (body2.Width / 2), pos2.Y + (body2.Height / 2)) :
                    CircleIntersectsRectangle(
                        pos2.X, pos2.Y, body2.Width / 2,
                        pos1.X, pos1.Y, body1.Width / 2, body1.Height / 2);
            return body2.IsRect ?
                    CircleIntersectsRectangle(
                        pos1.X, pos1.Y, body1.Width / 2,
                        pos2.X, pos2.Y, body2.Width / 2, body2.Height / 2) :
                    CircleIntersectsCircle(
                        pos1.X, pos1.Y, body1.Width / 2,
                        pos2.X, pos2.Y, body2.Width / 2);
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
            var cp = new Vector2(cx, cy);
            var v1 = new Vector2(x - w, y - h);
            var v2 = new Vector2(x - w, y + h);
            var v3 = new Vector2(x + w, y - h);
            var v4 = new Vector2(x + w, y + h);
            return LineIntersectsCircle(v1, v3, cp, r) ||
                   LineIntersectsCircle(v2, v4, cp, r) ||
                   LineIntersectsCircle(v1, v2, cp, r) ||
                   LineIntersectsCircle(v3, v4, cp, r) ||
                   PointIntersectsRectangle(cx, cy, x - w, y - h, x + w, y + h);
        }

        public static bool LineIntersectsCircle(
            Vector2 segment1, Vector2 segment2,
            Vector2 circlePos, float radius)
        {
            var closest = ClosestPointOnSegmentToPoint(segment1, segment2, circlePos);
            var distance = Math.Sqrt(Distance2(closest.X, closest.Y, circlePos.X, circlePos.Y));
            return distance <= radius;
        }

        public static Vector2 ClosestPointOnSegmentToPoint(
            Vector2 segA, Vector2 segB, Vector2 point)
        {
            var segV = segB - segA;
            var pointV = point - segA;
            var segVLen = segV.Length();
            if (segVLen <= 0)
                throw new ArgumentException("Invalid line segment.");
            segV.Normalize();
            var proj = Vector2.Dot(pointV, segV);
            if (proj <= 0)
                return segA;
            if (proj >= segVLen)
                return segB;
            var projV = segV * proj;
            var closest = projV + segA;
            return closest;
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
