using System;
using System.Collections;
using System.Collections.Generic;

namespace Bidtellect.Tcf.Models.Components.VendorList
{
    public class PurposeCollection : IEnumerable<Purpose>
    {
        protected Dictionary<int, Purpose> purposes = new();

        public int Count => purposes.Count;

        public void Add(Purpose purpose)
        {
            Add(purpose.Id, purpose);
        }

        public void Add(int purposeId)
        {
            Add(purposeId, null);
        }

        public void Add(int purposeId, Purpose purpose)
        {
            purposes[purposeId] = purpose;
        }

        public bool Remove(int purposeId)
        {
            return purposes.Remove(purposeId);
        }

        public bool Remove(int purposeId, out Purpose purpose)
        {
            return purposes.Remove(purposeId, out purpose);
        }

        public bool Remove(Purpose purpose)
        {
            if (purpose == null)
            {
                throw new ArgumentNullException(nameof(purpose));
            }

            return Remove(purpose.Id);
        }

        public bool Contains(int purposeId)
        {
            return purposes.ContainsKey(purposeId);
        }

        public bool TryGet(int purposeId, out Purpose purpose)
        {
            return purposes.TryGetValue(purposeId, out purpose);
        }

        public IEnumerator<Purpose> GetEnumerator()
        {
            return purposes.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
