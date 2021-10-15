using System;
using Bidtellect.Tcf.Models.Components.VendorList;

namespace Bidtellect.Tcf.Models
{
    /// <summary>
    /// Represents a Global Vendor List.
    /// </summary>
    /// <remarks>
    /// The Global Vendor List (GVL) is a technical document that CMPs download from a
    /// domain managed and published by IAB Europe. It lists all registered and approved
    /// Vendors, as well as standard Purposes, Special Purposes, Features, Special Features
    /// and Stacks. The information stored in the GVL is used for determining what legal
    /// disclosures must be made to the user.
    /// </remarks>
    public class VendorList
    {
        /// <summary>
        /// Gets or sets the Global Vendor List Specification Version.
        /// </summary>
        public int GvlSpecificationVersion { get; set; }

        /// <summary>
        /// Gets or sets the Global Vendor List version.
        /// </summary>
        /// <remarks>
        /// This value is incremented with each published file change.
        /// </remarks>
        public int VendorListVersion { get; set; }

        /// <summary>
        /// Gets or sets TCF Policy Version.
        /// </summary>
        /// <remarks>
        /// The TCF MO will increment this value whenever a GVL change (such as adding a
        /// new Purpose or Feature or a change in Purpose wording) legally invalidates
        /// existing TC Strings and requires CMPs to re-establish transparency and consent
        /// from users. TCF Policy changes should be relatively infrequent and only occur
        /// when necessary to support changes in global mandate. If the policy version number
        /// in the latest GVL is different from the value in your TC String, then you need
        /// to re-establish transparency and consent for that user. A version <c>1</c> format
        /// TC String is considered to have a version value of <c>1</c>.
        /// </remarks>
        public int TcfPolicyVersion { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the GVL was last updated.
        /// </summary>
        public DateTime LastUpdated { get; set; }

        /// <summary>
        /// Gets or sets a collection of standard Purposes.
        /// </summary>
        public PurposeCollection Purposes { get; set; }

        /// <summary>
        /// Gets or sets a collection of Special Purposes.
        /// </summary>
        public PurposeCollection SpecialPurposes { get; set; }

        /// <summary>
        /// Gets or sets a collection of standard Features.
        /// </summary>
        public FeatureCollection Features { get; set; }

        /// <summary>
        /// Gets or sets a collection of Special Features.
        /// </summary>
        public FeatureCollection SpecialFeatures { get; set; }

        /// <summary>
        /// Gets or sets a collection of Stacks.
        /// </summary>
        public StackCollection Stacks { get; set; }

        /// <summary>
        /// Gets or sets a collection of Vendors.
        /// </summary>
        public VendorCollection Vendors { get; set; }
    }
}
