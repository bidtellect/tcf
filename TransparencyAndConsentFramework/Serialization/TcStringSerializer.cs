using Bidtellect.Tcf.Models;
using Bidtellect.Tcf.Models.Components.ConsentString;
using Bidtellect.Tcf.Models.Components.VendorList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Bidtellect.Tcf.Serialization
{
    /// <summary>
    /// Provides functionality to serialize TC Strings.
    /// </summary>
    public class TcStringSerializer
    {
        /// <summary>
        /// Serializes the given <c>TcString</c> object into a string representation.
        /// </summary>
        /// <param name="tcString">The value to be serialized.</param>
        /// <returns>
        /// A string representation of the <c>TcString</c> object.
        /// </returns>
        public string Serialize(TcString tcString)
        {
            var builder = new StringBuilder();

            builder.Append(SerializeCore(tcString.Core));

            if (tcString.DisclosedVendors != null)
            {
                builder.Append(".");
                builder.Append(SerializeDisclosedVendors(tcString.DisclosedVendors));
            }

            if (tcString.PublisherTc != null)
            {
                builder.Append(".");
                builder.Append(SerializePublisherTc(tcString.PublisherTc));
            }

            return builder.ToString();
        }

        protected string SerializeCore(CoreString core)
        {
            using (var stream = new MemoryStream())
            {
                using (var binaryWriter = new BinaryWriter(stream))
                using (var writer = new BitWriter(binaryWriter))
                {
                    Write(writer, core.Version, 6);

                    Write(writer, core.Created, 36);
                    Write(writer, core.LastUpdated, 36);

                    Write(writer, core.CmpId, 12);
                    Write(writer, core.CmpVersion, 12);

                    Write(writer, core.ConsentScreen, 6);
                    Write(writer, core.ConsentLanguage, 6);

                    Write(writer, core.VendorListVersion, 12);
                    Write(writer, core.PolicyVersion, 6);

                    Write(writer, core.IsServiceSpecific);
                    Write(writer, core.UsesNonStandardStacks);

                    Write(writer, core.SpecialFeatureOptIns, 12);

                    Write(writer, core.PurposesConsents, 24);
                    Write(writer, core.PurposesLegitimateInterests, 24);
                    Write(writer, core.PurposeOneTreatment);

                    Write(writer, core.PublisherCountryCode, 6);

                    Write(writer, core.VendorConsents);
                    Write(writer, core.VendorLegitimateInterests);

                    Write(writer, core.PublisherRestrictions);
                }

                return EncodeBase64(stream.ToArray());
            }
        }

        protected string SerializeDisclosedVendors(VendorCollection vendors)
        {
            using (var stream = new MemoryStream())
            {
                using (var binaryWriter = new BinaryWriter(stream))
                using (var writer = new BitWriter(binaryWriter))
                {
                    Write(writer, (int)SegmentType.DisclosedVendors, 3);

                    Write(writer, vendors);
                }

                return EncodeBase64(stream.ToArray());
            }
        }

        protected string SerializePublisherTc(PublisherTc publisherTc)
        {
            using (var stream = new MemoryStream())
            {
                using (var binaryWriter = new BinaryWriter(stream))
                using (var writer = new BitWriter(binaryWriter))
                {
                    Write(writer, (int)SegmentType.PublisherTc, 3);

                    Write(writer, publisherTc.PurposeConsents, 24);
                    Write(writer, publisherTc.PurposeLegitimateInterests, 24);

                    var customPurposesCount = Math.Max(
                        publisherTc.CustomPurposeConsents.Count,
                        publisherTc.CustomPurposeLegitimateInterests.Count
                    );

                    Write(writer, customPurposesCount, 6);

                    Write(writer, publisherTc.CustomPurposeConsents, customPurposesCount);
                    Write(writer, publisherTc.CustomPurposeLegitimateInterests, customPurposesCount);
                }

                return EncodeBase64(stream.ToArray());
            }
        }

        protected string EncodeBase64(byte[] value)
        {
            return Base64Converter.Encode(value);
        }

        protected void Write(BitWriter writer, bool value)
        {
            writer.Write(value);
        }

        protected void Write(BitWriter writer, int value, int length)
        {
            writer.Write(value, length);
        }

        protected void Write(BitWriter writer, DateTime value, int length)
        {
            var epoch = GetEpoch(value);

            writer.Write(epoch, length);
        }

        protected void Write(BitWriter writer, string value, int length)
        {
            foreach (var letter in value)
            {
                Write(writer, letter - 'a', length);
            }
        }

        protected void Write(BitWriter writer, PublisherRestrictionCollection publisherRestrictions)
        {
            Write(writer, publisherRestrictions.Count, 12);

            foreach (var item in publisherRestrictions)
            {
                Write(writer, item.Key, 6);
                Write(writer, (int)item.Value.RestrictionType, 2);
                WriteVendorRange(writer, item.Value.Vendors);
            }
        }

        protected void Write(BitWriter writer, FeatureCollection features, int length)
        {
            for (var i = 1; i <= length; i += 1)
            {
                Write(writer, features.Contains(i));
            }
        }

        protected void Write(BitWriter writer, PurposeCollection purposes, int length)
        {
            for (var i = 1; i <= length; i += 1)
            {
                Write(writer, purposes.Contains(i));
            }
        }

        protected void Write(BitWriter writer, VendorCollection vendors)
        {
            var orderedVendorIds = ToOrderedArray(vendors);

            if (orderedVendorIds.Any())
            {
                var maxVendorId = orderedVendorIds.Last();

                Write(writer, maxVendorId, 16);

                var isRangeShorter = CalculateVendorRangeSize(orderedVendorIds) < CalculateVendorBitFieldSize(maxVendorId);

                if (isRangeShorter)
                {
                    Write(writer, true);
                    WriteVendorRange(writer, orderedVendorIds);
                }
                else
                {
                    Write(writer, false);
                    WriteVendorBitField(writer, vendors, maxVendorId);
                }
            }
            else
            {
                // Render an empty bit field.
                Write(writer, 0, 16);
                Write(writer, false);
            }
        }

        protected virtual void WriteVendorBitField(BitWriter writer, VendorCollection vendors, int length)
        {
            for (var i = 1; i <= length; i += 1)
            {
                Write(writer, vendors.Contains(i));
            }
        }

        protected virtual void WriteVendorRange(BitWriter writer, VendorCollection vendors)
        {
            WriteVendorRange(writer, ToOrderedArray(vendors));
        }

        protected virtual void WriteVendorRange(BitWriter writer, int[] orderedVendorIds)
        {
            var ranges = GetVendorRanges(orderedVendorIds);

            WriteVendorRangeCount(writer, ranges.Count);

            foreach (var range in ranges)
            {
                if (range.EndVendorId == range.StartVendorId)
                {
                    Write(writer, false);
                    WriteVendorId(writer, range.StartVendorId);
                }
                else
                {
                    Write(writer, true);
                    WriteVendorId(writer, range.StartVendorId);
                    WriteVendorId(writer, range.EndVendorId);
                }
            }
        }

        protected virtual void WriteVendorId(BitWriter writer, int vendorId)
        {
            Write(writer, vendorId, 16);
        }

        protected virtual void WriteVendorRangeCount(BitWriter writer, int count)
        {
            Write(writer, count, 12);
        }

        protected int CalculateVendorBitFieldSize(int maxVendorId)
        {
            return maxVendorId - 1;
        }

        protected int CalculateVendorRangeSize(int[] orderedVendorIds)
        {
            using (var binaryWriter = BinaryWriter.Null)
            using (var writer = new BitWriter(binaryWriter))
            {
                WriteVendorRange(writer, orderedVendorIds);

                return writer.Position;
            }
        }

        protected static ulong GetEpoch(DateTime value)
        {
            return (ulong)((value - new DateTime(1970, 1, 1)).TotalMilliseconds / 100);
        }

        protected static int[] ToOrderedArray(VendorCollection vendors)
        {
            return vendors.Ids.OrderBy(x => x).ToArray();
        }

        protected static List<VendorRange> GetVendorRanges(int[] orderedVendorIds)
        {
            var ranges = new List<VendorRange>();

            if (orderedVendorIds.Length == 0)
            {
                return ranges;
            }

            var currentRange = new VendorRange(orderedVendorIds[0]);

            for (var i = 1; i < orderedVendorIds.Length; i += 1)
            {
                if (currentRange.EndVendorId + 1 == orderedVendorIds[i])
                {
                    currentRange.EndVendorId += 1;
                }
                else
                {
                    ranges.Add(currentRange);

                    currentRange = new VendorRange(orderedVendorIds[i]);
                }
            }

            ranges.Add(currentRange);

            return ranges;
        }

        protected struct VendorRange
        {
            public VendorRange(int vendorId)
            {
                StartVendorId = vendorId;
                EndVendorId = vendorId;
            }

            public int StartVendorId { get; set; }
            public int EndVendorId { get; set; }
        }
    }
}
