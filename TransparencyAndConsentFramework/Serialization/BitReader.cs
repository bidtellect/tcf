using System;

namespace Bidtellect.Tcf.Serialization
{
    /// <summary>
    /// Reads individual bits sequentially from a byte array.
    /// </summary>
    public class BitReader
    {
        protected byte[] byteArray;
        protected int byteIndex = 0;

        protected byte byteBuffer;
        protected byte bitMask;

        /// <summary>
        /// Initializes a new instance of BitReader.
        /// </summary>
        /// <param name="byteArray">The array from which to read.</param>
        public BitReader(byte[] byteArray)
        {
            this.byteArray = byteArray;

            Reset();
        }

        /// <summary>
        /// Reads a single bit as a boolean value where
        /// <i>true</i> represents <c>1</c> and
        /// <i>false</i> represents <c>0</c>.
        /// </summary>
        public bool ReadBit()
        {
            var value = (byteBuffer & bitMask) > 0;

            bitMask >>= 1;

            if (bitMask <= 0)
            {
                byteIndex += 1;
                Reset();
            }

            return value;
        }

        /// <summary>
        /// Reads multiple bits as boolean values where
        /// <i>true</i> represents <c>1</c> and
        /// <i>false</i> represents <c>0</c>.
        /// </summary>
        /// <param name="array">An array of boolean values into which the bits will be read.</param>
        /// <param name="offset">An offset (0-based) from which to begin storing the bits read.</param>
        /// <param name="length">The maximum number of bits to be read.</param>
        public void ReadBits(bool[] array, int offset, int length)
        {
            for (var i = offset; i < length; i += 1)
            {
                array[i] = ReadBit();
            }
        }

        /// <summary>
        /// Reads multiple bits as boolean values where
        /// <i>true</i> represents <c>1</c> and
        /// <i>false</i> represents <c>0</c>.
        /// </summary>
        /// <param name="array">An array of boolean values into which the bits will be read.</param>
        public void ReadBits(bool[] array)
        {
            ReadBits(array, 0, array.Length);
        }

        /// <summary>
        /// Reads multiple bits as boolean values where
        /// <i>true</i> represents <c>1</c> and
        /// <i>false</i> represents <c>0</c>.
        /// </summary>
        /// <param name="length">The maximum number of bits to be read.</param>
        public bool[] ReadBits(int length)
        {
            var array = new bool[length];

            ReadBits(array);

            return array;
        }

        /// <summary>
        /// Reads a <i>base2</i> encoded signed integer.
        /// </summary>
        /// <param name="length">The maximum number of bits to be read.</param>
        public int ReadInt(int length)
        {
            if (length > 32 || length <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }

            return (int)ReadLong(length);
        }

        /// <summary>
        /// Reads a <i>base2</i> encoded signed long integer.
        /// </summary>
        /// <param name="length">The maximum number of bits to be read.</param>
        public long ReadLong(int length)
        {
            if (length > 64 || length <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }

            var value = 0L;

            if (ReadBit())
            {
                value |= 1;
            }

            for (var i = 1; i < length; i += 1)
            {
                value <<= 1;

                if (ReadBit())
                {
                    value |= 1;
                }
            }

            return value;
        }

        /// <summary>
        /// Reads a Fibonacci encoded unsigned integer.
        /// </summary>
        public long ReadFib()
        {
            var a = 0L;
            var b = 1L;

            var accumulator = 0L;

            var lastBit = false;

            while (true)
            {
                var sum = a + b;

                if (ReadBit())
                {
                    // If there are two 1s in a row, we stop.
                    if (lastBit)
                    {
                        break;
                    }

                    accumulator += sum;
                    lastBit = true;
                }
                else
                {
                    lastBit = false;
                }

                a = b;
                b = sum;
            }

            return accumulator;
        }

        /// <summary>
        /// Resets the <c>bitMask</c> and gets the next byte into the <c>byteBuffer</c>.
        /// The <c>byteBuffer</c> will be set to <c>0</c> if there are no more bytes to read.
        /// </summary>
        protected void Reset()
        {
            bitMask = 0b1000_0000;

            if (byteIndex < byteArray.Length)
            {
                byteBuffer = byteArray[byteIndex];
            }
            else
            {
                byteBuffer = 0;
            }
        }
    }
}
