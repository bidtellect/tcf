using System;
using System.Collections.Generic;
using System.IO;

namespace Bidtellect.Tcf.Serialization
{
    /// <summary>
    /// Writes individual bits sequentially using a <c>System.IO.BinaryWriter</c>.
    /// </summary>
    public class BitWriter : IDisposable
    {
        protected BinaryWriter binaryWriter;

        protected byte byteBuffer;
        protected byte bitMask;

        /// <summary>
        /// Gets the current position (in bits, starting at 0).
        /// </summary>
        public int Position { get; protected set; }

        /// <summary>
        /// Initializes a new instance of <c>BitWriter</c>.
        /// </summary>
        /// <param name="binaryWriter">
        /// The binary writer used to write the bits.
        /// </param>
        public BitWriter(BinaryWriter binaryWriter)
        {
            this.binaryWriter = binaryWriter;

            Reset();
        }

        /// <summary>
        /// Writes a boolean value where
        /// <i>true</i> is represented as <c>1</c> and
        /// <i>false</i> is represented as <c>0</c>.
        /// </summary>
        /// <param name="value">The value to write.</param>
        public void Write(bool value)
        {
            if (value)
            {
                byteBuffer |= bitMask;
            }

            AdvancePosition();
        }

        /// <summary>
        /// Writes a series of boolean values where
        /// <i>true</i> is represented as <c>1</c> and
        /// <i>false</i> is represented as <c>0</c>.
        /// </summary>
        /// <param name="values">An enumerable collection of values to write.</param>
        public void Write(IEnumerable<bool> values)
        {
            foreach (var value in values)
            {
                Write(value);
            }
        }

        /// <summary>
        /// Writes an unsigned integer using <i>base2</i> encoding.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <param name="length">The number of bits used to encode the value.</param>
        public void Write(int value, int length)
        {
            Write((uint)value, length);
        }

        /// <summary>
        /// Writes an unsigned integer using <i>base2</i> encoding.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <param name="length">The number of bits used to encode the value.</param>
        public void Write(uint value, int length)
        {
            Write((ulong)value, length);
        }

        /// <summary>
        /// Writes an unsigned long integer using <i>base2</i> encoding.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <param name="length">The number of bits used to encode the value.</param>
        public void Write(long value, int length)
        {
            Write((ulong)value, length);
        }

        /// <summary>
        /// Writes an unsigned long integer using <i>base2</i> encoding.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <param name="length">The number of bits used to encode the value.</param>
        public void Write(ulong value, int length)
        {
            var mask = 1UL << length - 1;

            while (mask > 0)
            {
                Write((value & mask) > 0);

                mask >>= 1;
            }
        }

        /// <summary>
        /// Writes an unsigned integer in Fibonacci encoding.
        /// </summary>
        /// <param name="value">The value to write.</param>
        public void WriteFib(int value)
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            var fib = ComputeFib((uint)value);

            var lastBit = false;

            for (var i = 0; ; i += 1)
            {
                var bit = (fib & 1 << i) > 0;

                Write(bit);

                if (lastBit && bit)
                {
                    break;
                }
                else
                {
                    lastBit = bit;
                }
            }
        }

        /// <summary>
        /// Flushes whatever is in the buffer to the underlying binary writer.
        /// </summary>
        public void Flush()
        {
            binaryWriter.Write(byteBuffer);
        }

        public void Dispose()
        {
            Flush();
        }

        /// <summary>
        /// Resets the <c>bitMask</c> and clears the <c>byteBuffer</c>.
        /// </summary>
        protected void Reset()
        {
            bitMask = 0b1000_0000;
            byteBuffer = 0;
        }

        /// <summary>
        /// Moves the position to the next bit; flushing and reseting if needed.
        /// </summary>
        protected void AdvancePosition()
        {
            bitMask >>= 1;

            if (bitMask <= 0)
            {
                Flush();
                Reset();
            }

            Position += 1;
        }

        protected static uint ComputeFib(uint value)
        {
            var a = 0u;
            var b = 1u;

            var n = 0;

            while (true)
            {
                var sum = a + b;

                if (sum > value)
                {
                    break;
                }

                a = b;
                b = sum;
                n += 1;
            }

            var bitField = 1u << n;

            while (value > 0)
            {
                uint difference;

                n -= 1;

                if (value >= b)
                {
                    bitField |= 1u << n;

                    value -= b;

                    difference = b - a;
                    b = a;
                    a = difference;

                    n -= 1;
                }

                difference = b - a;
                b = a;
                a = difference;
            }

            return bitField;
        }
    }
}
