using System;
using System.Collections;
using System.Collections.Generic;

namespace Bidtellect.Tcf.Models.Components.VendorList
{
    /// <summary>
    /// Represents a collection of Vendors.
    /// </summary>
    public class VendorCollection : IEnumerable<KeyValuePair<int, Vendor>>
    {
        protected Dictionary<int, Vendor> vendors = new Dictionary<int, Vendor>();

        /// <summary>
        /// Gets an enumerable collection of the Vendor IDs contained in this collection.
        /// </summary>
        public IEnumerable<int> Ids => vendors.Keys;

        /// <summary>
        /// Gets the number of elements contained in this collection.
        /// </summary>
        public int Count => vendors.Count;

        /// <summary>
        /// Initializes a new instance of <c>VendorCollection</c>.
        /// </summary>
        public VendorCollection()
        {
            vendors = new Dictionary<int, Vendor>();
        }

        /// <inheritdoc cref="VendorCollection.VendorCollection()" />
        /// <param name="capacity">The initial capacity of the collection.</param>
        public VendorCollection(int capacity)
        {
            vendors = new Dictionary<int, Vendor>(capacity);
        }

        /// <inheritdoc cref="VendorCollection.Add(int, Vendor)"/>
        public void Add(Vendor vendor)
        {
            if (vendor == null)
            {
                throw new ArgumentNullException(nameof(vendor));
            }

            Add(vendor.Id, vendor);
        }

        /// <inheritdoc cref="VendorCollection.Add(int, Vendor)"/>
        public void Add(int vendorId)
        {
            Add(vendorId, null);
        }

        /// <summary>
        /// Adds a Vendor to this collection.
        /// </summary>
        /// <param name="vendorId">The ID of the Vendor.</param>
        /// <param name="vendor">The Vendor to be added.</param>
        /// <exception cref="ArgumentException" />
        /// <exception cref="ArgumentOutOfRangeException" />
        public void Add(int vendorId, Vendor vendor)
        {
            if (vendorId < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(vendorId));
            }

            if (vendor != null && vendor.Id != vendorId)
            {
                throw new ArgumentException("The ID doesn't match the ID of the Vendor object.", nameof(vendorId));
            }

            vendors[vendorId] = vendor;
        }

        /// <summary>
        /// Removes a Vendor with the given Vendor ID from this collection.
        /// </summary>
        /// <param name="vendorId">The ID of the Vendor.</param>
        /// <returns>
        /// A value indicating whether the Vendor was successfully found and removed.
        /// </returns>
        public bool Remove(int vendorId)
        {
            return vendors.Remove(vendorId);
        }

        /// <summary>
        /// Removes a Vendor from this collection.
        /// </summary>
        /// <param name="vendor">The Vendor to be removed.</param>
        /// <returns>
        /// A value indicating whether the Vendor was successfully found and removed.
        /// </returns>
        /// <exception cref="System.ArgumentNullException"/>
        public bool Remove(Vendor vendor)
        {
            if (vendor == null)
            {
                throw new ArgumentNullException(nameof(vendor));
            }

            return Remove(vendor.Id);
        }

        /// <summary>
        /// Determines whether the collection contains a Vendor with the given ID.
        /// </summary>
        /// <param name="vendorId">The ID of the Vendor.</param>
        /// <returns>
        /// <c>true</c> if this collection contains a Vendor with the given ID;
        /// otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(int vendorId)
        {
            return vendors.ContainsKey(vendorId);
        }

        /// <summary>
        /// Gets a Vendor with the given ID.
        /// </summary>
        /// <param name="vendorId">The ID of the Vendor.</param>
        /// <param name="vendor">
        /// When this method returns, contains the Vendor,
        /// if the ID is found; otherwise, <c>null</c>.
        /// This parameter is passed uninitialized.
        /// </param>
        /// <returns>
        /// <c>true</c> if this collection contains a Vendor with the given ID;
        /// otherwise, <c>false</c>.
        /// </returns>
        public bool TryGet(int vendorId, out Vendor vendor)
        {
            return vendors.TryGetValue(vendorId, out vendor);
        }

        public IEnumerator<KeyValuePair<int, Vendor>> GetEnumerator()
        {
            return vendors.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
