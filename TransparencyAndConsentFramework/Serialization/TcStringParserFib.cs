using Bidtellect.Tcf.Models;
using Bidtellect.Tcf.Models.Components.VendorList;

namespace Bidtellect.Tcf.Serialization
{
    /// <inheritdoc cref="TcStringParser"/>
    public class TcStringParserFib : TcStringParser
    {
        protected const int MaxVendorId = (1 << 16) - 1;

        /// <inheritdoc cref="TcStringParser.TcStringParser(VendorList)"/>
        public TcStringParserFib(VendorList vendorList)
            : base(vendorList)
        {
        }

        protected override int ReadVersion(BitReader reader)
        {
            var version = reader.ReadInt(6);

            if (version != 3)
            {
                throw new TcStringParserException(TcStringParserException.ExceptionType.InvalidVersion);
            }

            return version;
        }

        protected override int ReadVendorId(BitReader reader)
        {
            var value = reader.ReadFib();

            if (value > MaxVendorId)
            {
                throw new System.InvalidOperationException();
            }

            return (int)value;
        }

        protected override int ReadVendorRangeCount(BitReader reader)
        {
            return (int)reader.ReadFib() - 1;
        }

        protected override void ReadVendorRange(BitReader reader, VendorCollection collection)
        {
            var rangeCount = ReadVendorRangeCount(reader);

            var offset = 0;

            for (var i = 0; i < rangeCount; i += 1)
            {
                var isRange = reader.ReadBit();

                if (isRange)
                {
                    var startVendorId = ReadVendorId(reader) + offset;
                    var endVendorId = ReadVendorId(reader) + startVendorId;

                    AddVendorRange(collection, startVendorId, endVendorId);

                    offset = endVendorId;
                }
                else
                {
                    var vendorId = ReadVendorId(reader) + offset;

                    AddVendor(collection, vendorId);

                    offset = vendorId;
                }
            }
        }
    }
}
