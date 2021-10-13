namespace Bidtellect.Tcf.Models.Components.VendorList
{
    /// <summary>
    /// Represents a GVL Purpose.
    /// </summary>
    public class Purpose
    {
        /// <summary>
        /// Gets or sets the ID of the purpose.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the purpose.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the purpose.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the legal description of the purpose.
        /// </summary>
        public string DescriptionLegal { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether CMPs should afford users
        /// the means to provide an opt-in consent choice.
        /// </summary>
        /// <remarks>
        /// Default is <c>true</c>.
        /// </remarks>
        public bool Consentable { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether CMPs should afford users
        /// the means to exercise a right to object.
        /// </summary>
        /// <remarks>
        /// Default is <c>true</c>.
        /// </remarks>
        public bool RightToObject { get; set; } = true;
    }
}
