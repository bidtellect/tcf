using System;
using Bidtellect.Tcf.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Bidtellect.Tcf.Tests
{
    [TestClass]
    public class TcStringParserTests
    {
        [TestMethod]
        public void CanParseExampleString()
        {
            var tcString = "COvFyGBOvFyGBAbAAAENAPCAAOAAAAAAAAAAAEEUACCKAAA.IFoEUQQgAIQwgIwQABAEAAAAOIAACAIAAAAQAIAgEAACEAAAAAgAQBAAAAAAAGBAAgAAAAAAAFAAECAAAgAAQARAEQAAAAAJAAIAAgAAAYQEAAAQmAgBC3ZAYzUw";

            var parser = new TcStringParser(null);

            var model = parser.Parse(tcString);

            // Core String
            Assert.AreEqual(2, model.Core.Version);
            Assert.IsTrue((model.Core.Created - new DateTime(2020, 2, 20, 23, 57, 39)).TotalMilliseconds < 500, "Dates are not within 500ms margin of error.");
            Assert.IsTrue((model.Core.LastUpdated - new DateTime(2020, 2, 20, 23, 57, 39)).TotalMilliseconds < 500, "Dates are not within 500ms margin of error.");
            Assert.AreEqual(27, model.Core.CmpId);
            Assert.AreEqual(0, model.Core.CmpVersion);
            Assert.AreEqual(0, model.Core.ConsentScreen);
            Assert.AreEqual("EN", model.Core.ConsentLanguage, true);
            Assert.AreEqual(15, model.Core.VendorListVersion);
            Assert.AreEqual(2, model.Core.PolicyVersion);
            Assert.AreEqual(false, model.Core.IsServiceSpecific);
            Assert.AreEqual(false, model.Core.UsesNonStandardStacks);
            Assert.AreEqual(0, model.Core.SpecialFeatureOptIns.Count);
            Assert.AreEqual(3, model.Core.PurposesConsents.Count);
            Assert.AreEqual(0, model.Core.PurposesLegitimateInterests.Count);
            Assert.AreEqual(false, model.Core.PurposeOneTreatment);
            Assert.AreEqual("AA", model.Core.PublisherCountryCode, true);
            Assert.AreEqual(3, model.Core.VendorConsents.Count);
            Assert.AreEqual(3, model.Core.VendorLegitimateInterests.Count);
            Assert.AreEqual(0, model.Core.PublisherRestrictions.Count);

            var purposeConsents = new HashSet<int>(new[] { 1, 2, 3 });
            for (var i = 1; i < 24; i += 1)
            {
                Assert.AreEqual(purposeConsents.Contains(i), model.Core.PurposesConsents.Contains(i));
            }

            var vendorConsents = new HashSet<int>(new[] { 2, 6, 8 });
            for (var i = 1; i < 24; i += 1)
            {
                Assert.AreEqual(vendorConsents.Contains(i), model.Core.VendorConsents.Contains(i));
            }

            var vendorLegitimateInterests = new HashSet<int>(new[] { 2, 6, 8 });
            for (var i = 1; i < 24; i += 1)
            {
                Assert.AreEqual(vendorLegitimateInterests.Contains(i), model.Core.VendorLegitimateInterests.Contains(i));
            }

            // Disclosed Vendors
            Assert.AreEqual(79, model.DisclosedVendors.Count);

            var disclosedVendors = new HashSet<int>(new[] { 2, 6, 8, 12, 18, 23, 37, 42, 47, 48, 53, 61, 65, 66, 72, 88, 98, 127, 128, 129, 133, 153, 163, 192, 205, 215, 224, 243, 248, 281, 294, 304, 350, 351, 358, 371, 422, 424, 440, 447, 467, 486, 498, 502, 512, 516, 553, 556, 571, 587, 612, 613, 618, 626, 648, 653, 656, 657, 665, 676, 681, 683, 684, 686, 687, 688, 690, 691, 694, 702, 703, 707, 708, 711, 712, 714, 716, 719, 720 });
            for (var i = 1; i < 1000; i += 1)
            {
                Assert.AreEqual(disclosedVendors.Contains(i), model.DisclosedVendors.Contains(i));
            }

            // Publisher Restrictions
            Assert.AreEqual(null, model.PublisherTc);
        }
    }
}
