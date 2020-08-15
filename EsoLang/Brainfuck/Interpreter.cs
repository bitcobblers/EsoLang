using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

namespace EsoLang.Brainfuck
{
    public abstract class Interpreter
    {
        public abstract string Delimiter { get; }
        public abstract Dictionary<string, Op> Map { get; }

        public abstract IEnumerable<Op> Tokenize(string source);

        public virtual string Stringify(string text)
        {
            const int MemorySize = 10;

            if(string.IsNullOrWhiteSpace(text))
            {
                return string.Empty;
            }

            var buffer = new byte[MemorySize];
            var pointer = 0;
            var bytes = UnicodeEncoding.UTF8.GetBytes(text);
            var emitted = new List<Op>();

            foreach(var b in bytes)
            {
                var nearest = GetNearest(buffer, b);

                emitted.AddRange(EmitShift(pointer, nearest, Op.IncPointer, Op.DecPointer));
                emitted.AddRange(EmitShift(buffer[nearest], b, Op.IncValue, Op.DecValue));

                pointer = nearest;
                buffer[nearest] = b;
            }

            return string.Empty;
        }

        internal static IEnumerable<Op> EmitShift(int from, int to, Op inc, Op dec)
        {
            if(to>from)
            {
                return Enumerable.Range(0, to - from).Select(_ => Op.IncPointer);
            }
            else if(to<from)
            {
                return Enumerable.Range(0, from - to).Select(_ => Op.DecPointer);
            }
            else
            {
                return new Op[] { };
            }
        }

        internal static int GetSign(int value) => value < 0 ? -1 : value == 0 ? 0 : 1;

        internal static int GetNearest(byte[] buffer, byte value)
        {
            int smallestIndex = 0;
            int smallestDelta = Math.Abs(buffer[0] - value);

            for (int i = 1; i < buffer.Length; i++)
            {
                int delta = Math.Abs(buffer[i] - value);

                if (delta < smallestDelta)
                {
                    smallestDelta = delta;
                    smallestIndex = i;
                }
            }

            return smallestIndex;
        }
    }
}