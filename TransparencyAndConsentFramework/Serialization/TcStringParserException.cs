using System;

namespace Bidtellect.Tcf.Serialization
{
    public class TcStringParserException : Exception
    {
        public ExceptionType Type { get; set; }

        public TcStringParserException(ExceptionType type) : base(GetMessage(type))
        {
            Type = type;
        }

        public enum ExceptionType
        {
            InvalidVersion,
            InvalidVendorId,
        }

        protected static string GetMessage(ExceptionType type)
        {
            switch (type)
            {
                case ExceptionType.InvalidVersion:
                    return "Invalid TC String version.";

                case ExceptionType.InvalidVendorId:
                    return "Invalid Vendor ID.";

                default:
                    return "An error occurred while parsing a TC String.";
            }
        }
    }
}
