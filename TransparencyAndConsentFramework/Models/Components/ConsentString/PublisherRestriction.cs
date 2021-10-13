using Bidtellect.Tcf.Models.Components.VendorList;

namespace Bidtellect.Tcf.Models.Components.ConsentString
{
    public class PublisherRestriction
    {
        public RestrictionType RestrictionType { get; set; }

        /// <summary>
        /// Gets or sets the purpose.
        /// </summary>
        public Purpose Purpose { get; set; }

        /// <summary>
        /// Gets or sets the vendors.
        /// </summary>
        public VendorCollection Vendors { get; set; }
    }
}
