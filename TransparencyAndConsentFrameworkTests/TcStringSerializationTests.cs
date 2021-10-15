using Bidtellect.Tcf.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bidtellect.Tcf.Tests
{
    [TestClass]
    public class TcStringSerializerTests
    {
        [TestMethod]
        public void SerializationRoundTrip()
        {
            var tcString = "COvFyGBOvFyGBAbAAAENAPCAAOAAAAAAAAAAAEEUACCKAAA.IFoEUQQgAIQwgIwQABAEAAAAOIAACAIAAAAQAIAgEAACEAAAAAgAQBAAAAAAAGBAAgAAAAAAAFAAECAAAgAAQARAEQAAAAAJAAIAAgAAAYQEAAAQmAgBC3ZAYzUw";

            var parser = new TcStringParser(null);

            var model = parser.Parse(tcString);

            var serializer = new TcStringSerializer();

            Assert.AreEqual(tcString, serializer.Serialize(model));
        }
    }
}
