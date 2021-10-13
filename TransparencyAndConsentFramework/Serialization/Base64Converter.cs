using System;
using System.Text;

namespace Bidtellect.Tcf.Serialization
{
    /// <summary>
    /// Converts byte arrays to base64 encoded strings and vice versa.
    /// </summary>
    public static class Base64Converter
    {
        /// <summary>
        /// Encodes a value using base64 encoding (RFC-4648).
        /// </summary>
        /// <param name="value">A byte array to encode.</param>
        /// <returns>
        /// A base64 encoded string representation of the <c>value</c>.
        /// </returns>
        public static string Encode(byte[] value)
        {
            return Encode(value, 0, value.Length);
        }

        /// <summary>
        /// Encodes a value using base64 encoding (RFC-4648).
        /// </summary>
        /// <param name="value">A byte array to encode.</param>
        /// <param name="offset">A 0-based offset in the <c>value</c>.</param>
        /// <param name="length">The number of bytes to encode.</param>
        /// <returns>
        /// A base64 encoded string representation of the <c>value</c>.
        /// </returns>
        public static string Encode(byte[] value, int offset, int length)
        {
            var builder = new StringBuilder(Convert.ToBase64String(value, offset, length));

            builder.Replace("+", "-");
            builder.Replace("/", "_");

            // Remove padding
            builder.Replace("=", "");

            return builder.ToString();
        }

        /// <summary>
        /// Decodes a value using base64 encoding (RFC-4648).
        /// </summary>
        /// <param name="value">A string encoded in base64.</param>
        /// <returns>
        /// A byte array representation of the <c>value</c>.
        /// </returns>
        public static byte[] Decode(string value)
        {
            var builder = new StringBuilder(value);

            builder.Replace("-", "+");
            builder.Replace("_", "/");

            var remainder = builder.Length % 4;

            // Add padding, if needed
            if (remainder > 0)
            {
                builder.Append('=', 4 - remainder);
            }

            return Convert.FromBase64String(builder.ToString());
        }
    }
}
