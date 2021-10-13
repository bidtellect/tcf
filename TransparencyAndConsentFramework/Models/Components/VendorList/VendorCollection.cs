using System;
using System.Collections;
using System.Collections.Generic;

namespace Bidtellect.Tcf.Models.Components.VendorList
{
    public class VendorCollection : IEnumerable<Vendor>
    {
        protected Dictionary<int, Vendor> vendors = new();

        public int Count => vendors.Count;

        public void Add(Vendor vendor)
        {
            Add(vendor.Id, vendor);
        }

        public void Add(int vendorId)
        {
            Add(vendorId, null);
        }

        public void Add(int vendorId, Vendor vendor)
        {
            vendors[vendorId] = vendor;
        }

        public bool Remove(int vendorId)
        {
            return vendors.Remove(vendorId);
        }

        public bool Remove(int vendorId, out Vendor vendor)
        {
            return vendors.Remove(vendorId, out vendor);
        }

        public bool Remove(Vendor vendor)
        {
            if (vendor == null)
            {
                throw new ArgumentNullException(nameof(vendor));
            }

            return Remove(vendor.Id);
        }

        public bool Contains(int vendorId)
        {
            return vendors.ContainsKey(vendorId);
        }

        public bool TryGet(int vendorId, out Vendor vendor)
        {
            return vendors.TryGetValue(vendorId, out vendor);
        }

        public IEnumerator<Vendor> GetEnumerator()
        {
            return vendors.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
