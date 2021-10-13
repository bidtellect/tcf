using System.Collections;
using System.Collections.Generic;

namespace Bidtellect.Tcf.Models.Components.ConsentString
{
    public class PublisherRestrictionCollection : IEnumerable<PublisherRestriction>
    {
        protected Dictionary<int, PublisherRestriction> publisherRestrictions;

        public int Count => publisherRestrictions.Count;

        public PublisherRestrictionCollection()
        {
            publisherRestrictions = new();
        }

        public PublisherRestrictionCollection(int capacity)
        {
            publisherRestrictions = new(capacity);
        }

        public void Add(int purposeId, PublisherRestriction publisherRestriction)
        {
            publisherRestrictions[purposeId] = publisherRestriction;
        }

        public bool Remove(int purposeId)
        {
            return publisherRestrictions.Remove(purposeId);
        }

        public bool Remove(int purposeId, out PublisherRestriction publisherRestriction)
        {
            return publisherRestrictions.Remove(purposeId, out publisherRestriction);
        }

        public bool Contains(int purposeId)
        {
            return publisherRestrictions.ContainsKey(purposeId);
        }

        public bool TryGet(int purposeId, out PublisherRestriction publisherRestriction)
        {
            return publisherRestrictions.TryGetValue(purposeId, out publisherRestriction);
        }

        public IEnumerator<PublisherRestriction> GetEnumerator()
        {
            return publisherRestrictions.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
