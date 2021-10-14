namespace Bidtellect.Tcf.Models.Components.VendorList
{
    /// <summary>
    /// Represents a GVL Stack.
    /// </summary>
    public class Stack
    {
        /// <summary>
        /// Gets or sets the ID of the stack.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a collection of purposes.
        /// </summary>
        public PurposeCollection Purposes { get; set; }

        /// <summary>
        /// Gets or sets a collection of special features.
        /// </summary>
        /// <remarks>
        /// Special features differ from simple features in that CMPs <b>must</b> provide
        /// users with a means to signal an opt-in choice as to whether vendors
        /// may employ the feature when performing any purpose processing.
        /// </remarks>
        public FeatureCollection SpecialFeatures { get; set; }

        /// <summary>
        /// Gets or sets the name of the stack.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the stack.
        /// </summary>
        public string Description { get; set; }
    }
}
