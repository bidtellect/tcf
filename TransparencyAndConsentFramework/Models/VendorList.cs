using System;
using Bidtellect.Tcf.Models.Components.VendorList;

namespace Bidtellect.Tcf.Models
{
    public class VendorList
    {
        public int GvlSpecificationVersion { get; set; }
        public int VendorListVersion { get; set; }
        public int TcfPolicyVersion { get; set; }
        public DateTime LastUpdated { get; set; }

        public PurposeCollection Purposes { get; set; }
        public PurposeCollection SpecialPurposes { get; set; }
        public FeatureCollection Features { get; set; }
        public FeatureCollection SpecialFeatures { get; set; }
        public StackCollection Stacks { get; set; }
        public VendorCollection Vendors { get; set; }
    }
}
