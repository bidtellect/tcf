namespace Bidtellect.Tcf.Models.Components.ConsentString
{
    /// <summary>
    /// Specifies constants that define a publisher restriction.
    /// </summary>
    public enum RestrictionType
    {
        /// <summary>
        /// Purpose Flatly Not Allowed by Publisher (regardless of Vendor declarations).
        /// </summary>
        PurposeFlatlyNotAllowedByPublisher = 0,

        /// <summary>
        /// Require Consent (if Vendor has declared the Purpose IDs legal basis as Legitimate Interest and flexible).
        /// </summary>
        RequireConsent = 1,

        /// <summary>
        /// Require Legitimate Interest (if Vendor has declared the Purpose IDs legal basis as Consent and flexible).
        /// </summary>
        RequireLegitimateInterest = 2,

        /// <summary>
        /// UNDEFINED (not used).
        /// </summary>
        Undefined = 3,
    }
}
