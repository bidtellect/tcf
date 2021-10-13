using System;
using System.Collections;
using System.Collections.Generic;

namespace Bidtellect.Tcf.Models.Components.VendorList
{
    public class FeatureCollection : IEnumerable<Feature>
    {
        protected Dictionary<int, Feature> features = new();

        public int Count => features.Count;

        public void Add(Feature feature)
        {
            features[feature.Id] = feature;
        }

        public void Add(int featureId)
        {
            Add(featureId, null);
        }

        public void Add(int featureId, Feature feature)
        {
            features[featureId] = feature;
        }

        public bool Remove(int featureId)
        {
            return features.Remove(featureId);
        }

        public bool Remove(int featureId, out Feature feature)
        {
            return features.Remove(featureId, out feature);
        }

        public bool Remove(Feature feature)
        {
            if (feature == null)
            {
                throw new ArgumentNullException(nameof(feature));
            }

            return Remove(feature.Id);
        }

        public bool Contains(int featureId)
        {
            return features.ContainsKey(featureId);
        }

        public bool TryGet(int featureId, out Feature feature)
        {
            return features.TryGetValue(featureId, out feature);
        }

        public IEnumerator<Feature> GetEnumerator()
        {
            return features.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
