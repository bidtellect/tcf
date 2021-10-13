using System;
using Bidtellect.Tcf.Models.Components.VendorList;

namespace Bidtellect.Tcf.Models.Components.ConsentString
{
    public class CoreString
    {
        public int Version { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        public int CmpId { get; set; }
        public int CmpVersion { get; set; }
        public int ConsentScreen { get; set; }
        public string ConsentLanguage { get; set; }
        public int VendorListVersion { get; set; }
        public int PolicyVersion { get; set; }
        public bool IsServiceSpecific { get; set; }
        public bool UsesNonStandardStacks { get; set; }
        public FeatureCollection SpecialFeatureOptIns { get; set; }
        public PurposeCollection PurposesConsents { get; set; }
        public PurposeCollection PurposesLegitimateInterests { get; set; }

        public bool PurposeOneTreatment { get; set; }
        public string PublisherCountryCode { get; set; }

        public VendorCollection VendorConsents { get; set; }
        public VendorCollection VendorLegitimateInterests { get; set; }

        public PublisherRestrictionCollection PublisherRestrictions { get; set; }
    }

}
