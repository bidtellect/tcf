namespace Bidtellect.Tcf.Serialization
{
    /// <inheritdoc cref="TcStringSerializer" />
    public class TcStringSerializerFib : TcStringSerializer
    {
        protected override void WriteVendorId(BitWriter writer, int vendorId)
        {
            writer.WriteFib(vendorId);
        }

        protected override void WriteVendorRangeCount(BitWriter writer, int count)
        {
            writer.WriteFib(count + 1);
        }

        protected override void WriteVendorRange(BitWriter writer, int[] orderedVendorIds)
        {
            var ranges = GetVendorRanges(orderedVendorIds);

            WriteVendorRangeCount(writer, ranges.Count);

            var lastVendorId = 0;

            foreach (var range in ranges)
            {
                if (range.EndVendorId == range.StartVendorId)
                {
                    Write(writer, false);
                    writer.WriteFib(range.StartVendorId - lastVendorId);
                }
                else
                {
                    Write(writer, true);
                    writer.WriteFib(range.StartVendorId - lastVendorId);
                    writer.WriteFib(range.EndVendorId - range.StartVendorId);
                }

                lastVendorId = range.EndVendorId;
            }
        }
    }
}
