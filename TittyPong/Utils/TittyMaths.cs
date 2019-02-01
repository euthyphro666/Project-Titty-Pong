using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TittyPong.Utils
{
    public static class TittyMaths
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
    }
}
