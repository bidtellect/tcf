using Bidtellect.Tcf.Models;
using Bidtellect.Tcf.Models.Components.ConsentString;
using Bidtellect.Tcf.Models.Components.VendorList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bidtellect.Tcf.Serialization
{
    /// <summary>
    /// Provides functionality to parse TC Strings.
    /// </summary>
    public class TcStringParser
    {
        protected readonly Dictionary<int, Feature> specialFeatureLookup;
        protected readonly Dictionary<int, Purpose> purposeLookup;
        protected readonly Dictionary<int, Vendor> vendorLookup;

        /// <summary>
        /// Initializes a new instance of <c>TcStringParser</c>.
        /// </summary>
        /// <param name="vendorList">
        /// A Global Vendor List object used for reference when parsing.
        /// This value can be <c>null</c>.
        /// </param>
        public TcStringParser(VendorList vendorList)
        {
            if (vendorList != null)
            {
                specialFeatureLookup = vendorList.SpecialFeatures.ToDictionary(x => x.Key, x => x.Value);
                purposeLookup = vendorList.Purposes.ToDictionary(x => x.Key, x => x.Value);
                vendorLookup = vendorList.Vendors.ToDictionary(x => x.Key, x => x.Value);
            }
        }

        /// <summary>
        /// Parses the given string value into a <c>TcString</c> object.
        /// </summary>
        /// <param name="value">The value to be parsed.</param>
        /// <returns>
        /// A model which represents the given value when parsed.
        /// </returns>
        public TcString Parse(string value)
        {
            return Parse(value, new ParseOptions());
        }

        /// <inheritdoc cref="TcStringParser.Parse(string)" />
        /// <param name="value">The value to be parsed.</param>
        /// <param name="options">Parse options.</param>
        public TcString Parse(string value, ParseOptions options)
        {
            var tcString = new TcString();

            Parse(value, tcString, options);

            return tcString;
        }

        protected void Parse(string value, TcString tcString, ParseOptions options)
        {
            var segments = value.Split('.');

            ParseCore(segments.First(), tcString, options);

            foreach (var base64Segment in segments.Skip(1))
            {
                switch (GetSegmentType(base64Segment))
                {
                    case SegmentType.DisclosedVendors:
                        ParseDisclosedVendors(base64Segment, tcString, options);
                        break;

                    case SegmentType.PublisherTc:
                        ParsePublisherTc(base64Segment, tcString, options);
                        break;
                }
            }
        }

        protected void ParseCore(string value, TcString tcString, ParseOptions options)
        {
            if (options.ExcludeCore)
            {
                return;
            }

            var reader = CreateBitReader(value);

            var core = new CoreString();

            core.Version = ReadVersion(reader);

            core.Created = ReadEpoch(reader, 36);
            core.LastUpdated = ReadEpoch(reader, 36);

            core.CmpId = reader.ReadInt(12);
            core.CmpVersion = reader.ReadInt(12);

            core.ConsentScreen = reader.ReadInt(6);
            core.ConsentLanguage = ReadLetters(reader, 2, 6);

            core.VendorListVersion = reader.ReadInt(12);
            core.PolicyVersion = reader.ReadInt(6);

            core.IsServiceSpecific = reader.ReadBit();
            core.UsesNonStandardStacks = reader.ReadBit();

            core.SpecialFeatureOptIns = ReadSpecialFeatures(reader);

            core.PurposesConsents = ReadPurposes(reader, 24);
            core.PurposesLegitimateInterests = ReadPurposes(reader, 24);
            core.PurposeOneTreatment = reader.ReadBit();

            core.PublisherCountryCode = ReadLetters(reader, 2, 6);

            core.VendorConsents = ReadVendors(reader);
            core.VendorLegitimateInterests = ReadVendors(reader);

            core.PublisherRestrictions = ReadPublisherRestrictions(reader);

            tcString.Core = core;
        }

        protected void ParseDisclosedVendors(string value, TcString tcString, ParseOptions options)
        {
            if (options.ExcludeDisclosedVendors)
            {
                return;
            }

            var reader = CreateBitReader(value);

            // Discard Segment type.
            reader.ReadInt(3);

            tcString.DisclosedVendors = ReadVendors(reader);
        }

        protected void ParsePublisherTc(string value, TcString tcString, ParseOptions options)
        {
            if (options.ExcludePublisherTc)
            {
                return;
            }

            var reader = CreateBitReader(value);

            // Discard Segment type.
            reader.ReadInt(3);

            var publisherTc = new PublisherTc
            {
                PurposeConsents = ReadPurposes(reader, 24),
                PurposeLegitimateInterests = ReadPurposes(reader, 24),
            };

            var customPurposesCount = reader.ReadInt(6);

            publisherTc.CustomPurposeConsents = ReadCustomPurpose(reader, customPurposesCount);
            publisherTc.CustomPurposeLegitimateInterests = ReadCustomPurpose(reader, customPurposesCount);

            tcString.PublisherTc = publisherTc;
        }

        protected virtual int ReadVersion(BitReader reader)
        {
            var version = reader.ReadInt(6);

            if (version != 2)
            {
                throw new TcStringParserException(TcStringParserException.ExceptionType.InvalidVersion);
            }

            return version;
        }

        protected virtual IEnumerable<KeyValuePair<int, bool>> ReadBitField(BitReader reader, int length)
        {
            var bits = reader.ReadBits(length);

            for (var i = 0; i < length; i += 1)
            {
                yield return new KeyValuePair<int, bool>(i + 1, bits[i]);
            }
        }

        protected virtual FeatureCollection ReadSpecialFeatures(BitReader reader)
        {
            const int featureCount = 12;

            var collection = new FeatureCollection(featureCount);

            foreach (var item in ReadBitField(reader, featureCount))
            {
                if (item.Value)
                {
                    collection.Add(item.Key, GetSpecialFeature(item.Key));
                }
            }

            return collection;
        }

        protected virtual PurposeCollection ReadPurposes(BitReader reader, int length)
        {
            var collection = new PurposeCollection();

            foreach (var item in ReadBitField(reader, length))
            {
                if (item.Value)
                {
                    collection.Add(item.Key, GetPurpose(item.Key));
                }
            }

            return collection;
        }

        protected virtual PurposeCollection ReadCustomPurpose(BitReader reader, int length)
        {
            var collection = new PurposeCollection();

            foreach (var item in ReadBitField(reader, length))
            {
                if (item.Value)
                {
                    collection.Add(item.Key);
                }
            }

            return collection;
        }

        protected virtual VendorCollection ReadVendors(BitReader reader)
        {
            var maxVendorId = reader.ReadInt(16);
            var isRangeEncoding = reader.ReadBit();

            var collection = new VendorCollection();

            if (isRangeEncoding)
            {
                ReadVendorRange(reader, collection);
            }
            else
            {
                ReadVendorBitField(reader, maxVendorId, collection);
            }

            return collection;
        }

        protected virtual void ReadVendorBitField(BitReader reader, int length, VendorCollection collection)
        {
            var bits = reader.ReadBits(length);

            for (var i = 1; i <= length; i += 1)
            {
                if (bits[i - 1])
                {
                    AddVendor(collection, i);
                }
            }
        }

        protected virtual void ReadVendorRange(BitReader reader, VendorCollection collection)
        {
            var rangeCount = ReadVendorRangeCount(reader);

            for (var i = 0; i < rangeCount; i += 1)
            {
                var isRange = reader.ReadBit();

                if (isRange)
                {
                    var startVendorId = ReadVendorId(reader);
                    var endVendorId = ReadVendorId(reader);

                    AddVendorRange(collection, startVendorId, endVendorId);
                }
                else
                {
                    var vendorId = ReadVendorId(reader);

                    AddVendor(collection, vendorId);
                }
            }
        }

        protected virtual void AddVendor(VendorCollection collection, int vendorId)
        {
            collection.Add(vendorId, GetVendor(vendorId));
        }

        protected virtual void AddVendorRange(VendorCollection collection, int startVendorId, int endVendorId)
        {
            for (var i = startVendorId; i <= endVendorId; i += 1)
            {
                AddVendor(collection, i);
            }
        }

        protected virtual int ReadVendorRangeCount(BitReader reader)
        {
            return reader.ReadInt(12);
        }

        protected virtual int ReadVendorId(BitReader reader)
        {
            var value = reader.ReadInt(16);

            if (value <= 0)
            {
                throw new TcStringParserException(TcStringParserException.ExceptionType.InvalidVendorId);
            }

            return value;
        }

        protected virtual string ReadLetters(BitReader reader, int count, int length)
        {
            var builder = new StringBuilder(count);

            for (var i = 0; i < count; i += 1)
            {
                builder.Append((char)('a' + reader.ReadInt(length)));
            }

            return builder.ToString();
        }

        protected virtual PublisherRestrictionCollection ReadPublisherRestrictions(BitReader reader)
        {
            var count = reader.ReadInt(12);

            var collection = new PublisherRestrictionCollection(count);

            for (var i = 0; i < count; i += 1)
            {
                var purposeId = reader.ReadInt(6);

                var publisherRestriction = new PublisherRestriction
                {
                    Purpose = GetPurpose(purposeId),
                    RestrictionType = (RestrictionType)reader.ReadInt(2),
                    Vendors = new VendorCollection(),
                };

                ReadVendorRange(reader, publisherRestriction.Vendors);

                collection.Add(purposeId, publisherRestriction);
            }

            return collection;
        }

        protected virtual TValue GetItemFromLookup<TKey, TValue>(TKey key, IDictionary<TKey, TValue> lookup)
        {
            if (lookup == null)
            {
                return default;
            }

            if (lookup.TryGetValue(key, out var value))
            {
                return value;
            }

            return default;
        }

        protected virtual Purpose GetPurpose(int purposeId)
        {
            return GetItemFromLookup(purposeId, purposeLookup);
        }

        protected virtual Feature GetSpecialFeature(int featureId)
        {
            return GetItemFromLookup(featureId, specialFeatureLookup);
        }

        protected virtual Vendor GetVendor(int vendorId)
        {
            return GetItemFromLookup(vendorId, vendorLookup);
        }

        protected static BitReader CreateBitReader(string base64String)
        {
            return new BitReader(Base64Converter.Decode(base64String));
        }

        protected static DateTime ReadEpoch(BitReader reader, int length)
        {
            var epoch = reader.ReadLong(length);

            return new DateTime(1970, 1, 1).AddMilliseconds(epoch * 100);
        }

        protected static SegmentType GetSegmentType(string unparsedSection)
        {
            var firstByte = Base64Converter
                .Decode(unparsedSection.Substring(0, 2))
                .First();

            return (SegmentType)((firstByte >> 5) & 0b111);
        }

        /// <summary>
        /// Represents <c>TcStringParser</c> options.
        /// </summary>
        public struct ParseOptions
        {
            /// <summary>
            /// Gets or sets a value indicating whether to exclude the Core segment when parsing.
            /// </summary>
            public bool ExcludeCore { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether to exclude the Disclosed Vendors segment when parsing.
            /// </summary>
            public bool ExcludeDisclosedVendors { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether to exclude Publisher TC segment when parsing.
            /// </summary>
            public bool ExcludePublisherTc { get; set; }
        }
    }
}
