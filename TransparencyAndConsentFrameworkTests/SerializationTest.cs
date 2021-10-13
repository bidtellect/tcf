using Bidtellect.Tcf.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bidtellect.Tcf.Tests
{
    [TestClass]
    public class SerializationTest
    {
        [TestMethod]
        public void RoundTrip()
        {
            byte[] message;

            using (var stream = new System.IO.MemoryStream())
            {
                using (var binaryWriter = new System.IO.BinaryWriter(stream))
                using (var bitWriter = new BitWriter(binaryWriter))
                {
                    bitWriter.Write(true);
                    bitWriter.Write(32, 8);
                    bitWriter.WriteFib(21);
                }

                message = stream.ToArray();
            }

            var bitReader = new BitReader(message);

            Assert.AreEqual(true, bitReader.ReadBit());
            Assert.AreEqual(32, bitReader.ReadInt(8));
            Assert.AreEqual(21, bitReader.ReadFib());
        }
    }
}
