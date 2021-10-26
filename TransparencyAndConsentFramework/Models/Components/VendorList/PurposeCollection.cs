using System;
using System.Collections;
using System.Collections.Generic;

namespace Bidtellect.Tcf.Models.Components.VendorList
{
    /// <summary>
    /// Represents a collection of Purposes.
    /// </summary>
    public class PurposeCollection : IEnumerable<KeyValuePair<int, Purpose>>
    {
        protected Dictionary<int, Purpose> purposes;

        /// <summary>
        /// Gets an enumerable collection of the Purpose IDs contained in this collection.
        /// </summary>
        public IEnumerable<int> Ids => purposes.Keys;

        /// <summary>
        /// Gets the number of elements contained in this collection.
        /// </summary>
        public int Count => purposes.Count;

        /// <summary>
        /// Initializes a new instance of <c>PurposeCollection</c>.
        /// </summary>
        public PurposeCollection()
        {
            purposes = new Dictionary<int, Purpose>();
        }

        /// <inheritdoc cref="PurposeCollection.PurposeCollection()"/>
        /// <param name="capacity">
        /// The initial capacity of the collection.
        /// </param>
        public PurposeCollection(int capacity)
        {
            purposes = new Dictionary<int, Purpose>(capacity);
        }

        /// <inheritdoc cref="PurposeCollection.Add(int, Purpose)" />
        public void Add(Purpose purpose)
        {
            Add(purpose.Id, purpose);
        }

        /// <inheritdoc cref="PurposeCollection.Add(int, Purpose)" />
        public void Add(int purposeId)
        {
            Add(purposeId, null);
        }

        /// <summary>
        /// Adds a Purpose to this collection.
        /// </summary>
        /// <param name="purposeId">The ID of the purporse.</param>
        /// <param name="purpose">The Purpose to be added.</param>
        /// <exception cref="ArgumentException" />
        /// <exception cref="ArgumentOutOfRangeException" />
        public void Add(int purposeId, Purpose purpose)
        {
            if (purposeId < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(purposeId));
            }

            if (purpose != null && purpose.Id != purposeId)
            {
                throw new ArgumentException("The ID doesn't match the ID of the Purpose object.", nameof(purposeId));
            }

            purposes[purposeId] = purpose;
        }

        /// <summary>
        /// Removes a Purpose with the given ID from this collection.
        /// </summary>
        /// <param name="purposeId">The ID of the Purpose.</param>
        /// <returns>
        /// A value indicating whether the Purpose was successfully found and removed.
        /// </returns>
        public bool Remove(int purposeId)
        {
            return purposes.Remove(purposeId);
        }

        /// <summary>
        /// Removes a purpose from this collection.
        /// </summary>
        /// <param name="purpose">The Purpose to be removed.</param>
        /// <returns>
        /// A value indicating whether the Purpose was succesfully found and removed.
        /// </returns>
        /// <exception cref="System.ArgumentNullException" />
        public bool Remove(Purpose purpose)
        {
            if (purpose == null)
            {
                throw new ArgumentNullException(nameof(purpose));
            }

            return Remove(purpose.Id);
        }

        /// <summary>
        /// Determines whether the collection contains a Purpose with the given ID.
        /// </summary>
        /// <param name="purposeId">The ID of the Purpose.</param>
        /// <returns>
        /// <c>true</c> if this collection contains a Purpose with the given ID;
        /// otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(int purposeId)
        {
            return purposes.ContainsKey(purposeId);
        }

        /// <summary>
        /// Gets a Purpose with the given ID.
        /// </summary>
        /// <param name="purposeId">The ID of the Purpose.</param>
        /// <param name="purpose">
        /// When this method returns, contains the Purpose,
        /// if the ID is found; otherwise, <c>null</c>.
        /// This parameter is passed uninitialized.
        /// </param>
        /// <returns>
        /// <c>true</c> if this collection contains a Purpose with the given ID;
        /// otherwise, <c>false</c>.
        /// </returns>
        public bool TryGet(int purposeId, out Purpose purpose)
        {
            return purposes.TryGetValue(purposeId, out purpose);
        }

        public IEnumerator<KeyValuePair<int, Purpose>> GetEnumerator()
        {
            return purposes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
