using System;
using System.Collections;
using System.Collections.Generic;

namespace Bidtellect.Tcf.Models.Components.VendorList
{
    /// <summary>
    /// Represents a collection of Features.
    /// </summary>
    public class FeatureCollection : IEnumerable<KeyValuePair<int, Feature>>
    {
        protected Dictionary<int, Feature> features;

        /// <summary>
        /// Gets an enumerable collection of the Feature IDs contained in this collection.
        /// </summary>
        public IEnumerable<int> Ids => features.Keys;

        /// <summary>
        /// Gets the number of elements contained in this collection.
        /// </summary>
        public int Count => features.Count;

        /// <summary>
        /// Initializes a new instance of <c>FeatureCollection</c>.
        /// </summary>
        public FeatureCollection()
        {
            features = new Dictionary<int, Feature>();
        }

        /// <inheritdoc cref="FeatureCollection.FeatureCollection()"/>
        /// <param name="capacity">
        /// The initial capacity of the collection.
        /// </param>
        public FeatureCollection(int capacity)
        {
            features = new Dictionary<int, Feature>(capacity);
        }

        /// <inheritdoc cref="FeatureCollection.Add(int, Feature)"/>
        public void Add(Feature feature)
        {
            Add(feature.Id, feature);
        }

        /// <inheritdoc cref="FeatureCollection.Add(int, Feature)"/>
        public void Add(int featureId)
        {
            Add(featureId, null);
        }

        /// <summary>
        /// Adds a Feature to this collection.
        /// </summary>
        /// <param name="featureId">The ID of the feature.</param>
        /// <param name="feature">The Feature to be added.</param>
        /// <exception cref="ArgumentException" />
        /// <exception cref="ArgumentOutOfRangeException" />
        public void Add(int featureId, Feature feature)
        {
            if (featureId < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(featureId));
            }

            if (feature != null && feature.Id != featureId)
            {
                throw new ArgumentException("The ID doesn't match the ID of the Feature object.", nameof(featureId));
            }

            features[featureId] = feature;
        }

        /// <summary>
        /// Removes a Feature with the given Feature ID from this collection.
        /// </summary>
        /// <param name="featureId">The ID of the Feature.</param>
        /// <returns>
        /// A value indicating whether the Feature was successfully found and removed.
        /// </returns>
        public bool Remove(int featureId)
        {
            return features.Remove(featureId);
        }

        /// <summary>
        /// Removes a Feature from this collection.
        /// </summary>
        /// <param name="feature">The Feature to be removed.</param>
        /// <returns>
        /// A value indicating whether the Feature was successfully found and removed.
        /// </returns>
        /// <exception cref="System.ArgumentNullException"/>
        public bool Remove(Feature feature)
        {
            if (feature == null)
            {
                throw new ArgumentNullException(nameof(feature));
            }

            return Remove(feature.Id);
        }

        /// <summary>
        /// Determines whether the collection contains a Feature with the given ID.
        /// </summary>
        /// <param name="featureId">The ID of the Feature.</param>
        /// <returns>
        /// <c>true</c> if this collection contains a Feature with the given ID;
        /// otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(int featureId)
        {
            return features.ContainsKey(featureId);
        }

        /// <summary>
        /// Gets a Feature with the given ID.
        /// </summary>
        /// <param name="featureId">The ID of the Feature.</param>
        /// <param name="feature">
        /// When this method returns, contains the Feature,
        /// if the ID is found; otherwise, <c>null</c>.
        /// This parameter is passed uninitialized.
        /// </param>
        /// <returns>
        /// <c>true</c> if this collection contains a Feature with the given ID;
        /// otherwise, <c>false</c>.
        /// </returns>
        public bool TryGet(int featureId, out Feature feature)
        {
            return features.TryGetValue(featureId, out feature);
        }

        public IEnumerator<KeyValuePair<int, Feature>> GetEnumerator()
        {
            return features.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
