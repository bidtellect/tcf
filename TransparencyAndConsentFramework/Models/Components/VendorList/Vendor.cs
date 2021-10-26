using System;

namespace Bidtellect.Tcf.Models.Components.VendorList
{
    /// <summary>
    /// Represents a GVL vendor.
    /// </summary>
    public class Vendor
    {
        /// <summary>
        /// Gets or sets the ID of the vendor.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the vendor.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a collection of purposes.
        /// </summary>
        /// <remarks>
        /// These purposes are declared as performed on the legal basis of consent.
        /// </remarks>
        public PurposeCollection Purposes { get; set; }

        /// <summary>
        /// Gets or sets a collection of special purposes.
        /// </summary>
        /// <remarks>
        /// These purposes are declared as performed on the legal basis of a legitimate interest.
        /// </remarks>
        public PurposeCollection SpecialPurposes { get; set; }

        /// <summary>
        /// Gets or sets a collection of legitimate-interest purposes.
        /// </summary>
        /// <remarks>
        /// These purposes are declared as performed on the legal basis of legitimate interest.
        /// </remarks>
        public PurposeCollection LegitimateInterestPurposes { get; set; }

        /// <summary>
        /// Gets or sets a collection of flexible purposes.
        /// </summary>
        /// <remarks>
        /// These are purposes where the vendor is flexible regarding the legal basis;
        /// they will perform the processing based on consent or a legitimate interest.
        /// The <i>default</i> is determined by which of the other two mutually-exclusive
        /// purpose fields is used to declare the purpose for the vendor.
        /// </remarks>
        public PurposeCollection FlexiblePurposes { get; set; }

        /// <summary>
        /// Gets or sets a collection of features.
        /// </summary>
        /// <remarks>
        /// These are features which the vendor may utilize when performing some declared Purposes processing.
        /// </remarks>
        public FeatureCollection Features { get; set; }

        /// <summary>
        /// Gets or sets a collection of special features.
        /// </summary>
        /// <remarks>
        /// These are features which the vendor may utilize when performing some declared Purposes processing.
        /// </remarks>
        public FeatureCollection SpecialFeatures { get; set; }

        /// <summary>
        /// Gets or sets the URL to the vendor's privacy policy document.
        /// </summary>
        public string PolicyUrl { get; set; }

        /// <summary>
        /// Gets or sets the date when the vendor was deleted.
        /// If it has a value, vendor is considered deleted after this date/time and <b>must not</b> be
        /// established to users.
        /// </summary>
        public DateTime? DeletedDate { get; set; }

        /// <summary>
        /// Gets or sets the vendor's overflow options.
        /// </summary>
        public OverflowOptions Overflow { get; set; }

        public class OverflowOptions
        {
            /// <summary>
            /// Gets or sets the vendor's HTTP GET request length limit.
            /// </summary>
            /// <remarks>
            /// Supported options are: <c>32</c>, <c>128</c>.
            /// </remarks>
            public int HttpGetLimit { get; set; }
        }
    }
}
