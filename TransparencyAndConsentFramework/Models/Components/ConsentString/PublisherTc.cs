using Bidtellect.Tcf.Models.Components.VendorList;

namespace Bidtellect.Tcf.Models.Components.ConsentString
{
    /// <summary>
    /// Represents the <i>Publisher TC</i> segment of a TC String.
    /// </summary>
    public class PublisherTc
    {
        /// <summary>
        /// Gets or sets a collection of Purposes established on the legal basis of consent.
        /// </summary>
        public PurposeCollection PurposeConsents { get; set; }

        /// <summary>
        /// Gets or sets a collection of Purposes established on the legal basis of legitimate interest.
        /// </summary>
        public PurposeCollection PurposeLegitimateInterests { get; set; }

        /// <summary>
        /// Gets or sets a collection of Custom Purposes established on the legal basis of consent.
        /// </summary>
        public PurposeCollection CustomPurposeConsents { get; set; }

        /// <summary>
        /// Gets or sets a collection of Custom Purposes established on the legal basis of legitimate interest.
        /// </summary>
        public PurposeCollection CustomPurposeLegitimateInterests { get; set; }
    }
}
