using Bidtellect.Tcf.Models.Components.ConsentString;
using Bidtellect.Tcf.Models.Components.VendorList;

namespace Bidtellect.Tcf.Models
{
    public class TcString
    {
        public CoreString Core { get; set; }
        public VendorCollection DisclosedVendors { get; set; }
        public VendorCollection AllowedVendors { get; set; }
        public PublisherTc PublisherTc { get; set; }
    }
}
