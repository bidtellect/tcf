using Bidtellect.Tcf.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bidtellect.Tcf.Tests
{
    [TestClass]
    public class GvlClientTests
    {
        [TestMethod]
        public void FetchVendorList()
        {
            var client = new GvlClient();

            var gvl = client.Fetch();

            Assert.IsNotNull(gvl);
            Assert.IsTrue(gvl.Purposes.Count > 0);
            Assert.IsTrue(gvl.SpecialPurposes.Count > 0);
            Assert.IsTrue(gvl.Features.Count > 0);
            Assert.IsTrue(gvl.SpecialFeatures.Count > 0);
            Assert.IsTrue(gvl.Stacks.Count > 0);
            Assert.IsTrue(gvl.Vendors.Count > 0);
        }
    }
}
