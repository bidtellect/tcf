using Bidtellect.Tcf.Models.Components.VendorList;

namespace Bidtellect.Tcf.Models.Components.ConsentString
{
    public class PublisherTc
    {
        public PurposeCollection PurposeConsents { get; set; }
        public PurposeCollection PurposeLegitimateInterests { get; set; }
        public PurposeCollection CustomPurposeConsents { get; set; }
        public PurposeCollection CustomPurposesLITransparency { get; set; }
    }
}
