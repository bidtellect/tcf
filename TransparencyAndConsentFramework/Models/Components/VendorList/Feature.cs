namespace Bidtellect.Tcf.Models.Components.VendorList
{
    /// <summary>
    /// Represents a GVL Feature.
    /// </summary>
    public class Feature
    {
        /// <summary>
        /// Gets or sets the ID of the feature.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the feature.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the feature.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the legal description of the feature.
        /// </summary>
        public string DescriptionLegal { get; set; }
    }
}
