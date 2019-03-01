using Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.IO
{
    public struct Input
    {
        public static readonly float OFFSET = 128;

        public byte Value;

        public Input(int value)
        {
            Value = Maths.Clamp(value + 128);
        }

        public static implicit operator Input(int value)
        {
            return new Input(value);
        }

        public static implicit operator byte(Input input)
        {
            return input.Value;
        }

        public static implicit operator float(Input input)
        {
            return input.Value - OFFSET;
        }

        public static float operator +(float left, Input input)
        {
            return left + input.Value - OFFSET;
        }

    }
}
