using System.Collections;
using System.Collections.Generic;

namespace Bidtellect.Tcf.Models.Components.ConsentString
{
    /// <summary>
    /// Represents a collection a Publisher Restrictions index by Purpose ID.
    /// </summary>
    public class PublisherRestrictionCollection : IEnumerable<KeyValuePair<int, PublisherRestriction>>    {
        protected Dictionary<int, PublisherRestriction> publisherRestrictions;

        /// <summary>
        /// Gets the number of elements contained in this collection.
        /// </summary>
        public int Count => publisherRestrictions.Count;

        /// <summary>
        /// Initializes a new instance of <c>PublisherRestrictionCollection</c>.
        /// </summary>
        public PublisherRestrictionCollection()
        {
            publisherRestrictions = new Dictionary<int, PublisherRestriction>();
        }

        /// <inheritdoc cref="PublisherRestrictionCollection.PublisherRestrictionCollection()"/>
        /// <param name="capacity">The initial capacity of the collection.</param>
        public PublisherRestrictionCollection(int capacity)
        {
            publisherRestrictions = new Dictionary<int, PublisherRestriction>(capacity);
        }

        /// <summary>
        /// Adds a Publisher Restriction for a given Purpose ID to this collection.
        /// </summary>
        /// <param name="purposeId">The ID of the Purpose.</param>
        /// <param name="publisherRestriction">The Publisher Restriction.</param>
        /// <remarks>
        /// Adding a Publisher Restriction for a Purpose ID that is already contained in the collection
        /// will result in that value being overwritten.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException"/>
        public void Add(int purposeId, PublisherRestriction publisherRestriction)
        {
            if (publisherRestriction == null)
            {
                throw new System.ArgumentNullException(nameof(publisherRestriction));
            }

            publisherRestrictions[purposeId] = publisherRestriction;
        }

        /// <summary>
        /// Removes a Publisher Restriction for a given Purpose ID from this collection.
        /// </summary>
        /// <param name="purposeId">The ID of the Purpose.</param>
        /// <returns>
        /// A value indicating whether the Publisher Restriction was successfully found and removed.
        /// </returns>
        public bool Remove(int purposeId)
        {
            return publisherRestrictions.Remove(purposeId);
        }

        /// <summary>
        /// Determines whether the collection contains a Publisher Restriction for a given Purpose ID.
        /// </summary>
        /// <param name="purposeId">The ID of the purpose.</param>
        /// <returns>
        /// <c>true</c> if this collection contains a Publisher Restriction for the given Purpose ID; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(int purposeId)
        {
            return publisherRestrictions.ContainsKey(purposeId);
        }

        /// <summary>
        /// Gets the Publisher Restriction for the given Purpose ID.
        /// </summary>
        /// <param name="purposeId">The ID of the purpose.</param>
        /// <param name="publisherRestriction">
        /// When this method returns, contains the Publisher Restriction for the Purpose ID,
        /// if the Purpose is found; otherwise, <c>null</c>.
        /// This parameter is passed uninitialized.
        /// </param>
        /// <returns>
        /// <c>true</c> if this collection contains a Publisher Restriction for the given Purpose ID; otherwise, <c>false</c>.
        /// </returns>
        public bool TryGet(int purposeId, out PublisherRestriction publisherRestriction)
        {
            return publisherRestrictions.TryGetValue(purposeId, out publisherRestriction);
        }

        public IEnumerator<KeyValuePair<int, PublisherRestriction>> GetEnumerator()
        {
            return publisherRestrictions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
