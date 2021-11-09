using Bidtellect.Tcf.Models.Components.ConsentString;
using Bidtellect.Tcf.Models.Components.VendorList;

namespace Bidtellect.Tcf.Models
{
    /// <summary>
    /// Represents a TCF TC String (Transparency &amp; Consent String).
    /// </summary>
    public class TcString
    {
        /// <summary>
        /// Gets or sets the Core segment.
        /// </summary>
        public CoreString Core { get; set; }

        /// <summary>
        /// Gets or sets a collection of disclosed Vendors.
        /// </summary>
        public VendorCollection DisclosedVendors { get; set; }

        /// <summary>
        /// Gets or sets transparency and consent for a set of personal
        /// data processing purposes for their own use.
        /// </summary>
        public PublisherTc PublisherTc { get; set; }
    }
}
