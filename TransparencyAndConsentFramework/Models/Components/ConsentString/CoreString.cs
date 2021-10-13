using System;
using Bidtellect.Tcf.Models.Components.VendorList;

namespace Bidtellect.Tcf.Models.Components.ConsentString
{
    /// <summary>
    /// Represents the <i>Core String</i> segment of a TC String.
    /// </summary>
    public class CoreString
    {
        /// <summary>
        /// Gets or sets the version number of the encoding format.
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// Gets or sets the date in which the TC String was first created.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the date in which the TC String was last updated.
        /// </summary>
        public DateTime LastUpdated { get; set; }

        /// <summary>
        /// Gets or sets the Consent Management Platform ID that last updated the TC String.
        /// </summary>
        public int CmpId { get; set; }

        /// <summary>
        /// Gets or sets Consent Management Platform version of the CMP that last updated this TC String.
        /// </summary>
        public int CmpVersion { get; set; }

        /// <summary>
        /// Gets or sets the CMP Screen number at which consent was given for a user with
        /// the CMP that last updated this TC String 
        /// </summary>
        public int ConsentScreen { get; set; }

        /// <summary>
        /// Gets or sets a two-letter code (<c>ISO 639-1</c>) indicating the language in which the CMP UI
        /// was presented to the user.
        /// </summary>
        public string ConsentLanguage { get; set; }

        /// <summary>
        /// Gets or sets the version of the GVL used to create the TC String.
        /// </summary>
        public int VendorListVersion { get; set; }

        /// <summary>
        /// Gets or sets the version of the policy used within the GVL.
        /// </summary>
        /// <remarks>
        /// From the corresponding field in the GVL that was used for obtaining consent.
        /// A new policy version invalidates existing strings and requires CMPs to re-establish
        /// transparency and consent from users. 
        /// </remarks>
        public int PolicyVersion { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the service is specific.
        /// </summary>
        /// <remarks>
        /// This property must always have the value of <c>true</c>.
        /// When a Vendor encounters a TC String with <c>IsServiceSpecific=0</c> then it is considered invalid.
        /// </remarks>
        public bool IsServiceSpecific { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the CMP used non-IAB standard stacks during consent gathering.
        /// </summary>
        /// <remarks>
        /// Setting this to <c>true</c> means that a publisher-run CMP – that is still IAB Europe registered – is
        /// using customized Stack descriptions and not the standard stack descriptions defined in the Policies.
        /// A CMP that services multiple publishers sets this value to <c>false</c>. 
        /// </remarks>
        public bool UsesNonStandardStacks { get; set; }

        /// <summary>
        /// Gets or sets a collection of special feature opt-ins.
        /// </summary>
        /// <remarks>
        /// The TCF Policies designates certain Features as <i>special</i> which means a CMP must afford the user
        /// a means to opt in to their use. These <i>Special Features</i> are published and numerically identified
        /// in the Global Vendor List separately from normal Features.
        /// </remarks>
        public FeatureCollection SpecialFeatureOptIns { get; set; }

        /// <summary>
        /// Gets or sets a collection of purposes established on the legal basis of consent.
        /// </summary>
        /// <remarks>
        /// <b>Important:</b> Special Purposes are a different ID space and not included in this collection.
        /// </remarks>
        public PurposeCollection PurposesConsents { get; set; }

        /// <summary>
        /// Gets or sets a collection of purposes where a legitimate interest was established.
        /// </summary>
        /// <remarks>
        /// The Purpose's transparency requirements are met for each Purpose on the legal basis of legitimate
        /// interest and the user has not exercised their <i>Right to Object</i> to that Purpose.
        /// </remarks>
        public PurposeCollection PurposesLegitimateInterests { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Purpose <c>1</c> was <b>NOT</b> disclosed.
        /// <para>
        /// <c>true</c> indicates that Purpose <c>1</c> was not disclosed at all.
        /// </para>
        /// <para>
        /// <c>false</c> indicates that Purpose <c>1</c> was disclosed commonly as consent as expected by the Policies.
        /// </para>
        /// </summary>
        public bool PurposeOneTreatment { get; set; }

        /// <summary>
        /// Gets or sets the country code (<c>ISO 3166-1 alpha-2</c>) of the country that determines legislation of reference.
        /// </summary>
        /// <remarks>
        /// Commonly, this corresponds to the country in which the publisher's business entity is established.
        /// </remarks>
        public string PublisherCountryCode { get; set; }

        /// <summary>
        /// Gets or sets a collection of vendors for which consent has been established.
        /// </summary>
        public VendorCollection VendorConsents { get; set; }

        /// <summary>
        /// Gets or sets a collection of vendors for which a legitimate interest has been established.
        /// </summary>
        public VendorCollection VendorLegitimateInterests { get; set; }

        /// <summary>
        /// Gets or sets a collection of publisher restrictions.
        /// </summary>
        public PublisherRestrictionCollection PublisherRestrictions { get; set; }
    }

}
