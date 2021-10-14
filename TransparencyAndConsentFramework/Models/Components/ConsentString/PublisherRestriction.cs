using Bidtellect.Tcf.Models.Components.VendorList;

namespace Bidtellect.Tcf.Models.Components.ConsentString
{
    /// <summary>
    /// Represents a TC String publisher restriction.
    /// </summary>
    public class PublisherRestriction
    {
        /// <summary>
        /// Gets or sets the type of restriction.
        /// </summary>
        public RestrictionType RestrictionType { get; set; }

        /// <summary>
        /// Gets or sets the Vendor's declared Purpose that the publisher has indicated that they are overriding.
        /// </summary>
        /// <remarks>
        /// <b>Important:</b> This value will be <c>null</c> if the parser does not
        /// have a <c>VendorList</c> when parsing a TC String.
        /// </remarks>
        public Purpose Purpose { get; set; }

        /// <summary>
        /// Gets or sets a collection of vendors which the publisher has designated as restricted under the
        /// Purpose in this <c>PublisherRestriction</c>.
        /// </summary>
        public VendorCollection Vendors { get; set; }
    }
}
